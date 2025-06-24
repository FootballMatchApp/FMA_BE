using AutoMapper;
using FMA.Common.DTOs;
using FMA.DAL.Entities;

namespace FMA.BLL.Mappers;

public class AutoMapperProfile  : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserDTO, User>().ForMember(pw =>pw.PasswordHash, o => o.Ignore());
    }
    
}