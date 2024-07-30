using System.Text.Json.Serialization;

namespace Avito.Logic.Models
{
    public class WishListItem : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public double Price { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }
    }
}
