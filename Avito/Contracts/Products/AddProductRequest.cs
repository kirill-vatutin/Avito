using Avito.Logic.Models;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
namespace Avito.Contracts.Products
{
    public record AddProductRequest(
           [Required] string Name ,
           [Required] string Description ,
           [Required] double Price ,
           [Required] int CategoryId 
        );
    
}
