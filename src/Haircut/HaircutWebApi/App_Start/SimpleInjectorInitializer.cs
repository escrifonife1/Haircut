[assembly: WebActivator.PostApplicationStartMethod(typeof(HaircutWebApi.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace HaircutWebApi.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;

    using SimpleInjector;
    using SimpleInjector.Extensions;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using Haircut.Database.Contract;
    using Haircut.Database.Repository;
    using SimpleInjector.Integration.WebApi;
    using Microsoft.Owin.Security.DataHandler.Serializer;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.DataProtection;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            
            InitializeContainer(container);

            //container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterWebApiControllers(System.Web.Http.GlobalConfiguration.Configuration);


            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {            
            //container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            container.Register<ILoginRepository, LoginRepository>(Lifestyle.Scoped);
            container.Register<IDataSerializer<AuthenticationTicket>, TicketSerializer>();
            container.Register<IDataProtector>(() => new DpapiDataProtectionProvider().Create("ASP.NET Identity"));
            

            // This is an extension method from the integration package.            

            // For instance:
            // container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
        }
    }
}