namespace SocialMediaApp.API.DTOs
{
    public class UserForLoginDTO
    {
        //No validation needed here, because username and password are already validated in UserForRegisterDTO
        public string Username { get; set; }
        public string Password { get; set; }
    }
}