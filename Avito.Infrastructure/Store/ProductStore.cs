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
    public class ProductStore :BaseStore, IProductStore
    {
        public ProductStore(AvitoDbContext context) : base(context) { }
       

        public async Task Add(Product product)
        {
           await  _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Product>> Get()
        {
            var products = await _context.Products.AsNoTracking().ToListAsync();
            return products;
        }

        public async Task<IReadOnlyList<Product>> GetByCategory(Category category)
        {
            IQueryable<Product> productsUQue = _context.Products;
            var products = productsUQue.Where(u => u.CategoryId == category.Id);
            return await  productsUQue.AsNoTracking().ToListAsync();
        }

        public async Task<Product?> GetByName(string name)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync(u => u.Name == name);
            return product;
        }
    }
}
