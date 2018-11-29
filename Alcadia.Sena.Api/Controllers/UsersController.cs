using Alcadia.Sena.Api.Extensions;
using Alcadia.Sena.Models.Users;
using Alcadia.Sena.Processing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alcadia.Sena.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IProcessor<User> _processor;

        public UsersController(IProcessor<User> processor)
        {
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<UserView>> GetPaged([FromQuery] UserFilter filter)
        {
            var result = await _processor.GetDynamicPagedAsync<UserFilter, UserView>(filter);
            return Response.GetPagedResponse(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<UserView> GetById(string id)
        {
            return await _processor.GetAsync<UserView>(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserAdd obj)
        {
            var result = await _processor.InsertAsync<UserAdd, UserView>(obj: obj);
            return CreatedAtRoute("EditUser", new { id = result.Id }, result);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut("{id}", Name = "EditUser")]
        public async Task<ActionResult> Put(string id, [FromBody] UserView obj)
        {
            await _processor.UpdateAsync(id, obj);
            return new NoContentResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _processor.DeleteAsync(id);
            return new NoContentResult();
        }
    }
}
