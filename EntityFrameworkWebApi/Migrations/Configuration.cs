namespace EntityFrameWorkWebApi.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EntityFrameWorkWebApi.Models.EntityFrameWorkWebApiContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EntityFrameWorkWebApi.Models.EntityFrameWorkWebApiContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Genres.AddOrUpdate(new Genre[]
            {
                new Genre() { GenreId = 1, Name = "Anime" },
                new Genre() { GenreId = 2, Name = "Classics" },
                new Genre() { GenreId = 3, Name = "Dacumentary" },
                new Genre() { GenreId = 4, Name = "Sci-Fi" }
            });

            context.Movies.AddOrUpdate(new Movie[]
            {
                new Movie() {
                    MovieId = 1,
                    Title = "Gost In The Cell",
                    Description = "Japaness media franchise originally published as a seinen manga series of the same name.",
                    GenreId = 1 },
                new Movie()
                {
                    MovieId = 2,
                    Title = "Robotech",
                    Description = "An alien ship crashes on Earth and the secrets it bears lead Earth into three interplanetary wars.",
                    GenreId = 1
                },
                new Movie()
                {
                    MovieId = 3,
                    Title = "A Trip to the Moon",
                    Description = "This silent short tells the story of an astronomer and his colleagues who journey to the moon.",
                    GenreId = 2
                },
                new Movie()
                {
                    MovieId = 4,
                    Title = "Edge of the Universe",
                    Description = "Leading astronomers reveal the latest discoveries about the cosmos.",
                    GenreId = 3
                },
                new Movie()
                {
                    MovieId = 5,
                    Title = "2001: A Space Odyssey",
                    Description = "Story follows the ascent of mankind into the near future space age through minimalist performance and a strong visual style.",
                    GenreId = 4
                }
            });
        }
    }
}
