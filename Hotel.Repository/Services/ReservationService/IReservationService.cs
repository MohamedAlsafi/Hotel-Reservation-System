﻿using Hotel.Core.Dtos.FeedbackDtos;
using Hotel.Core.Dtos.Reservation;
using Hotel.Repository.ViewModels;

namespace Hotel.Repository.Services.ReservationService
{
    public interface IReservationService
    {

        Task<ReservationViewModel> CreateReservationAsync(CreateReservationDto reservationDto, int customerId);

        Task<ResponseViewModel<ReservationViewModel>> CancelReservationAsync(int id);
        Task<ResponseViewModel<ReservationViewModel>> GetReservationByIdAsync(int id);
        Task<ResponseViewModel<ReservationViewModel>> UpdateReservationAsync( UpdateReservationDto reservationDto);
    }
   
       
    }






