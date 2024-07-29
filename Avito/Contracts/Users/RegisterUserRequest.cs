using System.ComponentModel.DataAnnotations;

namespace Avito.Contracts.Users
{
    public record RegisterUserRequest
    (
        [Required]  string FirstName, 
        [Required]  string Lastname ,
        [Required]  string Password,
        [Required]  string Email
       
    );
}
