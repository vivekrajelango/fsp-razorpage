using FSPBook.Data;
using FSPBook.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FSPBook.Utilities;
internal static class SeedData
{
    static int NumberOfSamplePosts { get; set; } = 20;
    public static void Seed(IServiceProvider serviceProvider)
    {
        using (var context = new Context(
            serviceProvider.GetRequiredService<
                DbContextOptions<Context>>()))
        {
            if (context.Profile.Count() == 0)
            {
                var profiles = new List<Profile> {
                    new Profile { Id = 1, FirstName = "Jane", LastName = "Doe", JobTitle = "Developer" },
                    new Profile { Id = 2, FirstName = "John", LastName = "Smith", JobTitle = "Consultant" },
                    new Profile { Id = 3, FirstName = "Maisie", LastName = "Jones", JobTitle = "Project Manager" },
                    new Profile { Id = 4, FirstName = "Jack", LastName = "Simpson", JobTitle = "Engagement Officer" },
                    new Profile { Id = 5, FirstName = "Sadie", LastName = "Williams", JobTitle = "Finance Director" },
                    new Profile { Id = 6, FirstName = "Pete", LastName = "Jackson", JobTitle = "Developer" },
                    new Profile { Id = 7, FirstName = "Sinead", LastName = "O'Leary", JobTitle = "Consultant" }
                };
                context.Profile.AddRange(profiles);
                var posts = new List<Post>();
                Random random = new Random();

                for (int i = 1; i <= NumberOfSamplePosts; i++)
                {
                    posts.Add(new Post { Id = i, Content = exampleText, DateTimePosted = new DateTimeOffset(2022, 11, 1, 10, 0, 0, TimeSpan.Zero), AuthorId = random.Next(1, 7) });
                }
                context.Post.AddRange(posts);

                context.SaveChanges();
            }
        }
    }

    private static string exampleText = @"Lorem Ipsum is simply dummy text of the printing and typesetting 
        industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an 
        unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived 
        not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.";
}
