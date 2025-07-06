using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMA.Common.DTOs;

namespace FMA.BLL.Services.Interfaces
{
    public interface IPitchService
    {
        Task<ResponseDTO> CreatePitchAsync(CreatePitchDTO createPitchDto);
        Task<ResponseDTO> UpdatePitchAsync(UpdatePitchDTO updatePitchDto);
        Task<ResponseDTO> DeletePitchAsync(Guid pitchId);
        Task<ResponseDTO> GetPitchByIdAsync(Guid pitchId);
        Task<ResponseDTO> GetAllPitchesAsync();

    }
}
