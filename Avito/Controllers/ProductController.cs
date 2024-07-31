using Avito.Contracts.Products;
using Avito.Logic.Models;
using Avito.Logic.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.IdentityModel.Tokens.Jwt;


namespace Avito.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IProductStore _repository;
        private readonly IUserStore _userRepository;

        public ProductController(IProductStore repository, IUserStore userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _repository.Get();

        }

        [HttpGet]
        [Route("wishList")]
        public async Task<IEnumerable<WishlistItem>> GetWishList()
        {
            return await _repository.GetWishList();

        }


        [HttpGet("{name}")]
        public async Task<IEnumerable<Product?>> GetByName(string name)
        {
            return await _repository.GetByName(name);
        }


        [HttpPost]
        [Authorize(Policy = "UserOnly")]
        public async Task<ActionResult> Post([FromBody] AddProductRequest productRequest)
        {
            var token = Request.Cookies["tasty"];
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token is missing!");
            }
            int userId = _userRepository.GetUserIdFromJwt(token);
            Product product = Product.Create(productRequest.Name, productRequest.Description, productRequest.Price, userId, productRequest.CategoryId);
            await _repository.Add(product);
            return Ok(product);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AddProductRequest productRequest)
        {
            Product? product = await _repository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            int userId = _userRepository.GetUserIdFromJwt(Request.Cookies["tasty"]);
            var isVerify = _userRepository.VerifyUser(userId, product.UserId);
            if (!isVerify)
            {
                return Unauthorized();
            }
            product.Name = productRequest.Name;
            product.Description = productRequest.Description;
            product.Price = productRequest.Price;
            product.CategoryId = productRequest.CategoryId;
            await _repository.Update(product);
            return Ok(product);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Product? product = await _repository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            var userId = _userRepository.GetUserIdFromJwt(Request.Cookies["tasty"]);
            var isVerify = _userRepository.VerifyUser(userId, product.UserId);
            if (!isVerify)
            {
                return Unauthorized();
            }
            await _repository.Delete(id);
            return Ok(product);
        }

        [HttpPost]
        [Route("addWishList")]
        public async Task<ActionResult> AddWishList(int id)
        {
            int userId = _userRepository.GetUserIdFromJwt(Request.Cookies["tasty"]);
            await _repository.AddWishList(id,userId);
            return Ok();
        }

        [HttpPatch]
        [Route("updatePrice")]
        public async Task<ActionResult> UpdatePrice(DTOPrice dTOPrice)
        {
            await _repository.UpdateProductPriceAsync(dTOPrice.productId,dTOPrice.newPrice);
            return Ok();
        }
        public record DTOPrice(
      int productId,
      double newPrice
      );
    }
}
   
