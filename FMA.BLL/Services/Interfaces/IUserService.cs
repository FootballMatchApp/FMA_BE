using FMA.Common.DTOs;
using FMA.DAL.Entities;

namespace FMA.BLL.Services.Interfaces;

public interface IUserService 
{
    Task<ResponseDTO> GetProfileAsync();
    Task<ResponseDTO> UpdateProfileAsync(UpdateUserDTO updateUserDTO);

}