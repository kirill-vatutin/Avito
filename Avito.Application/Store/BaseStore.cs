using Avito.Infrastructure;

namespace Avito.Application.Store
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
