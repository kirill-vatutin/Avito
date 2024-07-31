using Avito.Infrastructure;
using Avito.Logic.Models;
using Avito.Logic.Stores;
using Microsoft.EntityFrameworkCore;

namespace Avito.Application.Store
{
    public class ProductStore : BaseStore, IProductStore
    {

        private readonly IRabbitMqPublisher _rabbitMqPublisher;

        public ProductStore(
            AvitoDbContext context,
            IRabbitMqPublisher rabbitMqPublisher) : base(context)
        {
            _rabbitMqPublisher = rabbitMqPublisher;
        }


        public async Task Add(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Product>> Get()
        {
            var products = await _context.Products.AsNoTracking().ToListAsync();
            return products;
        }

        public async Task<IReadOnlyList<Product?>> GetByCategory(Category category)
        {
            IQueryable<Product> productsUQue = _context.Products;
            var products = productsUQue.Where(u => u.CategoryId == category.Id);
            return await productsUQue.AsNoTracking().ToListAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<IEnumerable<Product?>> GetByName(string name)
        {
            IQueryable<Product> products = _context.Products.AsNoTracking();
            var productsList = products.Where(p => EF.Functions.Like(p.Name, $"%{name}%"));
            return await productsList.ToListAsync();
        }

        public async Task Update(Product product)
        {
            await _context.Products.Where(u => u.Id == product.Id).ExecuteUpdateAsync(s => s
            .SetProperty(u => u.Name, product.Name)
            .SetProperty(u => u.Description, product.Description)
            .SetProperty(u => u.Price, product.Price)
            .SetProperty(u => u.CategoryId, product.CategoryId)
            );
        }

        public async Task UpdateProductPriceAsync(int productId, double newPrice)
        {
            var product = await _context.Products.FirstOrDefaultAsync(u => u.Id == productId);
            if (product != null)
            {
                
                await _context.Products.ExecuteUpdateAsync(s => s
                .SetProperty(u => u.Price, newPrice));
                await _context.SaveChangesAsync();

                IQueryable<WishlistItem> usersWithWishListQuaryable = _context.WishLists
                    .Where(w => w.ProductId == productId);
                var usersWithWishList = await usersWithWishListQuaryable.
                    Select(w => w.User)
                    .ToListAsync();

                foreach (var user in usersWithWishList)
                {
                    if (!string.IsNullOrEmpty(user.TelegramChatId))
                        _rabbitMqPublisher.PublishPriceChange(product.Name, newPrice, user.TelegramChatId);
                }
            }
        }

        public async Task AddWishList(int id,int userId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(u => u.Id == id);
            if (product != null)
            {
                WishlistItem wishlist = new WishlistItem
                {
                    ProductId = product.Id,
                    UserId = userId,
                    Name = product.Name,
                };
                _context.WishLists.Add(wishlist);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<WishlistItem>> GetWishList()
        {
            return await _context.WishLists.ToListAsync();
        }
    }
}
