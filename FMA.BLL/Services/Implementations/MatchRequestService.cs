using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMA.BLL.Services.Interfaces;
using FMA.BLL.Utilities;
using FMA.Common.DTOs;
using FMA.Common.Enums;
using FMA.DAL.Entities;
using FMA.DAL.UnitOfWork;

namespace FMA.BLL.Services.Implementations
{
    public class MatchRequestService : IMatchRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserUtility _userUtility;
        public MatchRequestService(IUnitOfWork unitOfWork, UserUtility userUtility)
        {
            _unitOfWork = unitOfWork;
            _userUtility = userUtility;
        }
        public async Task<ResponseDTO> AcceptMatchRequestAsync(Guid matchRequestId)
        {
            var matchRequest = await _unitOfWork.MatchRequestRepository.GetByIdAsync(matchRequestId);
            if (matchRequest == null)
            {
                return new ResponseDTO("Match request not found.", 400, false);
            }
            if (matchRequest.Status != MatchRequestStatus.IN_PROGRESS)
            {
                return new ResponseDTO("Match request is not in a state that can be accepted.", 400, false);
            }
            matchRequest.Status = MatchRequestStatus.ACCEPTED;
            matchRequest.DecisionTime = DateTime.UtcNow;
            try
            {
                await _unitOfWork.MatchRequestRepository.UpdateAsync(matchRequest);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error updating match request: {ex.Message}", 500, false);

            }
            return new ResponseDTO("Match request accepted successfully.", 200, true);
        }

        public async Task<ResponseDTO> CancelMatchRequestAsync(Guid matchRequestId)
        {
            var matchRequest = await _unitOfWork.MatchRequestRepository.GetByIdAsync(matchRequestId);
            if (matchRequest == null)
            {
                return new ResponseDTO("Match request not found.", 400, false);
            }
            if (matchRequest.Status != MatchRequestStatus.IN_PROGRESS)
            {
                return new ResponseDTO("Match request is not in a state that can be cancelled.", 400, false);
            }
            matchRequest.Status = MatchRequestStatus.REJECTED;
            matchRequest.DecisionTime = DateTime.UtcNow;
            try
            {
                await _unitOfWork.MatchRequestRepository.UpdateAsync(matchRequest);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error updating match request: {ex.Message}", 500, false);
            }
            return new ResponseDTO("Match request cancelled successfully.", 200, true);
        }

        public async Task<ResponseDTO> CreateMatchRequestAsync(CreateMatchRequestDTO createMatchRequestDTO)
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
            {
                return new ResponseDTO("User not authenticated.", 401, false);
            }
            if (createMatchRequestDTO.MatchPostId == Guid.Empty)
            {
                return new ResponseDTO("Match post ID is required.", 400, false);
            }
            var matchPost = await _unitOfWork.MatchPostRepository.GetByIdAsync(createMatchRequestDTO.MatchPostId);
            if (matchPost == null)
            {
                return new ResponseDTO("Match post not found.", 404, false);
            }
            //if (matchPost.Status != MatchPostStatus.OPEN)
            //{
            //    return new ResponseDTO("Match post is not open for requests.", 400, false);
            //}
            var matchRequest = new MatchRequest
            {
                MatchPostId = createMatchRequestDTO.MatchPostId,
                RequestById = userId,
                RequestByTeamId = createMatchRequestDTO.RequestByTeamId,
                Status = MatchRequestStatus.IN_PROGRESS,
                RequestTime = DateTime.UtcNow,
            };
            try
            {
                await _unitOfWork.MatchRequestRepository.AddAsync(matchRequest);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error creating match request: {ex.Message}", 500, false);
            }
            return new ResponseDTO("Match request created successfully.", 201, true);
        }

        public async Task<ResponseDTO> DeleteMatchRequestAsync(Guid matchRequestId)
        {
            var matchRequest = await _unitOfWork.MatchRequestRepository.GetByIdAsync(matchRequestId);
            if (matchRequest == null)
            {
                return new ResponseDTO("Match request not found.", 404, false);
            }
            try
            {
                await _unitOfWork.MatchRequestRepository.DeleteAsync(matchRequestId);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error deleting match request: {ex.Message}", 500, false);
            }
            return new ResponseDTO("Match request deleted successfully.", 200, true);
        }

        public async Task<ResponseDTO> GetAllMatchRequestsAsync()
        {
            var matchRequests =  _unitOfWork.MatchRequestRepository.GetAll();
            if (matchRequests == null || !matchRequests.Any())
            {
                return new ResponseDTO("No match requests found.", 404, false);
            }
            var matchRequestDTOs = matchRequests.Select(mr => new MatchRequestDTO
            {
                MatchRequestId = mr.MatchRequestId,
                MatchPostId = mr.MatchPostId,
                RequestById = mr.RequestById,
                RequestByTeamId = mr.RequestByTeamId,
                Status = mr.Status,
                RequestTime = mr.RequestTime,
                DecisionTime = mr.DecisionTime
            }).ToList();
            return new ResponseDTO("Match requests retrieved successfully.", 200, true, matchRequestDTOs);
        }

        public async Task<ResponseDTO> GetMatchRequestByIdAsync(Guid matchRequestId)
        {
            var matchRequest = await _unitOfWork.MatchRequestRepository.GetByIdAsync(matchRequestId);
            if (matchRequest == null)
            {
                return new ResponseDTO("Match request not found.", 404, false);
            }
            var matchRequestDTO = new MatchRequestDTO
            {
                MatchRequestId = matchRequest.MatchRequestId,
                MatchPostId = matchRequest.MatchPostId,
                RequestById = matchRequest.RequestById,
                RequestByTeamId = matchRequest.RequestByTeamId,
                Status = matchRequest.Status,
                RequestTime = matchRequest.RequestTime,
                DecisionTime = matchRequest.DecisionTime
            };
            return new ResponseDTO("Match request retrieved successfully.", 200, true, matchRequestDTO);
        }

        public async Task<ResponseDTO> GetMatchRequestsByMatchPostIdAsync(Guid matchPostId)
        {
            var matchRequests = await _unitOfWork.MatchRequestRepository.GetByMatchPostIdAsync(matchPostId);
            if (matchRequests == null || !matchRequests.Any())
            {
                return new ResponseDTO("No match requests found for the specified match post.", 404, false);
            }
            var matchRequestDTOs = matchRequests.Select(mr => new MatchRequestDTO
            {
                MatchRequestId = mr.MatchRequestId,
                MatchPostId = mr.MatchPostId,
                RequestById = mr.RequestById,
                RequestByTeamId = mr.RequestByTeamId,
                Status = mr.Status,
                RequestTime = mr.RequestTime,
                DecisionTime = mr.DecisionTime
            }).ToList();
            return new ResponseDTO("Match requests retrieved successfully.", 200, true, matchRequestDTOs);

        }

        public async Task<ResponseDTO> GetMatchRequestsByStatusAsync(string status)
        {
            var matchRequestStatus = Enum.TryParse<MatchRequestStatus>(status, true, out var parsedStatus) ? parsedStatus : MatchRequestStatus.IN_PROGRESS;
            var matchRequests = await _unitOfWork.MatchRequestRepository.GetByStatusAsync(matchRequestStatus);
            if (matchRequests == null || !matchRequests.Any())
            {
                return new ResponseDTO("No match requests found for the specified status.", 404, false);
            }
            var matchRequestDTOs = matchRequests.Select(mr => new MatchRequestDTO
            {
                MatchRequestId = mr.MatchRequestId,
                MatchPostId = mr.MatchPostId,
                RequestById = mr.RequestById,
                RequestByTeamId = mr.RequestByTeamId,
                Status = mr.Status,
                RequestTime = mr.RequestTime,
                DecisionTime = mr.DecisionTime
            }).ToList();
            return new ResponseDTO("Match requests retrieved successfully.", 200, true, matchRequestDTOs);

        }

        public async Task<ResponseDTO> GetMatchRequestsByTeamIdAsync(Guid teamId)
        {
            var matchRequests =await _unitOfWork.MatchRequestRepository.GetByTeamIdAsync(teamId);
            if (matchRequests == null || !matchRequests.Any())
            {
                return new ResponseDTO("No match requests found for the specified team.", 404, false);
            }
            var matchRequestDTOs = matchRequests.Select(mr => new MatchRequestDTO
            {
                MatchRequestId = mr.MatchRequestId,
                MatchPostId = mr.MatchPostId,
                RequestById = mr.RequestById,
                RequestByTeamId = mr.RequestByTeamId,
                Status = mr.Status,
                RequestTime = mr.RequestTime,
                DecisionTime = mr.DecisionTime
            }).ToList();
            return new ResponseDTO("Match requests retrieved successfully.", 200, true, matchRequestDTOs);

        }

        public async Task<ResponseDTO> GetMatchRequestsByUserIdAsync(Guid userId)
        {
            var matchRequests = await _unitOfWork.MatchRequestRepository.GetByUserIdAsync(userId);
            if (matchRequests == null || !matchRequests.Any())
            {
                return new ResponseDTO("No match requests found for the specified user.", 404, false);
            }
            var matchRequestDTOs = matchRequests.Select(mr => new MatchRequestDTO
            {
                MatchRequestId = mr.MatchRequestId,
                MatchPostId = mr.MatchPostId,
                RequestById = mr.RequestById,
                RequestByTeamId = mr.RequestByTeamId,
                Status = mr.Status,
                RequestTime = mr.RequestTime,
                DecisionTime = mr.DecisionTime
            }).ToList();
            return new ResponseDTO("Match requests retrieved successfully.", 200, true, matchRequestDTOs);

        }

        public async Task<ResponseDTO> GetMatchRequestsByUserId()
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
            {
                return new ResponseDTO("User not authenticated.", 401, false);
            }
            var matchRequests = await _unitOfWork.MatchRequestRepository.GetByUserIdAsync(userId);
            if (matchRequests == null || !matchRequests.Any())
            {
                return new ResponseDTO("No match requests found for the authenticated user.", 404, false);
            }
            var matchRequestDTOs = matchRequests.Select(mr => new MatchRequestDTO
            {
                MatchRequestId = mr.MatchRequestId,
                MatchPostId = mr.MatchPostId,
                RequestById = mr.RequestById,
                RequestByTeamId = mr.RequestByTeamId,
                Status = mr.Status,
                RequestTime = mr.RequestTime,
                DecisionTime = mr.DecisionTime
            }).ToList();
            return new ResponseDTO("Match requests retrieved successfully.", 200, true, matchRequestDTOs);
        }

        public async Task<ResponseDTO> UpdateMatchRequestAsync(UpdateMatchRequestDTO updateMatchRequestDTO)
        {
            var matchRequest = await _unitOfWork.MatchRequestRepository.GetByIdAsync(updateMatchRequestDTO.MatchRequestId);
            if (matchRequest == null)
            {
                return new ResponseDTO("Match request not found.", 404, false);
            }
            if (updateMatchRequestDTO.MatchPost != null)
            {
                matchRequest.MatchPostId = updateMatchRequestDTO.MatchPost.Value;
            }
            if (updateMatchRequestDTO.RequestByTeamId != null)
            {
                matchRequest.RequestByTeamId = updateMatchRequestDTO.RequestByTeamId.Value;
            }
            try
            {
                _unitOfWork.MatchRequestRepository.UpdateAsync(matchRequest);
                _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error updating match request: {ex.Message}", 500, false);
            }
            return new ResponseDTO("Match request updated successfully.", 200, true);
        }
    }
}
