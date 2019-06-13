using AppFramework.AppClasses;
using AppFramework.Linq;
using Premier.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API
{
    public static class Util
    {
        internal static Students studentLogin(String matricno, String password, bool withoutpassword = false)
        {
            var studentexists = new QueryableEntity<Students>().Any(x => x.MatricNo == matricno);

            if (!studentexists)
            {
                return null;
            }

            var latestcompany = (from sp in new QueryableEntity<StudentProgrammes>() where sp.MatricNo == matricno orderby sp.Session descending select sp.Company).ToList().AppFirst();

            if (latestcompany == null)
            {
                throw new Exception("StudentProgramme Not Found for " + matricno);
            }

            String passwordString = DatabaseHandler.DefaultDatabaseHandlerObject.getPasswordString(password);
            //String passwordString2 = AppFramework.AppClasses.Util.md5(password);
            var students = (from student in new QueryableEntity<Students>()
                            join stdprog in new QueryableEntity<StudentProgrammes>()
                            on student.MatricNo equals stdprog.MatricNo
                            where student.MatricNo == matricno &&
                            (student.Password == passwordString || withoutpassword) &&
                            student.Company == latestcompany &&
                            stdprog.Company == latestcompany
                            select student).ToList().AppFirst();

            return students;
        }

    }
}