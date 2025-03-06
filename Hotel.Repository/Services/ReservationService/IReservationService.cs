using Hotel.Core.Dtos.Reservation;
using Hotel.Core.Entities.Reservation;

namespace Hotel.Repository.Services.ReservationService
{
    public interface IReservationService
    {
        
        Task<IEnumerable<ReservationDto>> GetAllReservationsAsync();
        Task<Reservation> AddReservationAsync(Reservation reservation);
        Task<ReservationDto> GetReservationByIdAsync(int id);
        Task<ReservationDto> CreateReservationAsync(CreateReservationDto reservationDto);
        Task<bool> UpdateReservationAsync(int id, UpdateReservationDto reservationDto);
        Task<bool> CancelReservationAsync(int id);
        Task<bool> DeleteReservationAsync(int id);
        Task<ReservationDto> GetReservationWithCustomerAsync(CustomerViewModel customer);
    }
}
