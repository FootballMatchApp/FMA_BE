using FMA.BLL.Services.Interfaces;
using FMA.BLL.Utilities;
using FMA.Common.DTOs;
using FMA.Common.Enums;
using FMA.DAL.Entities;
using FMA.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMA.BLL.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserUtility _userUtility;
        private readonly IEmailService _emailService;

        public BookingService(IUnitOfWork unitOfWork, UserUtility userUtility, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _userUtility = userUtility;
            _emailService = emailService;

        }

        public async Task<ResponseDTO> GetAllAsync()
        {
            try
            {
                var bookings = await _unitOfWork.BookingRepository.GetAllAsync();
                if (bookings == null || !bookings.Any())
                {
                    return new ResponseDTO("There are no bookings", 404, false);
                }

                var result = bookings.Select(b => new BookingDTO
                {
                    BookingId = b.BookingId,
                    PitchId = b.PitchId,
                    MatchPostId = b.MatchPostId,
                    MatchRequestId = b.MatchRequestId,
                    Duration = b.Duration,
                    BookingTime = b.BookingTime,
                    Status = b.Status.ToString()
                }).ToList();

                return new ResponseDTO("Get all bookings successfully", 200, true, result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error getting bookings: {ex.Message}", 500, false);
            }
        }

        public async Task<ResponseDTO> CreateAsync(CreateBookingDTO dto)
        {
            try
            {
                var newBooking = new Booking
                {
                    BookingId = Guid.NewGuid(),
                    PitchId = dto.PitchId,
                    MatchPostId = dto.MatchPostId,
                    MatchRequestId = dto.MatchRequestId,
                    Duration = dto.Duration,
                    BookingTime = dto.BookingTime,
                    Status = BookingStatus.PENDING
                };

                await _unitOfWork.BookingRepository.AddAsync(newBooking);
                await _unitOfWork.SaveAsync();
                // Optionally, send a confirmation email or notification
                //string bookerName, string bookerEmail, Guid pitchId, DateTime bookingTime

                // Notify the match post creator
                var matchPost = await _unitOfWork.MatchPostRepository.GetByIdAsync(dto.MatchPostId);
                if (matchPost != null)
                {
                    var booker = await _unitOfWork.UserRepository.GetByIdAsync(matchPost.PostById);
                    if (booker != null)
                    {
                        await _emailService.SendBookingCreatedAsync(booker.Username, booker.Email, dto.PitchId, dto.BookingTime);
                    }
                }
                // Notify the request 
                var matchRequest = await _unitOfWork.MatchRequestRepository.GetByIdAsync(dto.MatchRequestId);
                if (matchRequest != null)
                {
                    var requester = await _unitOfWork.UserRepository.GetByIdAsync(matchRequest.MatchRequestId);
                    if (requester != null)
                    {
                        await _emailService.SendBookingCreatedAsync(requester.Username, requester.Email, dto.PitchId, dto.BookingTime);
                    }
                }

                // Notify the pitch owner
                var pitch = await _unitOfWork.PitchRepository.GetByIdAsync(dto.PitchId);
                if (pitch != null)
                {
                    var pitchOwner = await _unitOfWork.UserRepository.GetByIdAsync(pitch.OwnerId);
                    if (pitchOwner != null)
                    {
                        await _emailService.SendBookingNotificationToStationAsync(pitchOwner.Username, pitchOwner.Email, newBooking.BookingId, dto.PitchId, dto.BookingTime);
                    }
                }


                return new ResponseDTO("Booking created successfully", 201, true, newBooking);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error creating booking: {ex.Message}", 500, false);
            }
        }
        public async Task<ResponseDTO> UpdateAsync(UpdateBookingDTO dto)
        {
            try
            {
                var booking = await _unitOfWork.BookingRepository.GetByIdAsync(dto.BookingId);
                if (booking == null)
                {
                    return new ResponseDTO("Booking not found", 404, false);
                }

                booking.PitchId = dto.PitchId;
                booking.MatchPostId = dto.MatchPostId;
                booking.MatchRequestId = dto.MatchRequestId;
                booking.Duration = dto.Duration;
                booking.BookingTime = dto.BookingTime;

                if (Enum.TryParse(dto.Status, out BookingStatus parsedStatus))
                {
                    booking.Status = parsedStatus;
                }

                _unitOfWork.BookingRepository.UpdateAsync(booking);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO("Booking updated successfully", 200, true, booking);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error updating booking: {ex.Message}", 500, false);
            }
        }

        public async Task<ResponseDTO> DeleteAsync(Guid id)
        {
            try
            {
                var booking = await _unitOfWork.BookingRepository.GetByIdAsync(id);
                if (booking == null)
                {
                    return new ResponseDTO("Booking not found", 404, false);
                }

                _unitOfWork.BookingRepository.Delete(booking);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO("Booking deleted successfully", 200, true);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error deleting booking: {ex.Message}", 500, false);
            }
        }

    }
}
