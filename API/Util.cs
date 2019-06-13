using AppFramework.AppClasses;
using AppFramework.Linq;
using Premier.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace API
{
    public static class Util
    {
        internal static Students studentLogin(String matricno, String password, bool withoutpassword = false)
        {
            var studente = (from s in new QueryableEntity<Students>() where s.MatricNo==matricno select s ).First();
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

        internal static void connect()
        {
            //String username, String password
            AppSettings.GetFieldAndTableInfoFromDatabase = false;
            AppSettings.SeparateDefaultDBHandlerForHTTPRequests = true;
            AppSettings.SeparateSessionForHTTPRequests = true;
            AppSettings.LinkInfoIndexCheckOnStartup = false;
            AppSettings.EnableAlerts = false;
            AppSettings.UseVirtualDelete = true;
            //AppSettings.MainAssembly = "";
            //AppSettings.FileServerLocation = "";



            //if (AppFramework.AppClasses.Util.IsRunningInMono)
            //{
            //    AppFramework.AppClasses.AppSettings.MainAssembly = System.Reflection.Assembly.LoadFrom(HttpRuntime.AppDomainAppPath + "bin/BursaryEntities.dll");
            //}
            //else
            //{
            //    AppFramework.AppClasses.AppSettings.MainAssembly = System.Reflection.Assembly.LoadFrom(HttpRuntime.AppDomainAppPath + "\\bin\\BursaryEntities.dll");
            //}

            // Turn Off Client Side Validation
            //HtmlHelper.ClientValidationEnabled = false;
            //HtmlHelper.UnobtrusiveJavaScriptEnabled = false;



            AppFramework.AppClasses.AppSettings.QueryTimeOut = 1200;


            string username = WebConfigurationManager.AppSettings["PremierUsername"];
            string password = WebConfigurationManager.AppSettings["PremierPassword"];
            //tranzgateusername = WebConfigurationManager.AppSettings["TranzgateUsername"];
            //tranzgatepassword = WebConfigurationManager.AppSettings["TranzgatePassword"];
            //AppSettings.ServerIP = configlines[0];
            //AppSettings.Port = int.Parse(configlines[1]);




            //AppSettings.FileServerLocation = "\\\\localhost\\BursaryInstallation\\";
            //if (DatabaseHandler.DefaultDatabaseHandlerObject == null)

            //Bursary.Program_DLL.setupClient();


            String[] configlines = new string[] { WebConfigurationManager.AppSettings["DatabaseHost"], WebConfigurationManager.AppSettings["DatabaseName"], WebConfigurationManager.AppSettings["DatabaseUser"], WebConfigurationManager.AppSettings["DatabasePassword"] };

            DatabaseHandler.DefaultDatabaseHandlerObject = new AppFramework.AppClasses.MySqlClientSideDatabaseHandler(configlines);
            //AppSettings.FileServerLocation = configlines[4];


            //string userName = configlines[5];
            //string passWord = configlines[6];

            //AppSettings.LicenseEncryptionPublicKey = new StreamReader(AppFramework.AppClasses.Util.getAssemblyResourceFileStream(AppFramework.AppClasses.AppSettings.MainAssembly, "Premier.PublicKey.key")).ReadToEnd();



            //string[] configlines;

            //throw new Exception("Create database connection here. You can change the database handler");
            DatabaseHandler.DefaultDatabaseHandlerObject = new AppFramework.AppClasses.MySqlClientSideDatabaseHandler(configlines);
            AppFramework.AppClasses.Session.createNewSession(username, password);

            if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["appframeworkcompany"] == null && HttpContext.Current.Session != null && !String.IsNullOrWhiteSpace(Session.Company))
            {
                HttpContext.Current.Session["appframeworkcompany"] = Session.Company;
            }

            if (DatabaseHandler.DefaultDatabaseHandlerObject != null && HttpContext.Current != null && HttpContext.Current.Session != null && !String.IsNullOrWhiteSpace(HttpContext.Current.Session["appframeworkcompany"] as String) && AppFramework.AppClasses.Session.Company != (String)HttpContext.Current.Session["appframeworkcompany"])
            {
                AppFramework.AppClasses.Session.changeCompany((String)HttpContext.Current.Session["appframeworkcompany"]);
            }
        }

    }
}