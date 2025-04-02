using Hotel.Core.Data.Context;
using Hotel.Core.Dtos;
using Hotel.Core.Dtos.HotelDTO;
using Hotel.Core.Entities.customer;
using Hotel.Core.Entities.Enum;
using Hotel.Core.Entities.HotelStaffs;
using Hotel.Repository.Services.OfferService.JWT_Token;
using Hotel.Repository.Services.PasswordHashing;
using Hotel.Repository.Services.Username_Hashing;
using Hotel.Repository.UnitOfWork;
using Hotel_Reservation_System.Error;
using Hotel_Reservation_System.Filter;
using Hotel_Reservation_System.ViewModels.UserIdentity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Reservation_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelStaffController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUsernameHasher _usernameHasher;
        public HotelStaffController(IUnitOfWork unitOfWork ,ITokenService tokenService
         , IPasswordHasher passwordHasher , IUsernameHasher usernameHasher)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
           _passwordHasher = passwordHasher;
            _usernameHasher = usernameHasher;
        }
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<HotelStaffDTO>> Register(RegisterStaffDTO model)
        {
            if (model is null) return BadRequest(new ApiExcaptionResponse(400));
            var existingUser = await _unitOfWork.Repository<HotelStaff>().GetByCriteriaAsync(x => x.Email == model.Email);
            if (existingUser != null)
                return BadRequest(new ApiExcaptionResponse(400, "Email already exists"));

            var User = new HotelStaff()
            { Address = model.Address,
                Email = model.Email, 
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = _passwordHasher.HashPassword(model.Password),
                PhoneNumber = model.PhoneNumber, 
               Role = (Roles)Enum.Parse(typeof(Roles), model.Role),
               UserName = _usernameHasher.HashUserName(model.Email.Split('@')[0])
            };
            if(User is null) return BadRequest(new ApiExcaptionResponse(400));
            try
            {
                await _unitOfWork.Repository<HotelStaff>().AddAsync(User);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception 
                Console.WriteLine($"Error saving changes: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
            var ResultDto = new HotelStaffDTO()
            {
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                Token = _tokenService.GetTokenAsync(User.Id, User.Email, User.Role).Result

            };
            return Ok(ResultDto);

        }


        [HttpPost("Login")]
        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { Features.Login })]

        public async Task<ActionResult<HotelStaffDTO>> Login(LoginDTO model)
        {
            if (model is null) return BadRequest(new ApiExcaptionResponse(400));
            var user = await _unitOfWork.Repository<HotelStaff>().GetByCriteriaAsync(x => x.Email == model.Email);
            if (user is null) return Unauthorized(new ApiExcaptionResponse(401));

            var ResultDto = new HotelStaffDTO()
            {
                Email = user.Email,
                UserName = user.Email.Split('@')[0],
                Token = await _tokenService.GetTokenAsync(user.Id, user.Email, user.Role)
            };
            if (!_passwordHasher.VerifyPassword(model.Password, user.Password))
                return Unauthorized(new ApiExcaptionResponse(401));
            if (!_usernameHasher.VerifyUserName(ResultDto.Email.Split('@')[0], user.UserName))
                return Unauthorized(new ApiExcaptionResponse(401));

            return Ok(ResultDto);
        }
        [Authorize(Roles = "Staff")]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser(string Email , Roles roles)
        {
            if (Email is null) return Unauthorized(new ApiExcaptionResponse(401));
            var user = await _unitOfWork.Repository<Customer>().GetByCriteriaAsync(x => x.Email == Email);
            if (user is null) return Unauthorized(new ApiExcaptionResponse(401));
            var obj = new UserDTO()
            {
                UserName = user.UserName,
                Email = Email,
                Token = await _tokenService.GetTokenAsync(user.Id, user.Email , roles)
            };
            return Ok(obj);

        }

    }
}
