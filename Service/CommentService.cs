using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using mongodb_example.Context;
using mongodb_example.Interface;
using mongodb_example.Model;

namespace mongodb_example.Service;

public class CommentService : ICommentService
{
    protected IMongoCollection<Article> _collection;

    public CommentService(MongoDBContext context)
    {
        _collection = context.GetCollection<Article>("article");
    }

    public async Task<List<Comment>> Get(string articleId)
    {
        var article = await _collection.Find(Builders<Article>.Filter.Eq(x => x.id, articleId)).FirstOrDefaultAsync();

        return article.comments;
    }

    public async Task<Comment> Create(string articleId, Comment request)
    {
        Comment comment = new Comment(request.comment);

        var result = await _collection.FindOneAndUpdateAsync(
            Builders<Article>.Filter.Eq(x => x.id, articleId),
            Builders<Article>.Update.AddToSet<Comment>(x => x.comments, comment),
            options: new FindOneAndUpdateOptions<Article, Article>
            {
                ReturnDocument = ReturnDocument.After
            }
        );

        return comment;
    }

    public async Task<Comment> Update(string articleId, string commentId, Comment request)
    {
        var filter = Builders<Article>.Filter.And(
            Builders<Article>.Filter.Eq(x => x.id, articleId),
            Builders<Article>.Filter.ElemMatch(x => x.comments, x => x.id == commentId)
        );

        var update = Builders<Article>.Update.Set("comments.$.comment", request.comment);

        var result = await _collection.FindOneAndUpdateAsync(
            filter,
            update,
            options: new FindOneAndUpdateOptions<Article, Article>
            {
                ReturnDocument = ReturnDocument.After
            }
        );

        return result.comments.FirstOrDefault(x => x.id == commentId)!;
    }

    public async Task Delete(string articleId, string commentId)
    {
        var filter = Builders<Article>.Filter.And(
            Builders<Article>.Filter.Eq(x => x.id, articleId),
            Builders<Article>.Filter.ElemMatch(x => x.comments, x => x.id == commentId)
        );

        await _collection.FindOneAndUpdateAsync(
            filter,
            Builders<Article>.Update.PullFilter(x => x.comments, Builders<Comment>.Filter.Eq(x => x.id, commentId))
        );
    }
}