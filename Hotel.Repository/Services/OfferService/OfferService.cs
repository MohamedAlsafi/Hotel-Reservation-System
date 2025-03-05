using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.Core.Dtos.Offer;
using Hotel.Core.Entities.OfferModel;
using Hotel.Repository.IGenericRepository;
using Hotel.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Repository.Services.OfferService
{
    public class OfferService : IOfferService
    {
        private readonly IGenericRepository<Offer> _offerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OfferService(IGenericRepository<Offer> offerRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _offerRepository = offerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OfferDto>> GetAllOffersAsync()
        {
            var offers = await _offerRepository.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<OfferDto>>(offers);
        }

        public async Task<OfferDto> GetOfferByIdAsync(int id)
        {
            var offer = await _offerRepository.GetByIdAsync(id);
            return offer == null ? null : _mapper.Map<OfferDto>(offer);
        }

        public async Task<OfferDto> CreateOfferAsync(CreateOfferDto offerDto)
        {
            var offer = _mapper.Map<Offer>(offerDto);
            await _offerRepository.AddAsync(offer);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<OfferDto>(offer);
        }

        public async Task<bool> UpdateOfferAsync(int id, CreateOfferDto offerDto)
        {
            var offer = await _offerRepository.GetByIdAsync(id);
            if (offer == null) return false;

            _mapper.Map(offerDto, offer);
            _offerRepository.UpdateInclude(offer, nameof(Offer.StartDate), nameof(Offer.EndDate), nameof(Offer.DiscountPercentage));
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteOfferAsync(int id)
        {
            var offer = await _offerRepository.GetByIdAsync(id);
            if (offer == null) return false;

            _offerRepository.SoftDelete(offer);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
