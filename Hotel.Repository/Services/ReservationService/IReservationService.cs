using Hotel.Core.Data.Configuration;
using Hotel.Core.Dtos.FeedbackDtos;
using Hotel.Core.Dtos.Reservation;
using Hotel.Core.Entities.Reservation;
using Hotel.Repository.ViewModels;

namespace Hotel.Repository.Services.ReservationService
{
    public interface IReservationService
    {

        Task<ApiResponse<ReservationViewModel>> CreateReservationAsync(CreateReservationDto reservationDto);
        Task<ApiResponse<bool>> CancelReservationAsync(int id);
        Task<ApiResponse<IEnumerable<ReservationViewModel>>> GetAllReservationsAsync();
        Task<ApiResponse<ReservationViewModel>> GetReservationByIdAsync(int id);
        Task<ApiResponse<bool>> UpdateReservationAsync(int id, UpdateReservationDto reservationDto);
        Task<ApiResponse<bool>> ProvideFeedbackAsync(FeedbackDto mappedFeedback);
    }
}
