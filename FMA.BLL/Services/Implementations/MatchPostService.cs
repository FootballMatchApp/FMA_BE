using FMA.BLL.Services.Interfaces;
using FMA.Common.DTOs;
using FMA.Common.Enums;
using FMA.DAL.Entities;
using FMA.DAL.Repositories.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace FMA.BLL.Services.Implementations
{
    public class MatchPostService : IMatchPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MatchPostService> _logger;

        public MatchPostService(IUnitOfWork unitOfWork, ILogger<MatchPostService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ResponseDTO> GetAllAsync()
        {
            try
            {
                var posts = await _unitOfWork.MatchPostRepository.GetAllAsync();
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

                return new ResponseDTO
                {
                    StatusCode = 200,
                    Message = "Posts fetched successfully",
                    IsSuccess = true,
                    Result = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in MatchPostService.GetAllAsync");
                return new ResponseDTO
                {
                    StatusCode = 500,
                    Message = "Internal server error",
                    IsSuccess = false,
                    Result = null
                };
            }
        }

        public async Task<ResponseDTO> CreateAsync(CreateMatchPostDTO dto, Guid userId)
        {
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
                    PostStatus = PostStatus.PENDING
                };

                await _unitOfWork.MatchPostRepository.AddAsync(newPost);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO("Tạo bài post thành công", 201, true, newPost);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateAsync");
                return new ResponseDTO("Lỗi khi tạo bài post", 500, false);
            }
        }
    }
}
