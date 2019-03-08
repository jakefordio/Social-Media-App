using System;
using System.Threading.Tasks;
using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {

        private readonly DataContext context;

        public AuthRepository(DataContext context)
        {
            this.context = context;
        }

        public Task<User> Login(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt); //Out keyword creates a reference to the 2 byte array variables.
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            var hmac = new System.Security.Cryptography.HMACSHA512();
        }

        public Task<bool> UserExists(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}