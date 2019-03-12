using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMediaApp.API.Data;
using SocialMediaApp.API.DTOs;
using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Controllers
{
    // http://localhost:5000/api/values
    [Route("api/[controller]")] //ApiController attribute requires attribute routing instead of conventional routing, also enables validation for requests.
    [ApiController] //new to core 2.1, automagically validates request.
    //ControllerBase, rather than Controller, takes out support for Views, since in our App, Angular handles our views.
    public class AuthController : ControllerBase  //ControllerBase gives access to HTTP reponses and actions, no views.
    {
        private readonly IAuthRepository repo;
        private readonly IConfiguration config; //We inject this so that we can use AppSetting.JSON for security token.
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            this.repo = repo;
            this.config = config;
        }

        [HttpPost("register")] //Using register method
        //Parameters that we send up to our methods via HTTP, ASP.NET Core MVC will try to infer the parameters
        //from either the request body, query string, or form. You can use attributes like [FromBody] to tell it where userForRegisterDTO
        //is coming from, but using the new [ApiController] attribute on our class above should do it automagically.
        public async Task<IActionResult> Register(UserForRegisterDTO userForRegisterDTO) //need to use Data Transfer Object(DTO) JSON
        {
            // Since we are using [ApiController] attribute, we don't need to check the ModelState as below, since it infers automagically.
            // if( !ModelState.IsValid )
            // {
            //     return BadRequest(ModelState);
            // }

            userForRegisterDTO.Username = userForRegisterDTO.Username.ToLower(); //Non-case-sensitive

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

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userForLoginDTO)
        {
            //Checking to make sure we have a user, and username and password is stored in the database.
            var userFromRepo = await repo.Login(userForLoginDTO.Username.ToLower(), userForLoginDTO.Password);

            if ( userFromRepo == null ) //If search comes back with no results.
            {
                return Unauthorized(); //Tell user they are unauthorized, instead of giving a hint that username exists or password incorrect.
            }

            /**************************************** BEGIN CODE FOR CREATING USER TOKEN ****************************************/
            var claims = new[] //claims are information about our user.
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            //hash is not readable in our key.
            //Storing this in AppSettings.JSON so that we can use it in many other parts of our application,
            //similar to our database connection string. This requires use of Configuration Class, which we injected in constructor.
            //IRL, the token should be generated and much longer than "super secret key"
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));

            //signing credentials: takes security key above, and algorithm to hash this security key into our creds variable.
            //The server must sign the token before the sending it back, using the key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //Security token descriptor that contains claims, expiry date, and creds.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1), //Very bold to have an entire day, but this is a training course.
                SigningCredentials = creds
            };

            //Setting up handler, so taht we can actually create the Token and use it.
            var tokenHandler = new JwtSecurityTokenHandler();

            //Create the token, passing in our token descriptor
            var token = tokenHandler.CreateToken(tokenDescriptor);
            /**************************************** END CODE FOR CREATING USER TOKEN ****************************************/

            //return our token as an object to our client.
            return Ok(new {
                token = tokenHandler.WriteToken(token) //WriteToken writes our response that we are sending back to the client.
            });
        }
    }
}