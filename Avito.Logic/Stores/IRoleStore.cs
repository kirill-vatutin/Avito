using Avito.Logic.Models;

namespace Avito.Logic.Stores
{
    public interface IRoleStore
    {
        Task<IReadOnlyList<Role>> Get();
        Task Add(Role role);
    }
}
