using Xunit;
using Moq;
using FSPBook.DAL.Repository;
using FSPBook.Pages;
using FSPBook.Data;
using FSPBook.Data.Entities;
using Microsoft.Extensions.Configuration;

namespace FSPBook.Test;

public class FSPBookTest
{
    Context db = new Context(Utilities.TestDbContextOptions());

    [Fact]
    public void ProfileSave_Test()
    {

        // Arrange
        ProfileRepository p = new ProfileRepository(db);
        p.SaveProfile(new Data.Entities.Profile { Id = 1, FirstName = "Vivek Raj", LastName = "Elango", JobTitle = "Scientist" });
        p.Save();
        //Assert
        Assert.Equal(1, p.GetProfileById(1).Id);
        Assert.Equal("Vivek Raj", p.GetProfileById(1).FirstName);
        Assert.Equal("Elango", p.GetProfileById(1).LastName);

    }
    [Fact]
    public void ProfileDelete_Test()
    {

        // Arrange
        ProfileRepository p = new ProfileRepository(db);
        p.SaveProfile(new Data.Entities.Profile { Id = 1, FirstName = "Vignesh", LastName = "Raja", JobTitle = "Scientist" });
        p.Save();
        Profile profile = p.GetProfileById(1);
        p.DeleteProfile(profile);
        //Assert
        Assert.Equal(0, db.Post.Count());

    }
    [Fact]
    public void IndexPage_Test()
    {
        //Arrange
        Utilities.AddSampleData(db);
        var inMemorySettings = new Dictionary<string, string> {
                                    {"PageSize", "10"},
                                    {"News_Api", "https://api.thenewsapi.com/v1/news/top"},
                                    {"Api_Token", "gEHCqG79xWQN3rcdlGAKRgEfgwhFx9xiuo2diufN" },
                                    {"Language", "en"},
                                    {"Categories", "tech"},
                                    {"Limit", "5" },
                                    {"Locale", "us,ca,uk"},
                                };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
        IndexModel indexModel = new IndexModel(configuration, db);
        indexModel.OnGetAsync(string.Empty, string.Empty, string.Empty, 1);
        //Assert
        Assert.Equal(10, indexModel.Posts.Count());
    }
}
