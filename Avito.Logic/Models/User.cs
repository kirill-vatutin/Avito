namespace Avito.Logic.Models
{
    public class User : BaseEntity
    {
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public string TelegramChatId { get; set; } = string.Empty;

        public Role? Role;

        public IList<Product>? Products = null;
        public IList<WishlistItem>? WishList= null;
        public User()
        {
        }
        private User(string firstName, string lastName, string email, string hashedPassword,int roleId)
        {
            Firstname = firstName;
            Lastname = lastName;
            Email = email;
            PasswordHash = hashedPassword;
            RoleId = roleId;
        }

        public static User Create(string firstName, string lastName, string email, string hashedPassword,int roleId)
        {
            return new User(firstName, lastName, email, hashedPassword,roleId);
        }
    }
}
