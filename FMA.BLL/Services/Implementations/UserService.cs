using AutoMapper;
using FMA.BLL.Services.Interfaces;
using FMA.BLL.Utilities;
using FMA.Common.DTOs;
using FMA.DAL.Entities;
using FMA.DAL.Repositories.Interfaces;
using FMA.DAL.UnitOfWork;

namespace FMA.BLL.Services.Implementations;

public class UserService : IUserService
{ 
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserUtility _userUtility;
    public UserService (IUnitOfWork unitOfWork, UserUtility userUtility)
    {
        _unitOfWork = unitOfWork;
        _userUtility = userUtility;
    }

    public async Task<ResponseDTO> GetProfileAsync()
    {
        var userId =  _userUtility.GetUserIdFromToken();
        if (userId == Guid.Empty)
        {
            return new ResponseDTO("User not found", 404, false);
        }
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return new ResponseDTO("User not found", 404, false);
        }

        // Lấy TeamMember kèm User và Team
        var teamMember = await _unitOfWork.TeamMemberRepository.GetTeamMemberDetailAsync(userId);
        
        var team = teamMember?.Team;

        if (user == null)
        {
            return new ResponseDTO("User not found", 404, false);
        }

        var userProfileDto = new UserProfileDTO
        {
            UserId = user.UserId,
            FullName = user.Username,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Position = teamMember?.Position,
            JoinDate = teamMember?.JoinDate,
            TeamId = team?.TeamId ,
            TeamName = team?.TeamName,
            TeamDescription = team?.Description
        };

        return new ResponseDTO("Success", 200, true, userProfileDto);
    }

    public async Task<ResponseDTO> UpdateProfileAsync(UpdateUserDTO updateUserDTO)
    {
        var userId = _userUtility.GetUserIdFromToken();
        if (userId == Guid.Empty)
        {
            return new ResponseDTO("User not found", 404, false);
        }
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return new ResponseDTO("User not found", 404, false);
        }
        // Cập nhật thông tin người dùng
        user.Username = updateUserDTO.Username;
        user.Email = updateUserDTO.Email;
        
        if (updateUserDTO.Password != null && updateUserDTO.Password.Length > 0)
        {
            // Mã hóa mật khẩu trước khi lưu
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updateUserDTO.Password);

        }
        // Lưu thay đổi vào cơ sở dữ liệu
        try
        {
            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangeAsync();
        }
        catch (Exception ex)
        {
            return new ResponseDTO($"Error updating profile: {ex.Message}", 500, false);
        }
        return new ResponseDTO("Profile updated successfully", 200, true);


    }
}