using Common.Models;
using Data.Entities;
using Data.Repository.Interfaces;
using Services.Interfaces;

namespace Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User? Authenticate(CredentialsForAuthenticateDTO credentials)
        {
            return _userRepository.Authenticate(credentials);
        }

        public int CreateUser(CreateUserDTO createUserDTO)
        {
            if (_userRepository.GetUsers().All(user => user.Username != createUserDTO.Username))
            {
                try
                {
                    int newUserId = _userRepository.CreateUser(
                        new User
                        {
                            Username = createUserDTO.Username,
                            Password = createUserDTO.Password
                        }
                        );
                    return newUserId;
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}