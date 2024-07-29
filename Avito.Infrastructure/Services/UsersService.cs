using Avito.Infrastructure.Auth.Interfaces;
using Avito.Logic.Models;
using Avito.Logic.Stores;

namespace Avito.Infrastructure.Services
{
    public class UsersService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IUserStore _userRepository;

        public UsersService(
            IUserStore userRepository,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider)
        {
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _userRepository = userRepository;

        }

        public async Task Register(
            string firstName,
            string lastName,
            string email,
            string password
            )
        {
            var hashedPassword = _passwordHasher.Generate(password);
            var roleId = await _userRepository.GetRoleIdUser();
            User user = User.Create(firstName, lastName, email, hashedPassword, roleId);
            await _userRepository.Add(user);
        }
        public async Task<string> Login(string email,string password)
        {
            var user = await _userRepository.GetByEmail(email); 
            if (user == null)
            {
                return "0";
            }
            var result = _passwordHasher.Verify(password, user.PasswordHash);
            if (result == false)
            {
                return "-1";
            }
            var token = _jwtProvider.GenerateToken(user);

            return token;
        }
    }
}
