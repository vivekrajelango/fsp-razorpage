using FSPBook.Data;
using FSPBook.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace FSPBook.Test;

public class Utilities
{
    public static int NumberOfSamplePosts = 20;
    public static DbContextOptions<Context> TestDbContextOptions()
    {
        // Create a new service provider to create a new in-memory database.
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        // Create a new options instance using an in-memory database and 
        // IServiceProvider that the context should resolve all of its 
        // services from.
        var builder = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("FSPBookDataBase")
            .UseInternalServiceProvider(serviceProvider);

        return builder.Options;
    }
    public static void AddSampleData(Context context)
    {
            
        string exampleText = @"Lorem Ipsum is simply dummy text of the printing and typesetting 
    industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an 
    unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived 
    not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.";

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

