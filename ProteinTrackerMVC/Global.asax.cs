using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Funq;
using ProteinTrackerMVC.Api;
using ServiceStack.Redis;
using ServiceStack.WebHost.Endpoints;

namespace ProteinTrackerMVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            new ProteinTrackerAppHost().Init();
        }
    }

    public class ProteinTrackerAppHost : AppHostBase
    {
        public ProteinTrackerAppHost() : base("Protein Tracker Web Services", typeof(HelloService).Assembly) {}

        public override void Configure(Container container)
        {
            SetConfig(new EndpointHostConfig { ServiceStackHandlerFactoryPath = "api" });

            container.Register<IRedisClientsManager>(c => new PooledRedisClientManager());
            container.Register<IRepository>(c => new Repository(c.Resolve<IRedisClientsManager>()));
        }
    }

}