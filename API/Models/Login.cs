using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace API.Models
{
    public class Login
    {
        public string MatricNo { get; set; }
        public string Password { get; set; }
    }

    public class DashBoardProfile
    {
        public string FullName { get; set; }
        public string ProgrammeName { get; set; }
        public Byte[] PassportImageData { get; set; }
        public HttpResponseMessage HttpResponse { get; set; }
    }
}