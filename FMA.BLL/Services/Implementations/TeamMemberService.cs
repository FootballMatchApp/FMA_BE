using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMA.BLL.Services.Interfaces;
using FMA.BLL.Utilities;
using FMA.Common.DTOs;
using FMA.DAL.Entities;
using FMA.DAL.UnitOfWork;

namespace FMA.BLL.Services.Implementations
{
    public class TeamMemberService : ITeamMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserUtility _userUtility;
        public TeamMemberService (IUnitOfWork unitOfWork, UserUtility userUtility)
        {
            _unitOfWork = unitOfWork;
            _userUtility = userUtility;
        }
        public async Task<ResponseDTO> AddMemberToTeamAsync(Guid teamId, Guid userId, string? position)
        {
            var team = await _unitOfWork.TeamRepository.GetByIdAsync(teamId);
            if (team == null)
            {
                return new ResponseDTO ("Team not found", 400 ,false);
            }
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return new ResponseDTO("User not found", 400, false);
            }
            var existingMember = await _unitOfWork.TeamMemberRepository.GetByTeamAndUserAsync(teamId, userId);
            if (existingMember != null)
            {
                return new ResponseDTO("User is already a member of the team", 400, false);
            }
            var teamMember = new TeamMember
            {
                TeamMemberId = Guid.NewGuid(), // Generate a new unique ID for the team member
                TeamId = teamId,
                UserId = userId,
                Position = position ?? "Member", // Default position if not provided
                JoinDate = DateTime.UtcNow
            };
            try
            {
                await _unitOfWork.TeamMemberRepository.AddAsync(teamMember);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error saving refresh token: {ex.Message}", 500, false);
            }

            return new ResponseDTO ("Add member successfully" , 200, true);


        }

        public async Task<ResponseDTO> GetAllTeamMember()
        {
            var members = _unitOfWork.TeamMemberRepository.GetAll();
        
            if (members == null || !members.Any())
            {
                return new ResponseDTO("No team members found", 404, false);
            }

            var result = members.Select(m => new TeamMemberDTO
            {
                TeamMemberId = m.TeamMemberId,
                TeamId = m.TeamId,
                UserId = m.UserId,
                Position = m.Position,
                JoinDate = m.JoinDate,
                PhoneNumber = m.User.PhoneNumber ?? "N/A", // Assuming User entity has PhoneNumber
                Address = m.User.Address ?? "N/A" // Assuming User entity has Address
            });

            return new ResponseDTO("Success", 200, true, result);
        }


        public async Task<ResponseDTO> GetTeamMemberDetailAsync(Guid userId)
        {
            var member = await _unitOfWork.TeamMemberRepository.GetTeamMemberDetailAsync(userId);

            if (member == null)
                return new ResponseDTO("Team member not found", 404, false);

            var result = new TeamMemberDTO
            {
                TeamMemberId = member.TeamMemberId,
                TeamId = member.TeamId,
                UserId = member.UserId,
                Position = member.Position,
                JoinDate = member.JoinDate,
                PhoneNumber = member.User.PhoneNumber ?? "N/A", // Assuming User entity has PhoneNumber
                Address = member.User.Address ?? "N/A" // Assuming User entity has Address
            };

            return new ResponseDTO("Success", 200, true, result);
        }


        public async Task<ResponseDTO> GetTeamMembersAsync(Guid teamId)
        {
            var members = await _unitOfWork.TeamMemberRepository.GetMembersByTeamIdAsync(teamId);

            if (members == null || !members.Any())
                return new ResponseDTO("No members found in this team", 404, false);

            var result = members.Select(m => new TeamMemberDTO
            {
                TeamMemberId = m.TeamMemberId,
                TeamId = m.TeamId,
                UserId = m.UserId,
                Position = m.Position,
                JoinDate = m.JoinDate,
                PhoneNumber = m.User.PhoneNumber ?? "N/A", // Assuming User entity has PhoneNumber
                Address = m.User.Address ?? "N/A" // Assuming User entity has Address
            });

            return new ResponseDTO("Success", 200, true, result);
        }


        public async Task<ResponseDTO> RemoveMemberFromTeamAsync(Guid teamId, Guid userId)
        {
            var member = await _unitOfWork.TeamMemberRepository.GetByTeamAndUserAsync(teamId, userId);
            if (member == null)
                return new ResponseDTO("Member not found in the team", 404, false);

            try
            {
                await _unitOfWork.TeamMemberRepository.DeleteAsync(member.TeamMemberId);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error removing member: {ex.Message}", 500, false);
            }

            return new ResponseDTO("Member removed successfully", 200, true);
        }
    
    }
}
