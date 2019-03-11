using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.API.Data;
using SocialMediaApp.API.DTOs;
using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Controllers
{
    // http://localhost:5000/api/values
    [Route("api/[controller]")] //ApiController attribute requires attribute routing instead of conventional routing
    [ApiController] //new to core 2.1, automagically validates request.
    //ControllerBase, rather than Controller, takes out support for Views, since in our App, Angular handles our views.
    public class AuthController : ControllerBase  //ControllerBase gives access to HTTP reponses and actions, no views.
    {
        private readonly IAuthRepository repo;
        public AuthController(IAuthRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost("register")] //Using register method
        //Parameters that we send up to our methods via HTTP, ASP.NET Core MVC will try to infer the parameters
        //from either the request body, query string, or form. You can use attributes like [FromBody] to tell it where userForRegisterDTO
        //is coming from, but using the new [ApiController] attribute on our class above should do it automagically.
        public async Task<IActionResult> Register(UserForRegisterDTO userForRegisterDTO) //need to use Data Transfer Object(DTO) JSON
        {
            userForRegisterDTO.Username = userForRegisterDTO.Username.ToLower();

            if (await repo.UserExists(userForRegisterDTO.Username)) //Using our UserExists Method from our AuthRepository class.
            {
                return BadRequest("Username already exists"); //BadRequest inherits from ControllerBase.
            }

            var userToCreate = new User
            {
                Username = userForRegisterDTO.Username
            };

            var createdUser = await repo.Register(userToCreate, userForRegisterDTO.Password);

            return StatusCode(201); //StatusCode of CreatedAtRoute() <-- we will fix later when we can get current user.
        }
    }
}