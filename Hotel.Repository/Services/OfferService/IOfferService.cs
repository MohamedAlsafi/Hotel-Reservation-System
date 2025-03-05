using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Core.Dtos.Offer;

namespace Hotel.Repository.Services.OfferService
{
    public interface IOfferService
    {
        Task<IEnumerable<OfferDto>> GetAllOffersAsync();
        Task<OfferDto> GetOfferByIdAsync(int id);
        Task<OfferDto> CreateOfferAsync(CreateOfferDto offerDto);
        Task<bool> UpdateOfferAsync(int id, CreateOfferDto offerDto);
        Task<bool> DeleteOfferAsync(int id);
    }
}
