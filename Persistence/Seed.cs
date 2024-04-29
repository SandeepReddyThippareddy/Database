using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context,
            UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any() && !context.Events.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "Aditya",
                        UserName = "aditya",
                        Email = "aditya@gmail.com"
                    },
                    new AppUser
                    {
                        DisplayName = "Sandeep Reddy",
                        UserName = "sandeep",
                        Email = "sandeep@gmail.com"
                    }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "123456789");
                }

                var events = new List<Event> {
                 new()
                    {
                        Title = "Hackathon 2022",
                        Date = DateTime.UtcNow.AddMonths(-2),
                        Description = "Annual hackathon event",
                        Category = "technology",
                        City = "Atlanta",
                        Venue = "Georgia Tech Campus",
                    },
                    new()
                    {
                        Title = "Art Exhibition",
                        Date = DateTime.UtcNow.AddMonths(-1),
                        Description = "Art showcase by local artists",
                        Category = "art",
                        City = "Atlanta",
                        Venue = "Georgia Tech Student Center",
                    },
                    new()
                    {
                        Title = "Guest Lecture Series",
                        Date = DateTime.UtcNow.AddMonths(1),
                        Description = "Industry experts share insights",
                        Category = "education",
                        City = "Atlanta",
                        Venue = "Georgia Tech Global Learning Center",
                    },
                    new()
                    {
                        Title = "Music Festival",
                        Date = DateTime.UtcNow.AddMonths(2),
                        Description = "Live music performances",
                        Category = "music",
                        City = "Atlanta",
                        Venue = "Bobby Dodd Stadium",
                    },
                    new()
                    {
                        Title = "Career Fair",
                        Date = DateTime.UtcNow.AddMonths(3),
                        Description = "Connect with top employers",
                        Category = "career",
                        City = "Atlanta",
                        Venue = "McCamish Pavilion",
                    },
                    new()
                    {
                        Title = "Startup Showcase",
                        Date = DateTime.UtcNow.AddMonths(4),
                        Description = "Showcase of student startups",
                        Category = "business",
                        City = "Atlanta",
                        Venue = "Georgia Tech Advanced Technology Development Center",
                    },
                    new() {
                        Title = "Sports Tournament",
                        Date = DateTime.UtcNow.AddMonths(5),
                        Description = "Inter-university sports competition",
                        Category = "sports",
                        City = "Atlanta",
                        Venue = "Georgia Tech Campus Recreation Center",
                    }
                };
               

                await context.Events.AddRangeAsync(events);
                await context.SaveChangesAsync();
            }
        }
    }
}
