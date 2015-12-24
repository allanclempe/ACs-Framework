using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ACs.Framework.Web.Data;
using ACs.Framework.Web.Core;
using ACs.NHibernate.Next;
using ACs.Net.Mail;

namespace ACs.Framework.Web.Controllers
{
    [Route("api/[controller]")]
    public class FooController : Controller
    {
        private readonly IFooRepository _fooRepository;
        private readonly IMessageSender _messageSender;

        public FooController(IFooRepository fooRepository, IMessageSender messageSender)
        {
            _messageSender = messageSender;
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
        public async void Post([FromBody]Foo foo)
        {
            _fooRepository.Save(foo);

            await _messageSender.SendEmailAsync(foo.Email, "Activate your account",
                new HtmlMessage("Hello <param name=\"name\" />, <br>To Activate your account please use link bellow<br><br> <param name=\"url\" /><br><br>Regards")
                .SetParams(new { name = foo.Name, url = new Uri("http://somedomain.com/link-to-activate") })
                .ToHtml());

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
