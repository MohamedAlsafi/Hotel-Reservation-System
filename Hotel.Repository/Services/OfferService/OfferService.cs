﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.Core.Data.Configuration;
using Hotel.Core.Dtos.Offer;
using Hotel.Core.Entities.OfferModel;
using Hotel.Repository.IGenericRepository;
using Hotel.Repository.UnitOfWork;
using Hotel_Reservation_System.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Repository.Services.OfferService
{
    public class OfferService : IOfferService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OfferService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseViewModel<OfferViewModel>> CreateOfferAsync(CreateOfferDto offerDto)
        {
            var offer = _mapper.Map<Offer>(offerDto);
            await _unitOfWork.Repository<Offer>().AddAsync(offer);
            await _unitOfWork.SaveChangesAsync();

            var offerVm = _mapper.Map<OfferViewModel>(offer);
            return new ResponseViewModel<OfferViewModel>(true, "Offer created successfully", offerVm);
        }

        public async Task<ResponseViewModel<bool>> DeleteOfferAsync(int id)
        {
            var offer = await _unitOfWork.Repository<Offer>().GetByIdAsync(id);
            if (offer == null) return new ResponseViewModel<bool>(false, "Offer not found", false);

            _unitOfWork.Repository<Offer>().HardDelete(offer);
            await _unitOfWork.SaveChangesAsync();
            return new ResponseViewModel<bool>(true, "Offer deleted successfully", true);
        }

        public async Task<ResponseViewModel<IEnumerable<OfferViewModel>>> GetAllOffersAsync()
        {
            var offers = await _unitOfWork.Repository<Offer>().GetAll().Where(o=>o.IsRoomAvailable).ToListAsync();
            var offerVms = _mapper.Map<IEnumerable<OfferViewModel>>(offers);
            return new ResponseViewModel<IEnumerable<OfferViewModel>>(true, "Offers retrieved successfully", offerVms);
        }

        public async Task<ResponseViewModel<OfferViewModel>> GetOfferByIdAsync(int id)
        {
            var offer = await _unitOfWork.Repository<Offer>().GetByIdAsync(id);
            if (offer == null) return new ResponseViewModel<OfferViewModel>(false, "Offer not found", null);

            var offerVm = _mapper.Map<OfferViewModel>(offer);
            return new ResponseViewModel<OfferViewModel>(true, "Offer retrieved successfully", offerVm);
        }

        public async Task<ResponseViewModel<bool>> UpdateOfferAsync(int id, UpdateOfferDto offerDto)
        {
            if (offerDto is null) return new ResponseViewModel<bool>(false, "Can't update offer", false);
            var offer = await _unitOfWork.Repository<Offer>().GetByIdAsync(id);
            if (offer is null) return new ResponseViewModel<bool>(false, "Offer not found", false);

            _mapper.Map(offerDto, offer);
             await _unitOfWork.Repository<Offer>().UpdateInclude(offer);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseViewModel<bool>(true, "Offer updated successfully", true);
        }
    }
}
