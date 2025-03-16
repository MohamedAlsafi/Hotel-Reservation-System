using AutoMapper;
using Hotel.Core.Dtos.FeedbackDtos;
using Hotel.Core.Dtos.Reservation;
using Hotel.Core.Entities.customer;
using Hotel.Repository.Services.OfferService.JWT_Token;
using Hotel.Repository.Services.ReservationService;
using Hotel_Reservation_System.Error;
using Hotel_Reservation_System.ViewModels.Room;
using Hotel_Reservation_System.ViewModels.UserIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public AccountController(ITokenService tokenService
            , UserManager<Customer> userManager, SignInManager<Customer> signInManager 
            , IReservationService reservationService , IMapper mapper)
        {
           _tokenService = tokenService;
           _userManager = userManager;
            _signInManager = signInManager;
            _reservationService = reservationService;
            this._mapper = mapper;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO userDTO)
        {
            if (userDTO is null) return BadRequest(new ApiExcaptionResponse(400));

            var existingUser = await _userManager.FindByEmailAsync(userDTO.Email);
            if (existingUser != null)
                return BadRequest(new ApiExcaptionResponse(400, "Email already exists"));

            var User = new Customer
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                UserName = userDTO.Email.Split('@')[0],
                Email = userDTO.Email,
                Password = userDTO.Password,
                PhoneNumber = userDTO.PhoneNumber,
                NormalizedEmail = userDTO.Email.Normalize()

            };

            var result = await _userManager.CreateAsync(User, userDTO.Password);
            if (!result.Succeeded) return BadRequest(new ApiExcaptionResponse(400));
            var ResultDto = new UserDTO()
            {
                UserName = userDTO.Email.Split('@')[0],
                Email = userDTO.Email,
                Token = await _tokenService.GetTokenAsync(User, _userManager)

            };
            return Ok(ResultDto);


        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {

            var User = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (User is null) return Unauthorized(new ApiExcaptionResponse(401));

            var Result = await _signInManager.CheckPasswordSignInAsync(User, loginDTO.Password, false);

            if (!Result.Succeeded) return BadRequest(new ApiExcaptionResponse(401));
            var ResultDto = new UserDTO()
            {
                Email = User.Email,
                UserName = User.Email.Split('@')[0],
                Token = await _tokenService.GetTokenAsync(User, _userManager)

            };
            return Ok(ResultDto);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            if (Email is null) return Unauthorized(new ApiExcaptionResponse(401));
            var user = await _userManager.FindByEmailAsync(Email);
            if (user is null) return Unauthorized(new ApiExcaptionResponse(401));
            var obj = new UserDTO()
            {
                UserName = user.UserName,
                Email = Email,
                Token = await _tokenService.GetTokenAsync(user, _userManager)
            };
            return Ok(obj);

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("SearchForRoom")]
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
                return Ok(roomDto );

            }
               if (BookedRoom is not null)
                return BadRequest(new ApiExcaptionResponse(400, "Room Is Not Available!"));
            return Ok();
        }

        [Authorize(Roles ="Customer")]
        [HttpPost("MakeReservation")]

        public async Task<ActionResult<ReservationDto>> MakeReservation(ReservationDto reservationDto)
        {
            if (reservationDto is null) return BadRequest(new ApiExcaptionResponse(400, "Invalid Reservation Data"));
            var mappedReservation = _mapper.Map<CreateReservationDto>(reservationDto);
            var reservation = await _reservationService.CreateReservationAsync(mappedReservation);
            if (reservation is null) return BadRequest(new ApiExcaptionResponse(400, "Invalid Reservation Data"));
            return Ok(reservation);
        }

        [Authorize]
        [HttpPost("ProvideFeedback")]

        public async Task<ActionResult<FeedbackDto>> ProvideFeedback(FeedbackDto feedbackDto)
        {
            if (feedbackDto is null) return BadRequest(new ApiExcaptionResponse(400, "Invalid Feedback Data"));
            var mappedFeedback = _mapper.Map<FeedbackDto>(feedbackDto);
            var feedback = await _reservationService.ProvideFeedbackAsync(mappedFeedback);
            if (feedback is null) return BadRequest(new ApiExcaptionResponse(400, "Invalid Feedback Data"));
            return Ok(feedback);
        }

    }
}
