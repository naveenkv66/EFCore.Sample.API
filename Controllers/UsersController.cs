using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFCore.Sample.API;
using EFCore.Sample.API.DataModels;

namespace EFCore.Sample.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _context;

        public UsersController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetUserById")]
        public async Task<IActionResult> Get(long? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

   
   
       
        
        [HttpPost(Name = "SignUp")]
        public async Task<IActionResult> SignUp([Bind("EmployeeId,FirstName,LastName,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {

                user.Password=  BCrypt.Net.BCrypt.HashPassword(user.Password);
                _context.Add(user);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }


     


        [HttpPut(Name = "UpdateUser")]
       
        public async Task<IActionResult> UpdateUser(long id, [Bind("EmployeeId,FirstName,LastName,Email")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existinguser = await _context.Users
              .FirstOrDefaultAsync(m => m.Id == id);

                    if(existinguser == null) {
                        return NotFound();
                    }
                    existinguser.FirstName = user.FirstName;
                    existinguser.LastName = user.LastName;
                    existinguser.Email = user.Email;
                    existinguser.EmployeeId = user.EmployeeId;
                    _context.Update(user);
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

     

        [HttpDelete, ActionName("Delete")]
       
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Users == null)
            {
                return Problem("User not exist with given id.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
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
