using Hotel.Core.Entities.customer;
using Hotel.Repository.Services.OfferService.JWT_Token;
using Hotel_Reservation_System.Error;
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

        public AccountController(ITokenService tokenService, UserManager<Customer> userManager, SignInManager<Customer> signInManager)
        {
            this._tokenService = tokenService;
            this._userManager = userManager;
            this._signInManager = signInManager;
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
                PhoneNumber = userDTO.PhoneNumber
            };

            var result = await _userManager.CreateAsync(User, userDTO.Password);
            if (!result.Succeeded) return BadRequest(new ApiExcaptionResponse(400));
            var ResultDto = new UserDTO()
            {
                UserName = userDTO.FirstName,
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

    }
}
