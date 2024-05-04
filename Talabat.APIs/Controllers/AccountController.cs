using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Extentions;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        // 1. Register

        [HttpPost("register")] // POST : /api/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var user = new ApplicationUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            var returnedUser = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };

            return Ok(returnedUser);
        }

        // 2. Login

        [HttpPost("login")] // POST: /api/login

        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null) return Unauthorized(new ApiResponse(401, "Invalid Login"));

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401, "Invalid Login"));

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user?.Email ?? string.Empty,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet] // GET: /api/Account
        public async Task<ActionResult<UserDto>> GetCurrentUSer()
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;

            var user = await _userManager.FindByEmailAsync(email);

            return Ok(new UserDto()
            {
                DisplayName = user?.DisplayName ?? string.Empty,
                Email = user?.Email ?? string.Empty,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("address")] // GET: /api/Account/address
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {

            var user = await _userManager.FindUserWithAddressAsync(User);


            return Ok(_mapper.Map<AddressDto>(user.Address));
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("address")] // PUT: /api/account/address
        public async Task<ActionResult<Address>> UpdateUserAddress(AddressDto address)
        {
            var updatedAddress = _mapper.Map<Address>(address);

            var user = await _userManager.FindUserWithAddressAsync(User);

            updatedAddress.Id = user.Address.Id;

            user.Address = updatedAddress;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors.Select(E => E.Description) });
            }

            return Ok(address);



        }

    }
}
