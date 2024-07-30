using Avito.Infrastructure;
using Avito.Infrastructure.Auth.Interfaces;
using Avito.Logic.Models;
using Avito.Logic.Stores;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace Avito.Application.Store
{
    public class UserStore : BaseStore, IUserStore
    {


        public UserStore(AvitoDbContext context, IPasswordHasher passwordHasher) : base(context)
        {
        }



        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<User>> Get()
        {
            var users = _context.Users;
            return await users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetByEmail(string email)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<User?> GetById(int id)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }
        public async Task<int> GetRoleIdUser()
        {
            Role? role = await _context.Roles.AsNoTracking().FirstOrDefaultAsync(u => u.Name == "user");
            return role.Id;
        }


        public Task Update(User user)
        {
            throw new NotImplementedException();
        }

        public  int GetUserIdFromJwt(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var userIdClaim = jwtToken.Claims.First(claim => claim.Type == "userId");

            int tokenUserId = Convert.ToInt32(userIdClaim.Value);
            return tokenUserId;
        }

        public  bool VerifyUser(int tokenUserId, int userId)
        {

            return tokenUserId == userId;
        }

    }
}
