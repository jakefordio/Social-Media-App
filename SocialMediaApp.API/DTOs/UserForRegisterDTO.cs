using System.ComponentModel.DataAnnotations;

namespace SocialMediaApp.API.DTOs
{
    //Data Transfer Objects (DTO's) are often used to take more complex Data Models from MVC,
    //and simplify them so that they can be transferred from client to server via HTTP.
    //THIS CORRESPONDS TO THE USER MODEL
    public class UserForRegisterDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Password must be between 4 to 8 characters.")]
        public string Password { get; set; }
    }
}