using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avito.Infrastructure.Store
{
    public class BaseStore
    {
        protected readonly AvitoDbContext _context;
        protected BaseStore(AvitoDbContext context)
        {
                _context = context;
        }
    }
}
