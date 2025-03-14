using Hotel.Core.Entities.customer;
using Hotel.Repository.Services.OfferService.JWT_Token;
using Hotel_Reservation_System.Error;
using Hotel_Reservation_System.ViewModels.UserIdentity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Reservation_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<Customer> _userManager;

        public AccountController(ITokenService tokenService , UserManager<Customer> userManager)
        {
            this._tokenService = tokenService;
            this._userManager = userManager;
        }
        [HttpPost("Register")]
        public  async Task<ActionResult<UserDTO>> Register(RegisterDTO userDTO)
        {
            if (userDTO is null) return  BadRequest(new ApiExcaptionResponse(400));
            if (await _userManager.FindByEmailAsync(userDTO.Email) is null) 
                return BadRequest(new ApiExcaptionResponse(400));

            var User = new Customer
            {
                    UserName = userDTO.DisplayName,
                    Email = userDTO.Email,
            };
            
                var result = await _userManager.CreateAsync(User, User.Password);
                if (!result.Succeeded) return BadRequest(new ApiExcaptionResponse(400));
                var ResultDto = new UserDTO()
                {
                    UserName = userDTO.DisplayName,
                    Email = userDTO.Email,
                    Token = await _tokenService.GetTokenAsync(User, _userManager)

                };
            return Ok(ResultDto);


        }
    }
}
