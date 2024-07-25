using Avito.Logic.Models;

namespace Avito.Logic.Stores
{
    public interface IRoleStore
    {
        Task<IReadOnlyList<Role>> Get();
        Task Add(Role role);
        Task<Role?> GetById(int id);
        Task Update(Role name);
        Task Delete(Role role);

    }
}
