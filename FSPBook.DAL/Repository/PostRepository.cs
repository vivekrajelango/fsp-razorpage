using FSPBook.Data;
using FSPBook.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace FSPBook.DAL.Repository;

public class PostRepository : IPostRepository, IDisposable
{
    private Context _context;
    private DbSet<Post> _post;
    public PostRepository(Context context)
    {
        _context = context;
        _post = context.Set<Post>();
    }
    public void BulkSavePost(List<Post> PostList)
    {
        _post.AddRange(PostList);
    }

    public void DeletePost(Post post)
    {
        _post.Remove(post);
    }

    public IEnumerable<Post> GetAllPosts()
    {
        return _post.AsEnumerable();
    }

    public Post GetPostById(int id)
    {
        return _post.SingleOrDefault(p => p.Id == id)!;
    }

    public void SavePost(Post post)
    {
        _post.Add(post);
    }

    public void UpdatePost(Post post)
    {
        _post.Update(post);
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

