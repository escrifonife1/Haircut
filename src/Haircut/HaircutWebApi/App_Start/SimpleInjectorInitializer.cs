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
    using System;
    using HaircutWebApi.Models;
    using Owin;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security.DataHandler;
    using Microsoft.Owin.Security.DataHandler.Encoder;
    using SimpleInjector.Diagnostics;
    using System.Linq;
    using System.Web.Http;

    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());            
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.RegisterMvcIntegratedFilterProvider();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            try
            {
                container.Verify();
            }
            catch(Exception ex)
            {
                var msg = ex.ToString();
            }

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {
            //container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            //container.Register(typeof(ISecureDataFormat<>), typeof(SecureDataFormat<>));
            //container.RegisterCollection(typeof(ISecureDataFormat<AuthenticationTicket>), 
            //    new[] 
            //    {
            //        typeof(SecureDataFormat<AuthenticationTicket>),
            //        typeof(TicketDataFormat)
            //    });
            container.Register<ISecureDataFormat<AuthenticationTicket>, SecureDataFormat<AuthenticationTicket>>();
            container.RegisterWebApiRequest<ITextEncoder, Base64UrlTextEncoder>();
            //container.Register<ISecureDataFormat<AuthenticationTicket>, TicketDataFormat>();
            container.Register<IDataSerializer<AuthenticationTicket>, TicketSerializer>();

            container.RegisterWebApiRequest<HttpConfiguration>(
                () => new HttpConfiguration() { DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container) });
            container.RegisterWebApiRequest<ApplicationUserManager>();
            container.Register<IDataProtector>(() => new DpapiDataProtectionProvider().Create("ASP.NET Identity"));

            container.RegisterWebApiRequest(() => new ApplicationDbContext());
            container.RegisterWebApiRequest<IUserStore<
              ApplicationUser>>(() =>
                new UserStore<ApplicationUser>(
                  container.GetInstance<ApplicationDbContext>()));

            container.RegisterInitializer<ApplicationUserManager>(manager => InitializeUserManager(manager));
            
            container.Register<IUserStore<AppUser, string>>(() => new UserStore<AppUser>(), Lifestyle.Scoped);
            container.Register<UserManager<AppUser, string>>(() => new UserManager<AppUser, string>(new UserStore<AppUser>()), Lifestyle.Scoped);

            // Register your types, for instance using the scoped lifestyle:
            container.Register<DatabaseContext>(Lifestyle.Scoped);
            container.Register<ILoginRepository, LoginRepository>(Lifestyle.Scoped);
            container.Register<IScheduleRepository, ScheduleRepository>(Lifestyle.Scoped);
            container.Register<IHairdresserRepository, HairdresserRepository>(Lifestyle.Scoped);
            container.Register<IBarbershopRepository, BarbershopRepository>(Lifestyle.Scoped);

            // For instance:
            // container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);

            
        }

        private static void InitializeUserManager(
            ApplicationUserManager manager)
        {
            manager.UserValidator =
             new UserValidator<ApplicationUser>(manager)
             {
                 AllowOnlyAlphanumericUserNames = false,
                 RequireUniqueEmail = true
             };

            //Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator()
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
             
                manager.UserTokenProvider =
                 new DataProtectorTokenProvider<ApplicationUser>(
                  new DpapiDataProtectionProvider().Create("ASP.NET Identity"));
            
        }
    }

    public class AppUser : IdentityUser
    {
        public string Country { get; set; }
    }
}