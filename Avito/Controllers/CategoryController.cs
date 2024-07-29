using Avito.Logic.Models;
using Avito.Logic.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Avito.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryStore _repository;

        public CategoryController(ICategoryStore repository)
        {
            _repository = repository;
        }


        [HttpGet]
        public async Task<IEnumerable<Category>> Get()
        {
            return await _repository.Get();
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Category category)
        {
            await _repository.Add(category);
            return Ok(category);
        }
    }
}
