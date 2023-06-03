using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFCore.Sample.API;
using EFCore.Sample.API.DataModels;
using Microsoft.AspNetCore.Authorization;

namespace EFCore.Sample.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _context;

        public UsersController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("[action]/{email}")]
        public async Task<IActionResult> GetUserByEmailId(string? email)
        {
            if (email == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Email == email);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Get()
        {

            var user = await _context.Users.ToListAsync();

            return Ok(user);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SignUp([Bind("EmployeeId,FirstName,LastName,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _context.Add(user);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }





        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateUser(string email, [Bind("EmployeeId,FirstName,LastName")] User user)
        {
            

            if (ModelState.IsValid)
            {
                try
                {
                    var existinguser = await _context.Users
              .FirstOrDefaultAsync(m => m.Email == email);

                    if (existinguser == null)
                    {
                        return NotFound();
                    }
                    existinguser.FirstName = user.FirstName;
                    existinguser.LastName = user.LastName;                  
                    existinguser.EmployeeId = user.EmployeeId;
                    _context.Update(existinguser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok();
            }
            return BadRequest();
        }



        [HttpDelete]
        [Route("[action]/{email}")]
        public async Task<IActionResult> Delete(string email)
        {

            var existinguser = await _context.Users
              .FirstOrDefaultAsync(m => m.Email == email);

            if (existinguser != null)
            {
                _context.Users.Remove(existinguser);
            }
            else
            {
                return Problem("User not exist with given Email id.");
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool UserExists(long id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
