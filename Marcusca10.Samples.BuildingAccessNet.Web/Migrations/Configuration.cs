namespace Marcusca10.Samples.BuildingAccessNet.Web.Migrations
{
    using Marcusca10.Samples.BuildingAccessNet.Web.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Marcusca10.Samples.BuildingAccessNet.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Marcusca10.Samples.BuildingAccessNet.Web.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            // Set initial service paramenters
            if (context.ServiceParameters.Count() < 1)
            {
                context.ServiceParameters.Add(
                    new ServiceParametersModel()
                    {
                        Id = Guid.Parse("b49f7c30-6e94-410f-a748-4972a39e9f71"),
                        IsFirstRun = true,
                        EnableRegistration = true
                    });
            }

            // Create base roles
            context.Roles.AddOrUpdate(new IdentityRole() { Id = "5958c790-39e6-44c7-a5e2-32b292f3a375", Name = "Owner" });
            context.Roles.AddOrUpdate(new IdentityRole() { Id = "ef19893e-f3be-4f8f-97c7-6197739c9743", Name = "Admin" });
        }
    }
}
