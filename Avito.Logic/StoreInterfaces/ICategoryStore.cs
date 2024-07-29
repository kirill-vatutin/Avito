using Avito.Logic.Models;

namespace Avito.Logic.Stores
{
    public interface ICategoryStore
    {
        Task<IReadOnlyList<Category>> Get();
        Task Add(Category category);
    }
}
