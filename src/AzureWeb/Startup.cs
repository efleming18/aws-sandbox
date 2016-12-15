using System.Web.Http;
using Autofac.Integration.WebApi;
using Owin;

namespace AzureWeb
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = ContainerConfig.Configure();
            Bootstrap.CreateKnownAzureQueues();

            var config = ConfigureWebApi(app);
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacWebApi(config);
            app.UseAutofacMiddleware(container);
            app.UseWebApi(config);
        }

        private HttpConfiguration ConfigureWebApi(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);

            return config;
        }
    }
}
