using AutoMapper;
using FMA.BLL.Services.Interfaces;
using FMA.BLL.Utilities;
using FMA.Common.DTOs;
using FMA.DAL.Entities;
using FMA.DAL.Repositories.Interfaces;
using FMA.DAL.UnitOfWork;

namespace FMA.BLL.Services.Implementations;

public class UserService : IUserService
{ 
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserUtility _userUtility;
    public UserService (IUnitOfWork unitOfWork, UserUtility userUtility)
    {
        _unitOfWork = unitOfWork;
        _userUtility = userUtility;
    }

    public async Task<ResponseDTO> GetProfileAsync()
    {
        var userId =  _userUtility.GetUserIdFromToken();
        if (userId == Guid.Empty)
        {
            return new ResponseDTO("User not found", 404, false);
        }
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return new ResponseDTO("User not found", 404, false);
        }

        // Lấy TeamMember kèm User và Team
        var teamMember = await _unitOfWork.TeamMemberRepository.GetTeamMemberDetailAsync(userId);
        
        

        var team = teamMember?.Team;

        if (user == null)
        {
            return new ResponseDTO("User not found", 404, false);
        }

        var userProfileDto = new UserProfileDTO
        {
            UserId = user.UserId,
            FullName = user.Username,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Position = teamMember?.Position,
            JoinDate = teamMember?.JoinDate,
            TeamId = team?.TeamId ,
            TeamName = team?.TeamName,
            TeamDescription = team?.Description
        };

        return new ResponseDTO("Success", 200, true, userProfileDto);
    }

}