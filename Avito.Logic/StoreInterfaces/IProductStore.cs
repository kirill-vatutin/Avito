using Avito.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avito.Logic.Stores
{
    public interface IProductStore
    {

        Task<IReadOnlyList<Product>> Get();
        Task<IReadOnlyList<Product?>> GetByCategory(Category category);

        Task<IEnumerable<Product?>> GetByName(string name);
        Task Add(Product product);

        Task Delete(int id);
        Task<Product?> GetById(int id);
        Task Update(Product product);
    }
}
