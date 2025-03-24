using System.Net.Http;
using CatTinder.Models;
using Microsoft.EntityFrameworkCore;

namespace CatTinder.Data
{
    public static class DataSeeder
    {
        private static readonly string[] CatNames =
        {
            "Whiskers", "Fluffy", "Mittens", "Shadow", "Luna", "Simba", "Chloe", "Oscar", "Cleo", "Felix"
        };

        private static readonly string[] Descriptions =
        {
            "Loves belly rubs and naps.",
            "Will judge you from across the room.",
            "Professional laser dot hunter.",
            "Very vocal and affectionate.",
            "Can open doors. Be warned.",
            "Queen/King of the couch.",
            "Will steal your food.",
            "Sweet, but sassy.",
            "Loves to climb everything.",
            "Sleeps 23 hours a day."
        };

        public static async Task SeedCatsAsync(ApplicationDbContext context)
        {
            if (context.Cats.Any())
            {
                context.Cats.RemoveRange(context.Cats);
                await context.SaveChangesAsync();

                // Reset ID counter
                await context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Cats', RESEED, 0)");
            }


            if (!context.Cats.Any())
            {
                var cats = new List<Cat>();
                var random = new Random();

                using var httpClient = new HttpClient();

                for (int i = 0; i < 30; i++) // Adjust quantity
                {
                    byte[] imageBytes = await GetRandomCatImage(httpClient);

                    var cat = new Cat
                    {
                        Name = CatNames[random.Next(CatNames.Length)],
                        Description = Descriptions[random.Next(Descriptions.Length)],
                        Image = imageBytes
                    };

                    cats.Add(cat);
                }

                context.Cats.AddRange(cats);
                await context.SaveChangesAsync();
            }
        }

        private static async Task<byte[]> GetRandomCatImage(HttpClient client)
        {
            try
            {
                // Cataas returns a new cat image every time
                var url = "https://cataas.com/cat?width=400&height=400";
                return await client.GetByteArrayAsync(url);
            }
            catch
            {
                // Fallback: return an empty byte array if download fails
                return Array.Empty<byte>();
            }
        }
    }
}
