using FSPBook.Data.Entities;


namespace FSPBook.DAL.Repository;

public interface IProfileRepository
{
    void SaveProfile(Profile post);
    void BulkSaveProfile(List<Profile> profileList);
    void UpdateProfile(Profile post);
    IEnumerable<Profile> GetAllProfile();
    Profile GetProfileById(int id);
    void DeleteProfile(Profile post);
    void Save();
}

