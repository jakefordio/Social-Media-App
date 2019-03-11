using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {

        private readonly DataContext dbContext;

        public AuthRepository(DataContext context)
        {
            this.dbContext = context;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);

            if(user == null)
            {
                return null;
            }
            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            //passing in salt(key) from the CreatePasswordHash() method
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for(int i = 0; i < computedHash.Length; i++){
                    if(computedHash[i] != passwordHash[i])
                    { 
                        return false;
                    }
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            //"Out" keyword creates a reference to the 2 byte array variables, instead of directly using values.
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            //"Using" allows use of Dispose() method, from the IDisposable interface.
            //HMACSHA512 implements HMAC class, which implements KeyedHashAlgorithm, which implements HashAlgorithm.
            //HashAlgorithm class is what implements the IDisposable interface, which allows us to use "using".
            //"using" surrounds our call to the new incstance of the HMACSHA512 class, and automatically disposes
            //that instance when we are done with the class, freeing up resources.
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                //ComputeHash takes a byte array as an argument, so we have to encode our password
                //string as an array of characters(bytes) using GetBytes
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            //AnyAsync() will check to see if Any User exists in the database
            if (await dbContext.Users.AnyAsync(x => x.Username == username))
            {
                return true;
            }

            return false;
        }
    }
}