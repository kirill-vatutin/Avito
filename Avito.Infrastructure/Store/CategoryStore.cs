using Avito.Logic.Models;
using Avito.Logic.Stores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avito.Infrastructure.Store
{
    public class CategoryStore : BaseStore,ICategoryStore
    {


        public CategoryStore(AvitoDbContext context) : base(context) { }
        
       
        public async Task Add(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
           
        }

        public async Task<IReadOnlyList<Category>> Get()
        {
            var categories=await _context.Categories.AsNoTracking().ToListAsync();
            return categories;
        }
    }
}
