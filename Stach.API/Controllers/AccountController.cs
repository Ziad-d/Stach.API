using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stach.API.DTOs;
using Stach.API.Errors;
using Stach.Domain.Models.Identity;

namespace Stach.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")] // POST: /api/account/login
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized(new ApiResponse(401));

            var checkPassword = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (checkPassword.Succeeded is false)
                return Unauthorized(new ApiResponse(401));

            return Ok(new UserDTO
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "Here will be the token"
            });
        }

        [HttpPost("register")] // POST: /api/account/register
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO model)
        {
            var user = new AppUser
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded is false)
                return BadRequest(new ApiResponse(400));

            return Ok(new UserDTO
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "Here will be token"
            });
        }
    }
}
