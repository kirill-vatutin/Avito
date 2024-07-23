using Avito.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avito.Logic.Stores
{
    public interface IUserStore
    {
        Task<IReadOnlyList<User>> Get();
       
        Task<User> GetByEmail(string name);
        Task Add(User user);


    }
}
