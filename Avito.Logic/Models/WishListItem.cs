using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avito.Logic.Models
{
    public class WishlistItem:BaseEntity
    {
       
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public User? User { get; set; }
        public Product? Product { get; set; }
    }
}
