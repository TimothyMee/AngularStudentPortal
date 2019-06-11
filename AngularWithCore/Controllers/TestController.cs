using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithCore.Controllers
{
    [Route("api/[Controller")]
    public class TestController : Controller
    {
        [HttpGet("/api/values")]
        public IEnumerable<string> Get()
        {
            return new string[] { "a", "b", "c", "d" };
        }
    }
}
