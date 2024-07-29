using Avito.Contracts.Users;
using Avito.Infrastructure.Services;
using Avito.Logic.Models;
using Avito.Logic.Stores;
using Microsoft.AspNetCore.Mvc;

namespace Avito.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserStore _repository;
        private readonly HttpContext _httpContext;

        public UserController(IHttpContextAccessor httpContextAccessor,IUserStore repository)
        {
            _repository = repository;
            _httpContext = httpContextAccessor.HttpContext;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IResult> Register(
            RegisterUserRequest request,
            UsersService usersService)
        {
            await usersService.Register(request.FirstName, request.Lastname, request.Email, request.Password);
            return Results.Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IResult> Login(
             [FromBody]LoginUserRequest request,
             UsersService usersService
            )
        {
            var token = await usersService.Login(request.Email, request.Password);
            _httpContext.Response.Cookies.Append("tasty", token);
            return Results.Ok();
        }


        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _repository.Get();
        }


        [HttpGet("{email}")]
        public async Task<ActionResult<User>> GetByEmail(string email)
        {
            User? user = await _repository.GetByEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);

        }


        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> Post([FromBody] User user)
        {
            await _repository.Add(user);
            return Ok();

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            User? user = await _repository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            await _repository.Delete(user);
            return Ok();

        }
    }
}
