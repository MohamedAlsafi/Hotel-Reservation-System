using AutoMapper;
using Hotel.Core.Data.Context;
using Hotel.Core.Dtos.FeedbackDtos;
using Hotel.Core.Dtos.Reservation;
using Hotel.Core.Entities.customer;
using Hotel.Repository.Services.OfferService.JWT_Token;
using Hotel.Repository.Services.PasswordHashing;
using Hotel.Repository.Services.ReservationService;
using Hotel.Repository.Services.RoomService;
using Hotel.Repository.Services.Username_Hashing;
using Hotel.Repository.UnitOfWork;
using Hotel_Reservation_System.Error;
using Hotel_Reservation_System.ViewModels.Room;
using Hotel_Reservation_System.ViewModels.UserIdentity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUsernameHasher _usernameHasher;
        private readonly IRoomServices _roomServices;

        public AccountController(ITokenService tokenService,IReservationService reservationService,
            IMapper mapper,IUnitOfWork unitOfWork , IPasswordHasher passwordHasher , IUsernameHasher usernameHasher , IRoomServices roomServices)
        {
           _tokenService = tokenService;
           _reservationService = reservationService;
           _mapper = mapper;
           _unitOfWork = unitOfWork;
           _passwordHasher = passwordHasher;
           _usernameHasher = usernameHasher;
            this._roomServices = roomServices;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO userDTO)
        {
            if (userDTO is null) return BadRequest(new ApiExcaptionResponse(400));
            //  "password": "P@ssw0rd",
           // "email": "Hussam@yahoo.com",
            var existingUser = await _unitOfWork.Repository<Customer>().GetByCriteriaAsync(x => x.Email == userDTO.Email);
            if (existingUser != null)
                return BadRequest(new ApiExcaptionResponse(400, "Email already exists"));

            var User = new Customer
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email =userDTO.Email ,
                Password = _passwordHasher.HashPassword(userDTO.Password),
                PhoneNumber = userDTO.PhoneNumber,
                UserName = _usernameHasher.HashUserName(userDTO.Email.Split('@')[0])

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

            var Result = new LoginDTO()
            {
                Email = User.Email,
                Password = loginDTO.Password,
            };
            if(!_passwordHasher.VerifyPassword(Result.Password, User.Password))
                return Unauthorized(new ApiExcaptionResponse(401));
            if (!_usernameHasher.VerifyUserName(Result.Email.Split('@')[0] , User.UserName))
                return Unauthorized(new ApiExcaptionResponse(401));
            if (Result is null) return BadRequest(new ApiExcaptionResponse(401));
            var ResultDto = new UserDTO()
            {
                Email = User.Email,
                UserName = User.Email.Split('@')[0],
                Token = await _tokenService.GetTokenAsync(User, loginDTO.Email)

            };
            return Ok(ResultDto);
        }
        [Authorize(Roles = "Staff")]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser(string Email)
        {
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
        [Authorize(Roles ="User")]
        [HttpPost("SearchForRoom/{roomId}")]
        public async Task<ActionResult<RoomDto>> SearchForRoom(int roomId)
        {
            if (roomId <= 0) return BadRequest(new ApiExcaptionResponse(400, "Invalid RoomId"));
           var room = await _roomServices.SearchForRoomAsync(roomId);
            if (room is null) return BadRequest(new ApiExcaptionResponse(400, "Invalid RoomId"));
            var roomDto = _mapper.Map<RoomDto>(room);
            return Ok(roomDto);

        }
        [Authorize(Roles = "User")]

        [HttpPost("MakeReservation")]

        public async Task<ActionResult<ReservationDto>> MakeReservationForSpecificCustomer(ReservationDto reservationDto)
        {
            if (reservationDto is null) return BadRequest(new ApiExcaptionResponse(400, "Invalid Reservation Data"));
            var mappedReservation = _mapper.Map<CreateReservationDto>(reservationDto);
            var reservation = await _reservationService.CreateReservationAsync(mappedReservation);
            if (reservation is null) return BadRequest(new ApiExcaptionResponse(400, "Invalid Reservation Data"));
            return Ok(reservation);
        }
        [Authorize(Roles = "User")]

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
