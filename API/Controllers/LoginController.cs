using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;
using Premier.EntityClasses;
using AppFramework;

namespace API.Controllers
{
    public class LoginController : ApiController
    {
        // POST: api/Login
        [Route("Api/Login/UserLogin")]
        [HttpPost]
        public Response Login(Login Lg)
        {
            Students students = new Students();
            var student = Util.studentLogin(Lg.Username, Lg.Password, true); ;
            if (student == null)
                return new Response { Status = "Invalid", Message = "Invalid User." };
            else
                return new Response { Status = "Success", Message = Lg.Username };
        }
    }
}
