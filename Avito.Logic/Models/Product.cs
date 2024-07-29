using System.Text.Json.Serialization;

namespace Avito.Logic.Models
{
    public class Product : BaseEntity
    {
      

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public double Price { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }

        private Product(string name,string description, double price, int userId, int categoryId)
        {
            Name = name;
            Description = description;
            Price = price;
            UserId = userId;
            CategoryId = categoryId;
        }

        public static Product Create(string name,string description,double price,int userId,int categoryId)
        {
            return new Product(name,description, price, userId, categoryId);
        }

    }
}
