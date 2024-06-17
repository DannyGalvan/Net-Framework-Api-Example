namespace NetApi.Migrations
{
    using System.Data.Entity.Migrations;
    using Models;
    using BC = BCrypt.Net;
    using Context;

    internal sealed class Configuration : DbMigrationsConfiguration<ApiContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "NetApi.Context.ApiContext";
        }

        protected override void Seed(ApiContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Users.AddOrUpdate(
                            u => u.UserName,
                            new User
                            {
                                Email = "pruebas.test29111999@gmail.com",
                                Name = "SA",
                                LastName = "ADMIN",
                                Number = "51995142",
                                UserName = "MANAGER",
                                Password = BC.BCrypt.HashPassword("admin"),
                                Active = true,
                                Confirm = true,
                            }
                       );

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
