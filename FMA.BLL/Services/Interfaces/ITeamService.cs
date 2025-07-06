using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FMA.Common.DTOs;

namespace FMA.BLL.Services.Interfaces
{
    public interface ITeamService
    {
        Task<ResponseDTO> CreateTeamAsync(CreateTeamDTO createTeamDto);
        Task<ResponseDTO> UpdateTeamAsync(UpdateTeamDTO updateTeamDto);
        Task<ResponseDTO> DeleteTeamAsync(Guid teamId);
        Task<ResponseDTO> GetTeamByIdAsync(Guid teamId);
        Task<ResponseDTO> GetAllTeamsAsync();


    }
}
