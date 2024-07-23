namespace Avito.Logic.Models
{
    public class User : BaseEntity
    {
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int RoleId { get; set; }

        public Role? Role;

        public IList<Product>? Products = null;
    }
}
