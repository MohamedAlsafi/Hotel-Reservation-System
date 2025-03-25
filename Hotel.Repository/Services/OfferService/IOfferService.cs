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
        Task<ResponseViewModel<OfferViewModel>> CreateOfferAsync(CreateOfferDto offerDto);
        Task<ResponseViewModel<bool>> DeleteOfferAsync(int id);
        Task<ResponseViewModel<IEnumerable<OfferViewModel>>> GetAllOffersAsync();
        Task<ResponseViewModel<OfferViewModel>> GetOfferByIdAsync(int id);
        Task<ResponseViewModel<bool>> UpdateOfferAsync(int id, UpdateOfferDto offerDto);
    }
}
