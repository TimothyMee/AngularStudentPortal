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
        // GET: api/Login
        //[HttpGet]
        public Response Login(Login Lg)
        {
            //Lg = new Login();
            //Lg.Username = "110408010";
            //Lg.Password = "pass";
            //Students students = new Students();
            var student = Util.studentLogin(Lg.MatricNo, Lg.Password, true); 
            if (student == null)
                return new Response { Status = "Invalid", Message = "Invalid User." };
            else
                return new Response { Status = "Success", Message = Lg.MatricNo };
        }



    }
}
 