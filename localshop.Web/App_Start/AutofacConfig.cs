using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using localshop.Domain;
using localshop.Domain.Abstractions;
using localshop.Domain.Concretes;
using localshop.Domain.Entities;
using localshop.Infrastructures;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace localshop
{
    public class AutofacConfig
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Register maps
            builder.RegisterModule(new AutoMapperModule());

            // Register services
            RegisterServices(builder);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();
            builder.Register(c => c.Resolve<ApplicationDbContext>()).As<DbContext>().InstancePerRequest();

            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationRoleManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();

            builder.Register(c => new UserStore<ApplicationUser>(c.Resolve<DbContext>())).AsImplementedInterfaces().InstancePerRequest();
            builder.Register(c => new RoleStore<ApplicationRole>(c.Resolve<DbContext>())).AsImplementedInterfaces().InstancePerRequest();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();
            builder.Register(c =>  Startup.DataProtectionProvider).InstancePerRequest();
            builder.RegisterType<CityRepository>().As<ICityRepository>().AsSelf().InstancePerRequest();

            builder.RegisterType<ProductRepository>().As<IProductRepository>().AsSelf().InstancePerRequest();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().AsSelf().InstancePerRequest();
            builder.RegisterType<StatusRepository>().As<IStatusRepository>().AsSelf().InstancePerRequest();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().AsSelf().InstancePerRequest();
            builder.RegisterType<ContactRepository>().As<IContactRepository>().AsSelf().InstancePerRequest();
            builder.RegisterType<HomePageRepository>().As<IHomePageRepository>().AsSelf().InstancePerRequest();
            builder.RegisterType<WishlistRepository>().As<IWislistRepository>().AsSelf().InstancePerRequest();
            builder.RegisterType<ReviewRepository>().As<IReviewRepository>().AsSelf().InstancePerRequest();
        }
    }
}