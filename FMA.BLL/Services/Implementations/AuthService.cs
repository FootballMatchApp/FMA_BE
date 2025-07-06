using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FMA.BLL.Services.Interfaces;
using FMA.BLL.Utilities;
using FMA.Common.Constants;
using FMA.Common.DTOs;
using FMA.Common.Enums;
using FMA.DAL.Entities;
using FMA.DAL.Repositories.UnitOfWork;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FMA.BLL.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserUtility _userUtility;
        public AuthService(IUnitOfWork unitOfWork, UserUtility userUtility)
        {
            _unitOfWork = unitOfWork;
            _userUtility = userUtility;
        }
        public async Task<ResponseDTO> LoginAsync(LoginDTO loginDto)
        {
            //kiểm tra email
            var user = await _unitOfWork.UserRepository.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return new ResponseDTO("User is not valid !", 400, false);
            }

            

            // kiểm tra mật khẩu
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                return new ResponseDTO("Email or Password is not correct !!", 400, false);
            }

            //kiểm tra token
            var exitsRefreshToken = await _unitOfWork.UserTokenRepository.GetRefreshTokenByUserID(user.UserId);
            if (exitsRefreshToken != null)
            {
                exitsRefreshToken.IsRevoked = true;
                await _unitOfWork.UserTokenRepository.UpdateAsync(exitsRefreshToken);
            }

            //khởi tạo claim
            var claims = new List<Claim>
            {
                new Claim(JwtConstant.KeyClaim.userId, user.UserId.ToString()),
                new Claim(JwtConstant.KeyClaim.Role, user.Role.ToString())
            };

            //tạo refesh token
            var refreshTokenKey = JwtProvider.GenerateRefreshToken(claims);
            var accessTokenKey = JwtProvider.GenerateAccessToken(claims);

            var refreshToken = new UserToken
            {
                TokenId = Guid.NewGuid(),
                TokenKey = refreshTokenKey,
                UserId = user.UserId,
                IsRevoked = false,
                Type = TokenType.ACCOUNT_ACTIVATION,
                CreatedAt = DateTime.UtcNow
            };

            _unitOfWork.UserTokenRepository.Add(refreshToken);
            try
            {
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error saving refresh token: {ex.Message}", 500, false);
            }


            return new ResponseDTO("Login successfully !!", 200, true, new
            {
                AccessToken = accessTokenKey,
            });
        }

        public async Task<ResponseDTO> LogoutAsync()
        {
            var userId = _userUtility.GetUserIdFromToken();

            if (userId == null)
            {
                return new ResponseDTO("User ID not found in token.", 400, false);
            }
            try
            {
                // Retrieve the refresh token for the user
                var refreshToken = await _unitOfWork.UserTokenRepository.GetRefreshTokenByUserID(userId);
                if (refreshToken == null)
                {
                    return new ResponseDTO("Logout failse", 404, false);
                }

                // Revoke the refresh token
                refreshToken.IsRevoked = true;
                await _unitOfWork.UserTokenRepository.UpdateAsync(refreshToken);

                // Save changes
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO("Logout successfully !!!", 200, true);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error during logout: {ex.Message}", 500, false);
            }
        }

        public Task<ResponseDTO> RegisterAsync(RegisterDTO registerDto)
        {
            throw new NotImplementedException();
        }
    }
}
