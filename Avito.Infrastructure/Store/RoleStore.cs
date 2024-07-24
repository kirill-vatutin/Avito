using Avito.Logic.Models;
using Avito.Logic.Stores;
using Microsoft.EntityFrameworkCore;

namespace Avito.Infrastructure.Store
{
    public class RoleStore : BaseStore, IRoleStore
    {
        public RoleStore(AvitoDbContext context) : base(context) { }


        public async Task Add(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Role>> Get()
        {
            var roles = await _context.Roles.AsNoTracking().ToListAsync();
            return roles;
        }


    }
}
