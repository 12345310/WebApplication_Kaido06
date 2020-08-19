namespace WebApplication_Kaido06.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApplication_Kaido06.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication_Kaido06.Models.MovieDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WebApplication_Kaido06.Models.MovieDBContext";
        }

        protected override void Seed(WebApplication_Kaido06.Models.MovieDBContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Movies.AddOrUpdate(i => i.Title,
    new Movie
    {
        Title = "When Harry Met Sally",
        ReleaseDate = DateTime.Parse("1989-1-11"),
        Genre = "Romantic Comedy",
        Rating = "PG",
        Price = 7.99M
    },

     new Movie
     {
         Title = "Ghostbusters ",
         ReleaseDate = DateTime.Parse("1984-3-13"),
         Genre = "Comedy",
         Rating = "PG",
         Price = 8.99M
     },

     new Movie
     {
         Title = "Ghostbusters 2",
         ReleaseDate = DateTime.Parse("1986-2-23"),
         Genre = "Comedy",
         Rating = "PG",
         Price = 9.99M
     },

   new Movie
   {
       Title = "Rio Bravo",
       ReleaseDate = DateTime.Parse("1959-4-15"),
       Genre = "Western",
       Rating = "PG",
       Price = 3.99M
   }


);
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
