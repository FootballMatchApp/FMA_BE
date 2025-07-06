using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMA.Common.DTOs;

namespace FMA.BLL.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDTO> LoginAsync(LoginDTO loginDto);
        Task<ResponseDTO> RegisterAsync(RegisterDTO registerDto);
        Task<ResponseDTO> LogoutAsync();
    }
}
