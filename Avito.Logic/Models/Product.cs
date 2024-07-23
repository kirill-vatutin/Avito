namespace Avito.Logic.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User? User { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }


    }
}
