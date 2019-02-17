using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HS.Data;
using HS.Data.Providers;
using HS.IService.Menus;
using Microsoft.AspNetCore.Mvc;

namespace HS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        public IContextFactory _contextFactory { get; set; }

        public ValuesController(IContextFactory contextFactory)
        {
            this._contextFactory = contextFactory;
        }
        // GET api/values
        [HttpGet]
        [EntityAuthorize(Infrastructure.PermissionFlags.Detail)]
        public ActionResult<IEnumerable<string>> Get()
        {
            
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            using (var ctx = _contextFactory.Create())
            {
                ctx.Menus.Add(new Menu() { ParentId = 0, Sort = id });
                return Content(ctx.SaveChanges().ToString());
            }
            //return Content(id.ToString());
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
