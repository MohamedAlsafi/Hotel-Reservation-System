using Hotel.Core.Dtos.Reservation;
using Hotel.Core.Entities.Reservation;
using Hotel_Reservation_System.ViewModels;
using System;


namespace Hotel.Repository.Services.Payment
{
    public interface IPaymentService
    {
        Task<ReservationDto> MakePaymentOrUpdateAsync(int customerId,int ReservationId);
         
    }
}
