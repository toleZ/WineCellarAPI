using Common.Models;
using Data.Entities;
using Data.Repository.Interfaces;

namespace Data.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {

        private readonly WineCellarContext _context;
        public UserRepository(WineCellarContext context)
        {
            _context = context;
        }

        public User? Authenticate(CredentialsForAuthenticateDTO credentials)
        {
            User? userToAuthenticate = _context.Users.FirstOrDefault(u => u.Username == credentials.Username && u.Password == credentials.Password);

            return userToAuthenticate;
        }
        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }
        public int CreateUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return _context.Users.Max(u => u.Id);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}