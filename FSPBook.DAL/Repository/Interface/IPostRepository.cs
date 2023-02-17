using FSPBook.Data.Entities;


namespace FSPBook.DAL.Repository;

public interface IPostRepository
{
    void SavePost(Post post);

    void BulkSavePost(List<Post> PostList);
    void UpdatePost(Post post);
    IEnumerable<Post> GetAllPosts();
    Post GetPostById(int id);
    void DeletePost(Post post);
    void Save();
}

