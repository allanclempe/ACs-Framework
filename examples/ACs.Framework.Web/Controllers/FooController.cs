using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ACs.Framework.Web.Data;
using ACs.Framework.Web.Core;
using ACs.NHibernate.Next;

namespace ACs.Framework.Web.Controllers
{
    [Route("api/[controller]")]
    public class FooController : Controller
    {
        private readonly IFooRepository _fooRepository;
        public FooController(IFooRepository fooRepository)
        {
            _fooRepository = fooRepository;
        }

        // GET: api/values
        [HttpGet,SessionRequired]
        public IEnumerable<Foo> Get()
        {
            return _fooRepository.GetAll();
        }

        [HttpGet("notfound")]
        public IActionResult NotFound()
        {
            return HttpNotFound();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
