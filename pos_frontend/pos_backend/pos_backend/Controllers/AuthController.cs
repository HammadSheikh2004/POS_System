using Domain.DTOs;
using Infrastructure.Identity_User;
using Infrastructure.JwtService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace pos_backend.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Signin")]
        public async Task<IActionResult> Signin([FromBody] SigninDtos signinDtos, [FromServices] JwtTokenService tokenService, [FromServices] SignInManager<ApplicationUser> signInManager)
        {
            var user = await _userManager.FindByNameAsync(signinDtos.userName!);
            if (user == null)
            {
                return Unauthorized(new { unauthorizedMessage = "User Not Found!" });
            }

            if (await _userManager.IsLockedOutAsync(user))
            {
                return Unauthorized(new { unauthorizedMessage = "User is locked out. Please try again later." });
            }

            var result = await signInManager.PasswordSignInAsync(user, signinDtos.password!, isPersistent: false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                await _userManager.ResetAccessFailedCountAsync(user);
                var token = await tokenService.GenerateJwtToken(user);
                
                return Ok(new { successMessage = "Login successful!", token = token });
            }

            if (result.IsLockedOut)
            {
                return Unauthorized(new { unauthorizedMessage = "Account locked due to multiple failed attempts. Please call your administrator." });
            }
            var accessFailedCount = await _userManager.GetAccessFailedCountAsync(user);
            var attremptLeft = 3 - accessFailedCount;

            return Unauthorized(new { unauthorizedMessage = $"Invalid login attempt! You have {attremptLeft} attempts left." });
        }

        [HttpGet("UsersDetails")]
        public async Task<IActionResult> GetUsersDetails()
        {
            var user = await _userManager.Users.ToListAsync();
 
            return Ok(user);
        }
    }
}
