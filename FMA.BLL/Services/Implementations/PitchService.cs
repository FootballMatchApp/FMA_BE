using FMA.BLL.Services.Interfaces;
using FMA.BLL.Utilities;
using FMA.Common.DTOs;
using FMA.DAL.Entities;
using FMA.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace FMA.BLL.Services.Implementations
{
    public class PitchService : IPitchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserUtility _userUtility;

        public PitchService(IUnitOfWork unitOfWork, UserUtility userUtility)
        {
            _unitOfWork = unitOfWork;
            _userUtility = userUtility;
        }

        public async Task<ResponseDTO> CreatePitchAsync(CreatePitchDTO createPitchDto)
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
            {
                return new ResponseDTO("User not authenticated", 401, false);
            }

            var pitch = new Pitch
            {
                PitchId = Guid.NewGuid(),
                OwnerId = userId,
                Name = createPitchDto.Name,
                ContactNumber = createPitchDto.ContactNumber,
                Location = createPitchDto.Location,
                Latitude = createPitchDto.Latitude,
                Longitude = createPitchDto.Longitude,
                PricePerHour = createPitchDto.PricePerHour,
                Status = FMA.Common.Enums.PitchStatus.AVAILABLE
            };

            try
            {
                await _unitOfWork.PitchRepository.AddAsync(pitch);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error creating pitch: {ex.Message}", 500, false);
            }

            return new ResponseDTO("Pitch created successfully", 201, true);
        }

        public async Task<ResponseDTO> DeletePitchAsync(Guid pitchId)
        {
            var pitch = await _unitOfWork.PitchRepository.GetByIdAsync(pitchId);
            if (pitch == null)
            {
                return new ResponseDTO("Pitch not found", 404, false);
            }

            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty || pitch.OwnerId != userId)
            {
                return new ResponseDTO("Unauthorized to delete this pitch", 403, false);
            }

            try
            {
                _unitOfWork.PitchRepository.Delete(pitch);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error deleting pitch: {ex.Message}", 500, false);
            }

            return new ResponseDTO("Pitch deleted successfully", 200, true);
        }

        public async Task<ResponseDTO> GetAllPitchesAsync()
        {
            var pitches = await _unitOfWork.PitchRepository.GetAll().ToListAsync();
            if (pitches == null || !pitches.Any())
            {
                return new ResponseDTO("No pitches found", 404, false);
            }

            var pitchDtos = pitches.Select(pitch => new GetPitchDTO
            {
                PitchId = pitch.PitchId,
                Name = pitch.Name,
                ContactNumber = pitch.ContactNumber,
                Location = pitch.Location,
                Latitude = pitch.Latitude,
                Longitude = pitch.Longitude,
                PricePerHour = pitch.PricePerHour,
                Status = pitch.Status
            }).ToList();

            return new ResponseDTO("Success", 200, true, pitchDtos);
        }

        public async Task<ResponseDTO> GetPitchByIdAsync(Guid pitchId)
        {
            var pitch = await _unitOfWork.PitchRepository.GetByIdAsync(pitchId);
            if (pitch == null)
            {
                return new ResponseDTO("Pitch not found", 404, false);
            }

            var pitchDto = new GetPitchDTO
            {
                PitchId = pitch.PitchId,
                Name = pitch.Name,
                ContactNumber = pitch.ContactNumber,
                Location = pitch.Location,
                Latitude = pitch.Latitude,
                Longitude = pitch.Longitude,
                PricePerHour = pitch.PricePerHour,
                Status = pitch.Status
            };

            return new ResponseDTO("Success", 200, true, pitchDto);
        }

        public async Task<ResponseDTO> UpdatePitchAsync(UpdatePitchDTO updatePitchDto)
        {
            var pitch = await _unitOfWork.PitchRepository.GetByIdAsync(updatePitchDto.PitchId);
            if (pitch == null)
            {
                return new ResponseDTO("Pitch not found", 404, false);
            }

            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty || pitch.OwnerId != userId)
            {
                return new ResponseDTO("Unauthorized to update this pitch", 403, false);
            }

            pitch.Name = updatePitchDto.Name;
            pitch.ContactNumber = updatePitchDto.ContactNumber;
            pitch.Location = updatePitchDto.Location;
            pitch.Latitude = updatePitchDto.Latitude;
            pitch.Longitude = updatePitchDto.Longitude;
            pitch.PricePerHour = updatePitchDto.PricePerHour;

            try
            {
                await _unitOfWork.PitchRepository.UpdateAsync(pitch);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error updating pitch: {ex.Message}", 500, false);
            }

            return new ResponseDTO("Pitch updated successfully", 200, true);
        }
    }
} 