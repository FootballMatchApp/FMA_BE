using FMA.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMA.BLL.Services.Interfaces
{
    public interface IBookingService
    {
        Task<ResponseDTO> GetAllAsync();
        Task<ResponseDTO> CreateAsync(CreateBookingDTO dto);
        Task<ResponseDTO> UpdateAsync(UpdateBookingDTO dto);
        Task<ResponseDTO> DeleteAsync(Guid id);

    }
}
