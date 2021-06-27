namespace localshop.Domain.Migrations
{
    using localshop.Domain.Concretes;
    using localshop.Domain.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<localshop.Domain.Concretes.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(localshop.Domain.Concretes.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            SeedRootUserAndRoles(context);
            SeedOrderStatuses(context);
            SeedPaymentMethod(context);

            context.SaveChanges();
        }

        public static void SeedOrderStatuses(ApplicationDbContext context)
        {
            var orderStatuses = new OrderStatus[]
            {
                new OrderStatus { Id="f9d10000-d769-34e6-4e67-08d7b48f1d56", Name="Pending" },
                new OrderStatus { Id="f9d10000-d769-34e6-d603-08d7b94c7194", Name="Approved" },
                new OrderStatus { Id="f9d10000-d769-34e6-a60e-08d7b48f1d56", Name="Delivered" },
                new OrderStatus { Id="f9d10000-d769-34e6-a9d0-08d7b48f1d56", Name="Cancelled" }
            };
            context.OrderStatuses.AddOrUpdate(orderStatuses);
        }

        public static void SeedPaymentMethod(ApplicationDbContext context)
        {
            var paymentMethods = new PaymentMethod[]
            {
                new PaymentMethod { Id="f9d10000-d769-34e6-aadb-08d7b492a03e", Name="Cash on delivery" },
                new PaymentMethod { Id="f9d10000-d769-34e6-0077-08d7b492a03f", Name="Direct bank transfer" }
            };
            context.PaymentMethods.AddOrUpdate(paymentMethods);
        }

        public static void SeedRootUserAndRoles(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));

            string roleName = "root";
            string password = "Admin123@localshop.hau";
            string email = "admin@localshop.com";

            string[] seedRoles = { "administrator", "modifier", "customer" };
            foreach (var role in seedRoles)
            {
                if (!roleManager.RoleExists(role))
                {
                    roleManager.Create(new ApplicationRole(role));
                }
            }

            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new ApplicationRole(roleName));
            }

            var userTemp = userManager.FindByName(email);
            if (userTemp == null)
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Hau",
                    LastName = "Nguyen Dinh",
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                }, password);

                userTemp = userManager.FindByName(email);
            }

            if (!userManager.IsInRole(userTemp.Id, roleName))
            {
                userManager.AddToRole(userTemp.Id, roleName);
            }
        }
    }
}
