using Autofac;
using AzureWeb.Modules;

namespace AzureWeb
{
    public class ContainerConfig
    {
        public static IContainer Configure()
        {
            //AutoFac Setup
            var builder = new ContainerBuilder();

            builder.RegisterModule(new DefaultModule());

            var container = builder.Build();
            return container;
        }
    }
}