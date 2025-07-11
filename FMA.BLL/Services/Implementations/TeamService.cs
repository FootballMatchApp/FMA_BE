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
    public class TeamService : ITeamService
    { 
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserUtility _userUtility;
        public TeamService(IUnitOfWork unitOfWork, UserUtility userUtility)
        {
            _unitOfWork = unitOfWork;
            _userUtility = userUtility;
        }
        public async Task<ResponseDTO> CreateTeamAsync(CreateTeamDTO createTeamDto)
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == null)
            {
                return new ResponseDTO("User not authenticated", 401, false);
            }
            var team = new Team
            {
                TeamId = Guid.NewGuid(), // Generate a new unique ID for the team
                TeamName = createTeamDto.TeamName,
                Description = createTeamDto.Description,
                CreatedById = userId, // Assuming userId is a Guid
                
            };
            try
            {
                await _unitOfWork.TeamRepository.AddAsync(team);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error creating team: {ex.Message}", 500, false);
            }
            return new ResponseDTO("Team created successfully", 201, true);
        }

        public async Task<ResponseDTO> DeleteTeamAsync(Guid teamId)
        {
            var team = await _unitOfWork.TeamRepository.GetByIdAsync(teamId);
            if (team == null)
            {
                return new ResponseDTO("Team not found", 404, false);
            }
            try
            {
                _unitOfWork.TeamRepository.Delete(team);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error deleting team: {ex.Message}", 500, false);
            }
            return new ResponseDTO("Team deleted successfully", 200, true);
        }

        public async Task<ResponseDTO> GetAllTeamsAsync()
        {
            var teams = _unitOfWork.TeamRepository.GetAll();
            if (teams == null || !teams.Any())
            {
                return new ResponseDTO("No teams found", 404, false);
            }
            var teamDtos = teams.Select(team => new GetTeamDTO
            {
                TeamId = team.TeamId,
                TeamName = team.TeamName,
                Description = team.Description,

            }).ToList();
            return new ResponseDTO("Success", 200, true, teamDtos);
        }

        public async Task<ResponseDTO> GetTeamByIdAsync(Guid teamId)
        {
            var team = await _unitOfWork.TeamRepository.GetByIdAsync(teamId);
            if (team == null)
            {
                return new ResponseDTO("Team not found", 404, false);
            }
            var teamDto = new GetTeamDTO
            {
                TeamId = team.TeamId,
                TeamName = team.TeamName,
                Description = team.Description,
            };
            return new ResponseDTO("Success", 200, true, teamDto);
        }

        public async Task<ResponseDTO> UpdateTeamAsync(UpdateTeamDTO updateTeamDto)
        {
            var team = await _unitOfWork.TeamRepository.GetByIdAsync(updateTeamDto.TeamId);
            if (team == null)
            {
                return new ResponseDTO("Team not found", 404, false);
            }
            team.TeamName = updateTeamDto.TeamName;
            team.Description = updateTeamDto.Description;
            try
            {
                _unitOfWork.TeamRepository.UpdateAsync(team);
                _unitOfWork.SaveChangeAsync().Wait();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error updating team: {ex.Message}", 500, false);
            }
            return new ResponseDTO("Team updated successfully", 200, true);
        }
    }
}
