using HumanResourceAPI.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanResourceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

     
        [HttpGet]
        public ActionResult<Entity> Get() 
        {
            var result = repository.Get();
            return Ok(result);
        }

        [HttpGet("{key}")]
        public virtual ActionResult<Entity> Get(Key key)
        {
            var result = repository.Get(key);
            return Ok(result);
        }

        [HttpPut]
        public ActionResult<Entity> Update(Entity entity)
        {
            int result = repository.Update(entity);
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<Entity> Insert(Entity entity)
        {
            int result = repository.Insert(entity);
            return Ok(result);
        }

        [HttpDelete]

        public ActionResult<Entity> Delete(Entity entity)
        {
            int result = repository.Delete(entity);
            return Ok(result);
        }
    }
}
