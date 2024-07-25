using Avito.Logic.Models;
using Avito.Logic.Stores;
using Microsoft.EntityFrameworkCore;

namespace Avito.Infrastructure.Store
{
    public class UserStore : BaseStore, IUserStore
    {
        public UserStore(AvitoDbContext context) : base(context) { }

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<User>> Get()
        {
            var users = _context.Users;
            return await  users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetByEmail(string email)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public Task Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
