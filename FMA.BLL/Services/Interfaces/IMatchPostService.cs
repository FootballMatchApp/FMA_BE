using FMA.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMA.BLL.Services.Interfaces
{
    public interface IMatchPostService
    {
        Task<ResponseDTO> GetAllAsync();

        Task<ResponseDTO> CreateAsync(CreateMatchPostDTO dto, Guid userId);

    }

}
