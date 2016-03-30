using System.Web.Http;
using App.Common;
using App.Identity;
using App.Models;
using Autofac;

namespace App
{
    public partial class Startup
    {
        private static IContainer RegisterServices()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TodoItemRepository>()
                .As<IRepository>()
                .As<IAsyncRepository>();

            builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
                .Where(t => t.Name.EndsWith("Controller"))
                .AsSelf();

            //builder.RegisterInstance(new DomanUserLoginProvider("local"))
            //    .As<ILoginProvider>()
            //    .SingleInstance();

            builder.RegisterInstance(new LocalUserLoginProvider())
                .As<ILoginProvider>()
                .SingleInstance();

            //builder.RegisterType<LocalUserLoginProvider>()
            //    .As<ILoginProvider>()
            //    .SingleInstance();

            return builder.Build();
        } 
        
        public static void ConfigureComposition(HttpConfiguration config)
        {
            IContainer container = RegisterServices();
            config.DependencyResolver = new AutoFacDependencyResolver(container);
        }
    }
}