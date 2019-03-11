namespace SocialMediaApp.API.DTOs
{
    //Data Transfer Objects (DTO's) are often used to take more complex Data Models from MVC,
    //and simplify them so that they can be transferred from client to server via HTTP.
    //THIS CORRESPONDS TO THE USER MODEL
    public class UserForRegisterDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}