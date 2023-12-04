using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using KjosAssignment5.Data;
using System;
using System.Linq;


namespace KjosAssignment5.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new KjosAssignment5Context(
                serviceProvider.GetRequiredService<
                    DbContextOptions<KjosAssignment5Context>>()))
            {
                // Look for any songs.
                if (context.Song.Any())
                {
                    return;   // DB has been seeded
                }
                context.Song.AddRange(
                    new Song
                    {
                        Title = "test1",
                        ReleaseDate = DateTime.Parse("2000-1-1"),
                        Genre = "Pop",
                        Artist = "test1",
                        Price = 20.00M
                    },
                    new Song
                    {
                        Title = "test2",
                        ReleaseDate = DateTime.Parse("2000-1-1"),
                        Genre = "Rock",
                        Artist = "test2",
                        Price = 30.00M
                    },
                    new Song
                    {
                        Title = "test3",
                        ReleaseDate = DateTime.Parse("2000-1-1"),
                        Genre = "Country",
                        Artist = "test3",
                        Price = 15.00M
                    },
                    new Song
                    {
                        Title = "test4",
                        ReleaseDate = DateTime.Parse("2000-1-1"),
                        Genre = "Hip-hop",
                        Artist = "test4",
                        Price = 20.00M
                    }
                );
                context.SaveChanges();
            }
        }
    }

}
