using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginDbContext dbContext;

        public LoginController(LoginDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Get all users
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = dbContext.Logins.ToList(); // Retrieve all users from the Logins table
            return Ok(users); // Return the list of users as a response
        }
        [HttpGet]
        [Route("users/q={Id:Guid}")]
        public IActionResult oneId([FromRoute] Guid Id) 
        {
            //var user=dbContext.Logins.Find(Id);
            var user=dbContext.Logins.FirstOrDefault(x=> x.Id == Id);
            if(user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Login addUserRequest)
        {
            // Generate a new GUID for the user if it's not provided
            addUserRequest.Id = Guid.NewGuid();

            // Add the new user to the database
            dbContext.Logins.Add(addUserRequest);
            dbContext.SaveChanges();

            // Return the CreatedAtAction response
            return CreatedAtAction(nameof(oneId), new { id = addUserRequest.Id }, addUserRequest);
        }
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] Login updateUserRequest)
        {
            // Find the user by Id
            var user = dbContext.Logins.FirstOrDefault(x => x.Id == id);
            // If the user doesn't exist, return a 404 error
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            // Update the user's properties with new data
            user.FullName = updateUserRequest.FullName;
            user.Email = updateUserRequest.Email;
            // Add any other fields that need to be updated
            // Save changes to the database
            dbContext.SaveChanges();
            // Return the updated user object
            return Ok(user);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var user=dbContext.Logins.FirstOrDefault(x=>x.Id == id);
            if(user == null)
            {
                    return BadRequest();
            }
            dbContext.Logins.Remove(user);
            dbContext.SaveChanges();
            return Ok(user);
        }





    }
}
