using EFCore.Sample.API.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using OneSanofi.API.Services;
using System.ComponentModel;

namespace EFCore.Sample.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IJWTTokenService jWTTokenService;

        public AccountController(MyDbContext context, IJWTTokenService jWTTokenService)
        {
            _context = context;
            this.jWTTokenService = jWTTokenService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Signin([Bind("Email,Password")] UserDto userDto)
        {
            if (ModelState.IsValid)
            {

               
                var user = await _context.Users
               .FirstOrDefaultAsync(m => m.Email == userDto.Email);
                if (user != null && BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password))
                {
                    var token =  this.jWTTokenService.GenerateAccessToken(user);
                    return Ok(token);
                }
                return Unauthorized();

            }
            return Unauthorized();
        }


    }

    public class UserDto
    {
        [Required]
        public string? Email { get; set; }

        [Required,PasswordPropertyText]
        public string? Password { get; set; }
    }
}
