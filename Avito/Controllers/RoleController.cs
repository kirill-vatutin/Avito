using Avito.Logic.Models;
using Avito.Logic.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Avito.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]
    public class RoleController : ControllerBase
    {

        private readonly IRoleStore _repository;
        public RoleController(IRoleStore repository)
        {
            _repository = repository;
        }
     

        [HttpGet]
        [Route("roles")]
        public async Task<IEnumerable<Role>> Roles()
        {
            var roles =await  _repository.Get();
            return roles;
        }

       
      
        [HttpPost]
        [Route("addRole")]
        public async Task<ActionResult<Role>> Post([FromBody] Role role)
        {
            await _repository.Add(role);
            return Ok();

        }

        // PUT api/<RoleController>/5
        [HttpPatch("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] string name)
        {
            Role? role =await  _repository.GetById(id);
            if (role != null)
            {
                await _repository.Update(role);
                return Ok();
            }
            else return NotFound();
                

        }

        // DELETE api/<RoleController>/5
        [HttpDelete]
        [Route("roles/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Role? role = await _repository.GetById(id);
            if (role == null)
            {
                return NotFound();
            }
            await _repository.Delete(role);
            return Ok();

        }
    }
}
