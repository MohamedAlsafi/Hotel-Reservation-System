using AutoMapper;
using Hotel.Core.Data.Context;
using Hotel.Core.Dtos.FeedbackDtos;
using Hotel.Core.Dtos.Reservation;
using Hotel.Core.Entities.customer;
using Hotel.Repository.Services.OfferService.JWT_Token;
using Hotel.Repository.Services.ReservationService;
using Hotel.Repository.UnitOfWork;
using Hotel_Reservation_System.Error;
using Hotel_Reservation_System.ViewModels.Room;
using Hotel_Reservation_System.ViewModels.UserIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Hotel_Reservation_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(ITokenService tokenService,IReservationService reservationService,IMapper mapper,IUnitOfWork unitOfWork)
        {
           _tokenService = tokenService;
           _reservationService = reservationService;
           _mapper = mapper;
           _unitOfWork = unitOfWork;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO userDTO)
        {
            if (userDTO is null) return BadRequest(new ApiExcaptionResponse(400));

            var existingUser = await _unitOfWork.Repository<Customer>().GetByCriteriaAsync(x => x.Email == userDTO.Email);
            if (existingUser != null)
                return BadRequest(new ApiExcaptionResponse(400, "Email already exists"));

            var User = new Customer
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                Password = userDTO.Password,
                PhoneNumber = userDTO.PhoneNumber,
                UserName = userDTO.Email.Split('@')[0]

            };

            if (User is null) return BadRequest(new ApiExcaptionResponse(400));
            try
            {
                await _unitOfWork.Repository<Customer>().AddAsync(User);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception 
                Console.WriteLine($"Error saving changes: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
         
            var ResultDto = new UserDTO()
            {
                UserName = userDTO.Email.Split('@')[0],
                Email = userDTO.Email,
                Token = await _tokenService.GetTokenAsync(User, userDTO.Email)

            };

            return Ok(ResultDto);


        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {

            var User = await _unitOfWork.Repository<Customer>().GetByCriteriaAsync(x => x.Email == loginDTO.Email);
            if (User is null) return Unauthorized(new ApiExcaptionResponse(401));

            var Result = await _unitOfWork.Repository<Customer>().GetByCriteriaAsync(x => x.Email == loginDTO.Email && x.Password == loginDTO.Password);
            if (Result is null) return BadRequest(new ApiExcaptionResponse(401));
            var ResultDto = new UserDTO()
            {
                Email = User.Email,
                UserName = User.Email.Split('@')[0],
                Token = await _tokenService.GetTokenAsync(User, loginDTO.Email)

            };
            return Ok(ResultDto);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            if (Email is null) return Unauthorized(new ApiExcaptionResponse(401));
            var user = await _unitOfWork.Repository<Customer>().GetByCriteriaAsync(x => x.Email == Email);
            if (user is null) return Unauthorized(new ApiExcaptionResponse(401));
            var obj = new UserDTO()
            {
                UserName = user.UserName,
                Email = Email,
                Token = await _tokenService.GetTokenAsync(user,user.Email)
            };
            return Ok(obj);

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("SearchForRoom/{roomId}")]
        public async Task<ActionResult<RoomDto>> SearchForRoom(int roomId)
        {
            if (roomId <= 0) return BadRequest(new ApiExcaptionResponse(400, "Invalid RoomId"));
            var BookedRoom = await _reservationService.GetReservationByIdAsync(roomId);
            if (BookedRoom is null)
            {
                var roomDto = new RoomDto
                {
                    Id = BookedRoom.RoomId,
                    Facilities = BookedRoom.Facilities,
                    ImageUrls = BookedRoom.ImageUrls,
                    Price = BookedRoom.Price,
                };
                return Ok(roomDto);

            }
            if (BookedRoom is not null)
                return BadRequest(new ApiExcaptionResponse(400, "Room Is Not Available!"));
            return Ok();
        }

      //  [Authorize(Roles = "Customer")]
        [HttpPost("MakeReservation")]

        public async Task<ActionResult<ReservationDto>> MakeReservationForSpecificCustomer(ReservationDto reservationDto)
        {
            if (reservationDto is null) return BadRequest(new ApiExcaptionResponse(400, "Invalid Reservation Data"));
            var mappedReservation = _mapper.Map<CreateReservationDto>(reservationDto);
            var reservation = await _reservationService.CreateReservationAsync(mappedReservation);
            if (reservation is null) return BadRequest(new ApiExcaptionResponse(400, "Invalid Reservation Data"));
            return Ok(reservation);
        }

        [Authorize]
        [HttpPost("ProvideFeedback")]

        public async Task<ActionResult<FeedbackDto>> ProvideFeedbackFromSpecificCustomer(FeedbackDto feedbackDto)
        {
            if (feedbackDto is null) return BadRequest(new ApiExcaptionResponse(400, "Invalid Feedback Data"));
            var mappedFeedback = _mapper.Map<FeedbackDto>(feedbackDto);
            var feedback = await _reservationService.ProvideFeedbackAsync(mappedFeedback);
            if (feedback is null) return BadRequest(new ApiExcaptionResponse(400, "Invalid Feedback Data"));
            return Ok(feedback);
        }

    }
}
