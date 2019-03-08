using System.Threading.Tasks;
using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Data
{
    public interface IAuthRepository //C# Convention interface start with capital "I"
    {
        Task<User> Register(User user, string password);
        
        Task<User> Login(string username, string password);

        Task<bool> UserExists(string username);
    }
}