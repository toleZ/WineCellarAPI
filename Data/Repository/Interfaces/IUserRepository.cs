using Common.Models;
using Data.Entities;

namespace Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        User? Authenticate(CredentialsForAuthenticateDTO credentialsDTO);
        IEnumerable<User> GetUsers();
        int CreateUser(User user);
    }
}