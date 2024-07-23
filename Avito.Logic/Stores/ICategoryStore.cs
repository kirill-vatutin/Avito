using Avito.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avito.Logic.Stores
{
    public interface ICategoryStore
    {
        Task<IReadOnlyList<Category>> Get();
        Task Add(Category category);
    }
}
