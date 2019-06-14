using API.Models;
using AppFramework.Linq;
using Premier.EntityClasses;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class MenuController : ApiController
    {
        [HttpGet]
        [Route("api/menu/dashboardprofile/{MatricNo}")]
        public DashBoardProfile DashBoardProfile(string MatricNo)
        {
            //Lg = new Login();
            //Lg.Username = "110408010";
            //Lg.Password = "pass";
            //Students students = new Students();
            var student = Util.GetStudent(MatricNo);
            if (student == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NoContent)
                {
                    ReasonPhrase = "Student Not Found",
                };

                return new DashBoardProfile { HttpResponse = response };
            }
            var studentProgrammeName = student.getCurrentStudentProgramme().Programme.ProgrammeName;
            var passportImageData = (from sp in new QueryableEntity<StudentPassports>() where sp.MatricNo == student.MatricNo select sp.PassportData).ToList().AppFirst();
            ;
            return new DashBoardProfile { FullName = student.FullName, PassportImageData = passportImageData, ProgrammeName = studentProgrammeName };
        }

    }
}
