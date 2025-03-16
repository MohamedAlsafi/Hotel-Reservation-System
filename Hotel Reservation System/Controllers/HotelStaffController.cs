using Hotel.Core.Data.Context;
using Hotel.Core.Dtos;
using Hotel.Core.Dtos.HotelDTO;
using Hotel.Core.Dtos.Room.Create;
using Hotel.Core.Entities.Enum;
using Hotel.Core.Entities.Enum.HotelStaff;
using Hotel.Repository.Services.OfferService.JWT_Token;
using Hotel_Reservation_System.Error;
using Hotel_Reservation_System.ViewModels.UserIdentity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Reservation_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelStaffController : ControllerBase
    {
        private readonly CustomerIdentityDbContext _dbContext;
        private readonly ITokenService _tokenService;

        public HotelStaffController(CustomerIdentityDbContext dbContext,ITokenService tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<HotelStaffDTO>> Register(RegisterStaffDTO model)
        {
            if (model is null) return BadRequest(new ApiExcaptionResponse(400));
            var existingUser = await _dbContext.HotelStaff.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (existingUser != null)
                return BadRequest(new ApiExcaptionResponse(400, "Email already exists"));

            var User = new HotelStaff()
            { Address = model.Address,
                Email = model.Email, FirstName = model.FirstName,
                LastName = model.LastName, Password = model.Password,
                PhoneNumber = model.PhoneNumber, 
               Role = (HotelStaffRole)Enum.Parse(typeof(HotelStaffRole), model.Role),
               UserName = model.Email.Split('@')[0]
            };
            if(User is null) return BadRequest(new ApiExcaptionResponse(400));
            try
            {
                await _dbContext.HotelStaff.AddAsync(User);
                await _dbContext.SaveChangesAsync();
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
                Token = _tokenService.GetTokenAsyncForHotelStaff(User.Id, User.Email, User.Role).Result

            };
            return Ok(ResultDto);

        }


        [HttpPost("Login")]
        public async Task<ActionResult<HotelStaffDTO>> Login(LoginDTO model)
        {
            if (model is null) return BadRequest(new ApiExcaptionResponse(400));
            var user = await _dbContext.HotelStaff.FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);

            if(user is null) return Unauthorized(new ApiExcaptionResponse(401));

            var ResultDto = new HotelStaffDTO()
            {
                Email = user.Email,
                UserName = user.Email.Split('@')[0],
                Token  = _tokenService.GetTokenAsyncForHotelStaff(user.Id, user.Email, user.Role).Result
            };

            return Ok(ResultDto);
        }

     
    }
}
