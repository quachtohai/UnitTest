using App.Models;
using App.Repository;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserInterface userInterface) : ControllerBase
    {
        [HttpPost("add")]
        public async Task<IActionResult> Create(User user)
        {
            var result = await userInterface.CreateAsync(user);
            if (result)
                return CreatedAtAction(nameof(Create), new { id = user.Id }, user);
            else
                return BadRequest();
        }
        [HttpGet("get")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userInterface.GetAllAsync();
            if (!users.Any()) return NotFound();
            else
                return Ok(users);
        }

        [HttpGet("get-single/{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await userInterface.GetByIdAsync(id);
            if (user is null) return NotFound();
            else
                return Ok(user);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            var result = await userInterface.UpdateAsync(user);
            if (result)
                return Ok();
            else
                return NotFound();
        }



        [HttpPut("delete/{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await userInterface.DeleteAsync(id);
            if (result)
                return NoContent();
            else
                return NotFound();
        }




    }
}
