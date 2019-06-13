using AppFramework.AppClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace API
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            loadWebLoadedAssemblies();

            start();
            GlobalConfiguration.Configuration.EnsureInitialized();

        }

        private List<Assembly> loadWebLoadedAssemblies()
        {
            List<Assembly> ret = new List<Assembly>();
            string webloadeddllsstring = WebConfigurationManager.AppSettings["WebLoadedAssemblies"];
            if (!string.IsNullOrWhiteSpace(webloadeddllsstring))
            {
                string[] webloadeddlls = webloadeddllsstring.Split(new char[] { ',', ' ', '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (webloadeddlls.Length == 0)
                {
                    webloadeddlls = new string[] { webloadeddllsstring };
                }
                //throw new Exception(webloadeddlls[0]+"|"+webloadeddlls[1]);
                using (var webClient = new WebClient())
                {
                    foreach (var webloadeddll in webloadeddlls)
                    {
                        string temp = webloadeddll.Trim();
                        string temppdb = temp.Replace(".dll", ".pdb");

                        try
                        {
                            byte[] assemblybytes = webClient.DownloadData(temp);
                            byte[] pdbbytes = null;
                            try
                            {
                                pdbbytes = webClient.DownloadData(temppdb);
                            }
                            catch (Exception exx)
                            {
                                //we can't use this to log it as this would require loading of appframework
                                //Infolog.writeToEventLog(new Exception(string.Format("Unable to load pdb at '{0}'", temppdb), exx), InfoType.Warning);
                            }

                            Assembly assembly;
                            if (pdbbytes == null)
                            {
                                assembly = Assembly.Load(assemblybytes);
                            }
                            else
                            {
                                assembly = Assembly.Load(assemblybytes, pdbbytes);
                            }

                            ret.Add(assembly);
                            //throw new Exception(assembly.FullName);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(string.Format("Unable to load assembly at '{0}'", temp), ex);
                        }
                    }
                }
                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            }
            return ret;
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
            if (assembly == null)
            {
                assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == args.Name);
            }
            if (assembly != null)
            {
                return assembly;
            }
            else
            {
                return null;
            }
        }

        private static void start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AppSettings.ExternalUserEnviroment = true;

            AppSettings.MainAssembly = System.Reflection.Assembly.Load("Premier");
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            Util.connect();
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            try
            {
                DatabaseHandler.DefaultDatabaseHandlerObject.Dispose();
            }
            catch { }
        }


        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            Util.connect();

            //string[]  configlines;
            //configlines = File.ReadAllLines(HttpRuntime.AppDomainAppPath + "\\bin\\config.cfg");

            //string userName = configlines[6];
            //string passWord = configlines[7];

            //Util.connect(userName, passWord);
        }

        protected void Application_Error()
        {
            try
            {
                //Response.Write("Ending");
                DatabaseHandler.DefaultDatabaseHandlerObject.rollBackTransaction();
            }
            catch { }

            try
            {
                DatabaseHandler.DefaultDatabaseHandlerObject.Dispose();
            }
            catch { }

            try
            {
                Exception ex = Context.Error;

                if (ex != null)
                {
                    Infolog.writeToEventLog(ex,InfoType.Error);
                    //if (AppFramework.AppClasses.Session.User != null)
                    //{
                    //    Context.ClearError();
                    //    Response.Redirect("~/Menu/Dashboard");
                    //}
                }
            }
            catch { }
        }
    }
}
