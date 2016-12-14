using System.Reflection;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Core.Azure.Interfaces;
using Module = Autofac.Module;

namespace AzureWeb.Modules
{
    public class DefaultModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var api = Assembly.GetExecutingAssembly();
            var core = Assembly.GetAssembly(typeof(AzureQueues));

            builder.RegisterControllers(api);
            builder.RegisterApiControllers(api);

            builder.RegisterAssemblyTypes(core).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(api).AsImplementedInterfaces();
        }
    }
}