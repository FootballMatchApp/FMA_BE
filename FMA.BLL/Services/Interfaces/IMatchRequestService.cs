using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMA.Common.DTOs;

namespace FMA.BLL.Services.Interfaces
{
    public interface IMatchRequestService
    {
        Task<ResponseDTO> CreateMatchRequestAsync(CreateMatchRequestDTO createMatchRequestDTO);
        Task<ResponseDTO> UpdateMatchRequestAsync(UpdateMatchRequestDTO updateMatchRequestDTO);
        Task<ResponseDTO> DeleteMatchRequestAsync(Guid matchRequestId);
        Task<ResponseDTO> GetMatchRequestByIdAsync(Guid matchRequestId);
        Task<ResponseDTO> GetMatchRequestsByMatchPostIdAsync(Guid matchPostId);
        Task<ResponseDTO> GetMatchRequestsByUserIdAsync(Guid userId);
        Task<ResponseDTO> GetMatchRequestsByUserId();
        Task<ResponseDTO> GetMatchRequestsByTeamIdAsync(Guid teamId);
        Task<ResponseDTO> GetAllMatchRequestsAsync();
        Task<ResponseDTO> AcceptMatchRequestAsync(Guid matchRequestId);
        Task<ResponseDTO> CancelMatchRequestAsync(Guid matchRequestId);
        Task<ResponseDTO> GetMatchRequestsByStatusAsync(string status);

    }
}
