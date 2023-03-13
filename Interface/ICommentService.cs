using mongodb_example.Model;

namespace mongodb_example.Interface;

public interface ICommentService
{
    Task<List<Comment>> Get(string articleId);
    Task<Comment> Create(string articleId, Comment request);
    Task<Comment> Update(string articleId, string commentId, Comment request);
    Task Delete(string articleId, string commentId);
}