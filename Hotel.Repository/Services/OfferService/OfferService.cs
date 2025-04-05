using AutoMapper;
using Hotel.Core.Dtos.Offer;
using Hotel.Core.Entities.HotelStaffs;
using Hotel.Core.Entities.OfferModel;
using Hotel.Repository.Services.OfferService;
using Hotel.Repository.UnitOfWork;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
public class OfferService : IOfferService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OfferService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OfferDto> CreateOfferAsync(CreateOfferDto offerDto)
    {
        ValidateOfferData(offerDto.Title, offerDto.DiscountPercentage, offerDto.StartDate, offerDto.EndDate);

        var offer = _mapper.Map<Offer>(offerDto);

        if (offerDto.CreatedByStaff.HasValue)
        {
            var staff = await _unitOfWork.Repository<HotelStaff>()
                                .GetByIdAsync(offerDto.CreatedByStaff.Value);
            if (staff == null) throw new ArgumentException("Staff member not found.");
            offer.CreatedByStaff = staff;
        }

        await _unitOfWork.Repository<Offer>().AddAsync(offer);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<OfferDto>(offer);
    }

    public async Task<bool> DeleteOfferAsync(int id)
    {
        var offer = await _unitOfWork.Repository<Offer>().GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Offer {id} not found");

        _unitOfWork.Repository<Offer>().HardDelete(offer);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<OfferListingDto>> GetActiveOffersAsync()
    {
        return await _unitOfWork.Repository<Offer>()
            .GetAll()
            .Where(o => o.StartDate <= DateTime.UtcNow && o.EndDate >= DateTime.UtcNow)
            .ProjectTo<OfferListingDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<OfferListingDto> GetOfferByIdAsync(int id)
    {
        var offer = await _unitOfWork.Repository<Offer>().GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Offer with ID {id} not found");

        if (!IsOfferActive(offer))
            throw new InvalidOperationException($"Offer {id} is not currently active");

        return _mapper.Map<OfferListingDto>(offer);
    }

    public async Task<OfferDto> GetOfferDetailsByIdAsync(int id)
    {
        var offer = await _unitOfWork.Repository<Offer>().GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Offer with ID {id} not found");

        return _mapper.Map<OfferDto>(offer);
    }

    public async Task<OfferDto> UpdateOfferAsync( UpdateOfferDto dto)
    {
        ValidateOfferData(dto.Title, dto.DiscountPercentage, dto.StartDate, dto.EndDate);

        var offer = await _unitOfWork.Repository<Offer>().GetByIdAsync(dto.Id)
            ?? throw new KeyNotFoundException($"Offer {dto.Id} not found");

        await _unitOfWork.Repository<Offer>().UpdateInclude(
            entity: offer,
            nameof(Offer.Title),
            nameof(Offer.Description),
            nameof(Offer.DiscountPercentage),
            nameof(Offer.StartDate),
            nameof(Offer.EndDate)
        );

        offer.Title = dto.Title;
        offer.Description = dto.Description;
        offer.DiscountPercentage = dto.DiscountPercentage;
        offer.StartDate = dto.StartDate;
        offer.EndDate = dto.EndDate;

        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<OfferDto>(offer);
    }

    private static bool IsOfferActive(Offer offer)
        => DateTime.UtcNow >= offer.StartDate && DateTime.UtcNow <= offer.EndDate;

    private void ValidateOfferData(string title, decimal discountPercentage, DateTime startDate, DateTime endDate)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(title))
            errors.Add("Title is required");

        if (discountPercentage <= 0 || discountPercentage > 100)
            errors.Add("Discount percentage must be between 0 and 100");

        if (startDate > endDate)
            errors.Add("Start date must be before end date");

        if (errors.Any())
            throw new ArgumentException(string.Join(". ", errors));
    }

}
