using CDLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CDSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // check for configs and database
            var appSettingSecretFilename = "AppSettingsSecrets.config";
            var connectionStringsFilename = "ConnectionStrings.config";
            string appSettingSecretPath = Server.MapPath("~/" + appSettingSecretFilename);
            string connectionStringsPath = Server.MapPath("~/" + connectionStringsFilename);
            if (!System.IO.File.Exists(appSettingSecretPath))
            {
                System.IO.File.WriteAllText(appSettingSecretPath, ConfigContent.AppSettingSecretContent);
            }
            if (!System.IO.File.Exists(connectionStringsPath))
            {
                System.IO.File.WriteAllText(connectionStringsPath, ConfigContent.ConnectionStringsContent);
            }
            

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

    public class ConfigContent
    {
        private const string  _appSettingSecretContent = @"
<appSettings>
  <!-- SendGrid account and password -->
  <!-- see Create a secure ASP.NET MVC 5 web app with log in, email confirmation and password reset (C#) | Hook up SendGrid -->
  <!-- https://docs.microsoft.com/en-us/aspnet/mvc/overview/security/create-an-aspnet-mvc-5-web-app-with-email-confirmation-and-password-reset -->
  <add key='mailAccount' value='' />
  <add key='mailPassword' value='' />
</appSettings>
";

        private const string _connectionStringContentTemplate = @"
  <connectionStrings>
    <add name='DefaultConnection' connectionString='Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\{dbName}.mdf;Initial Catalog={dbName};Integrated Security=True'
      providerName='System.Data.SqlClient' />
    <add name='dev' connectionString='Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\{dbName}.mdf;Initial Catalog={dbName};Integrated Security=True' providerName='System.Data.SqlClient' />
    <add name='test' connectionString='Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\{dbName}.mdf;Initial Catalog={dbName};Integrated Security=True' providerName='System.Data.SqlClient' />
    <add name='prod' connectionString='Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\{dbName}.mdf;Initial Catalog={dbName};Integrated Security=True' providerName='System.Data.SqlClient' />
  </connectionStrings>
";
        // cd-20170509022643


        public static string AppSettingSecretContent
        {
            get {
                return _appSettingSecretContent.Replace("'", "\"");
            }
        }

        public static string ConnectionStringsContent
        {
            get
            {
                var content = _connectionStringContentTemplate.Replace("'", "\"");
                var dbName = "CD-@date".Replace("@date", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                content = content.Replace("{dbName}", dbName);
                return content;
            }
        }
    }
}
