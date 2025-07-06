using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMA.Common.DTOs;

namespace FMA.BLL.Services.Interfaces
{
    public interface ITeamMemberService
    {
        Task<ResponseDTO> AddMemberToTeamAsync(Guid teamId, Guid userId);
        Task<ResponseDTO> RemoveMemberFromTeamAsync(Guid teamId, Guid userId);
        Task<ResponseDTO> GetTeamMembersAsync(Guid teamId);
        Task<ResponseDTO> GetTeamMemberDetailAsync(Guid userId);
        Task<ResponseDTO> GetAllTeamMember();
    }
}
