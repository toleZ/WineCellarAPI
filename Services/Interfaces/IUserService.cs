using Common.Models;
using Data.Entities;

namespace Services.Interfaces
{
    public interface IUserService
    {
        User? Authenticate(CredentialsForAuthenticateDTO credentialsDTO); 
        int CreateUser(CreateUserDTO createUserDTO);
    }
}