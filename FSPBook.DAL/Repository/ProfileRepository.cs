using FSPBook.DAL.Repository;
using FSPBook.Data;
using FSPBook.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace FSPBook.DAL.Repository;

public class ProfileRepository : IProfileRepository, IDisposable
{
    private Context _context;
    private DbSet<Profile> _profile;
    public ProfileRepository(Context context)
    {
        _context = context;
        _profile = context.Set<Profile>();
    }

    public void DeleteProfile(Profile post)
    {
        _profile.Remove(post);
    }

    public IEnumerable<Profile> GetAllProfile()
    {
        return _profile.AsEnumerable();
    }

    public Profile GetProfileById(int id)
    {
        return _profile.SingleOrDefault(p => p.Id == id)!;
    }

    public void SaveProfile(Profile post)
    {
        _profile.Add(post);
    }

    public void UpdateProfile(Profile post)
    {
        _profile.Update(post);
    }
    public void BulkSaveProfile(List<Profile> profileList)
    {
        _profile.AddRange(profileList);
    }
    public void Save()
    {
        _context.SaveChanges();
    }
    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }


}

