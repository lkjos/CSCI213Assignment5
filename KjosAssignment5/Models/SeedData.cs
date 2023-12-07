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
                        Title = "Radioactive",
                        ReleaseDate = DateTime.Parse("2012-10-29"),
                        Genre = "Rock",
                        Artist = "Imagine Dragons",
                        Price = 15.00M
                    },
                    new Song
                    {
                        Title = "Locked out of Heaven",
                        ReleaseDate = DateTime.Parse("2012-10-01"),
                        Genre = "Pop",
                        Artist = "Bruno Mars",
                        Price = 20.00M
                    },
                    new Song
                    {
                        Title = "Little Talks",
                        ReleaseDate = DateTime.Parse("2011-12-20"),
                        Genre = "Alternative",
                        Artist = "Of Monsters and Men",
                        Price = 12.50M
                    },
                    new Song
                    {
                        Title = "Stars",
                        ReleaseDate = DateTime.Parse("2022-8-26"),
                        Genre = "Hip-hop",
                        Artist = "JID",
                        Price = 15.00M
                    },
                    new Song
                    {
                        Title = "What You Know",
                        ReleaseDate = DateTime.Parse("2011-2-7"),
                        Genre = "Alternative",
                        Artist = "Two Door Cinema Club",
                        Price = 10.00M
                    },
                    new Song
                    {
                        Title = "Black Hole Sun",
                        ReleaseDate = DateTime.Parse("1994-3-8"),
                        Genre = "Rock",
                        Artist = "Soundgarden",
                        Price = 17.50M
                    },
                    new Song
                    {
                        Title = "Chandelier",
                        ReleaseDate = DateTime.Parse("2014-3-17"),
                        Genre = "Pop",
                        Artist = "Sia",
                        Price = 20.00M
                    },
                    new Song
                    {
                        Title = "LOYALTY",
                        ReleaseDate = DateTime.Parse("2017-7-28"),
                        Genre = "Hip-hop",
                        Artist = "Kendrick Lamar",
                        Price = 20.00M
                    }
                );
                context.SaveChanges();
            }
        }
    }

}
