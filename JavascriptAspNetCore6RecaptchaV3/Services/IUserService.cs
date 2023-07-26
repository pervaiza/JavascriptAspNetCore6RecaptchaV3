using JavascriptAspNetCore6RecaptchaV3.Models;

namespace JavascriptAspNetCore6RecaptchaV3.Services
{
    public interface IUserService
    {
        bool CreateUser(NewUser newUser);
    }

    public class UserService : IUserService
    {
        public bool CreateUser(NewUser newUser)
        {
            return true;
        }
    }
}
