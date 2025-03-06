using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Core.Data.Configuration;
using Hotel.Core.Dtos.Offer;
using Hotel_Reservation_System.ViewModels;

namespace Hotel.Repository.Services.OfferService
{
    public interface IOfferService
    {
        Task<ApiResponse<OfferViewModel>> CreateOfferAsync(CreateOfferDto offerDto);
        Task<ApiResponse<bool>> DeleteOfferAsync(int id);
        Task<ApiResponse<IEnumerable<OfferViewModel>>> GetAllOffersAsync();
        Task<ApiResponse<OfferViewModel>> GetOfferByIdAsync(int id);
        Task<ApiResponse<bool>> UpdateOfferAsync(int id, UpdateOfferDto offerDto);
    }
}
