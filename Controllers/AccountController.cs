using EFCore.Sample.API.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace EFCore.Sample.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly MyDbContext _context;

        public AccountController(MyDbContext context)
        {
            _context = context;
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
                    return Ok("Authentication Success");
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
