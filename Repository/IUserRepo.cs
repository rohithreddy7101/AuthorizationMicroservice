
using AuthorizationMicroservice.Models;

namespace AuthorizationMicroservice.Repository
{
    public interface IUserRepo
    {
        User AuthenticateUser(User login);
        string GenerateJSONWebToken(User userInfo);
    }
}
