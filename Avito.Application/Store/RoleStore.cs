using Avito.Infrastructure;
using Avito.Logic.Models;
using Avito.Logic.Stores;
using Microsoft.EntityFrameworkCore;

namespace Avito.Application.Store
{
    public class RoleStore : BaseStore, IRoleStore
    {
        public RoleStore(AvitoDbContext context) : base(context) { }


        public async Task Add(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Role role)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Role>> Get()
        {
            var roles = await _context.Roles.AsNoTracking().ToListAsync();
            return roles;
        }

        public async Task<Role?> GetById(int id)
        {
            Role? role = await _context.Roles.FirstOrDefaultAsync(u => u.Id == id);
            return role;
        }

        public async Task Update(Role role)
        {
            await _context.Roles.Where(x => x.Id == role.Id).ExecuteUpdateAsync(s => s
            .SetProperty(u => u.Name, role.Name));
        }


    }
}
