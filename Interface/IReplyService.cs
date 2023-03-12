using mongodb_example.Model;

namespace mongodb_example.Interface;

public interface IReplyService
{
    Task<Reply> Create(string articleId, string commentId, Reply request);
    Task Delete(string articleId, string commentId, string replyId);
}