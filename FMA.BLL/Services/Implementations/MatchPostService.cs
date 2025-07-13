using FMA.BLL.Services.Interfaces;
using FMA.BLL.Utilities;
using FMA.Common.DTOs;
using FMA.Common.Enums;
using FMA.DAL.Entities;
using FMA.DAL.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace FMA.BLL.Services.Implementations
{
    public class MatchPostService : IMatchPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserUtility _userUtility;

        public MatchPostService(IUnitOfWork unitOfWork, UserUtility userUtility)
        {
            _unitOfWork = unitOfWork;

            _userUtility = userUtility;
        }

        public async Task<ResponseDTO> GetAllAsync()
        {
            try
            {
                var posts = await _unitOfWork.MatchPostRepository.GetAllAsync();
                if (posts == null || !posts.Any())
                {
                    return new ResponseDTO("There are no match post", 404, false);
                }
                var result = posts.Select(p => new MatchPostDTO
                {
                    PostId = p.PostId,
                    PostById = p.PostById,
                    ReceivingUserId = p.ReceivingUserId,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    PitchId = p.PitchId,
                    PostByTeamId = p.PostByTeamId,
                    MatchTime = p.MatchTime,
                    Description = p.Description,
                    PostStatus = p.PostStatus.ToString()
                }).ToList();

                return new ResponseDTO ("Get match post successfully", 200, true, result);

            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error getting matchpost : {ex.Message}", 500, false);

            }
        }

        public async Task<ResponseDTO> CreateAsync(CreateMatchPostDTO dto)
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
            {
                return new ResponseDTO("User is not valid", 400, false);
            }
            try
            {
                var newPost = new MatchPost
                {
                    PostId = Guid.NewGuid(),
                    PostById = userId,
                    PitchId = dto.PitchId,
                    MatchTime = dto.MatchTime,
                    Description = dto.Description,
                    PostByTeamId = dto.PostByTeamId,
                    PostStatus = PostStatus.PENDING,
                    CreatedAt = DateTime.UtcNow,

                };

                await _unitOfWork.MatchPostRepository.AddAsync(newPost);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO("Create match post successfully", 201, true, newPost);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error saving matchpost: {ex.Message}", 500, false);
            }
        }
    }
}
