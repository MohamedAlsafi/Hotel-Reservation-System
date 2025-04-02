using AutoMapper;
using Hotel.Core.Data.Context;
using Hotel.Core.Dtos.FeedbackDtos;
using Hotel.Core.Dtos.Reservation;
using Hotel.Core.Entities.customer;
using Hotel.Core.Entities.Enum;
using Hotel.Repository.Services.OfferService.JWT_Token;
using Hotel.Repository.Services.PasswordHashing;
using Hotel.Repository.Services.ReservationService;
using Hotel.Repository.Services.RoomService;
using Hotel.Repository.Services.Username_Hashing;
using Hotel.Repository.UnitOfWork;
using Hotel_Reservation_System.Error;
using Hotel_Reservation_System.Filter;
using Hotel_Reservation_System.ViewModels.Room;
using Hotel_Reservation_System.ViewModels.UserIdentity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Reservation_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUsernameHasher _usernameHasher;

        public AccountController(ITokenService tokenService,IUnitOfWork unitOfWork , IPasswordHasher passwordHasher , IUsernameHasher usernameHasher)
        {
           _tokenService = tokenService;
           _unitOfWork = unitOfWork;
           _passwordHasher = passwordHasher;
           _usernameHasher = usernameHasher;
        }
        [HttpPost("Register")]
        [AllowAnonymous]
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
                Token = await _tokenService.GetTokenAsync(User.Id, userDTO.Email , User.Role)

            };

            return Ok(ResultDto);


        }
        [HttpPost("Login")]
        [Authorize]
        [TypeFilter<CustomAuthorizeFilter>( Arguments = new object[] {Features.Login})]
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
                Token = await _tokenService.GetTokenAsync(User.Id, loginDTO.Email , User.Role)

            };
            return Ok(ResultDto);
        }


        [HttpPost("SignOut")]
        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { Features.SignOut })]
        public async Task<ActionResult<UserDTO>> SignOut(LoginDTO loginDTO)
        {
            if(loginDTO is null) return BadRequest(new ApiExcaptionResponse(400));
            var User = await _unitOfWork.Repository<Customer>().GetByCriteriaAsync(x => x.Email == loginDTO.Email );
            if (User is null) return Unauthorized(new ApiExcaptionResponse(401));
            var Result = new LoginDTO()
            {
                Email = User.Email,
                Password = loginDTO.Password,
            };
            if (!_passwordHasher.VerifyPassword(Result.Password, User.Password))
                return Unauthorized(new ApiExcaptionResponse(401));
            if (!_usernameHasher.VerifyUserName(Result.Email.Split('@')[0], User.UserName))
                return Unauthorized(new ApiExcaptionResponse(401));
            if (Result is null) return BadRequest(new ApiExcaptionResponse(401));

          await  AuthenticationHttpContextExtensions.SignOutAsync(HttpContext,CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
