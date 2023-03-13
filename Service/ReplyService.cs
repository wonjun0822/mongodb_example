using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using mongodb_example.Context;
using mongodb_example.Interface;
using mongodb_example.Model;

namespace mongodb_example.Service;

public class ReplyService : IReplyService
{
    protected IMongoCollection<Article> _collection;

    public ReplyService(MongoDBContext context)
    {
        _collection = context.GetCollection<Article>("article");
    }

    public async Task<Reply> Create(string articleId, string commentId, Reply request)
    {
        Reply reply = new Reply(request.comment);

        var filter = Builders<Article>.Filter.And(
            Builders<Article>.Filter.Eq(x => x.id, articleId),
            Builders<Article>.Filter.ElemMatch(x => x.comments, x => x.id == commentId)
        );

        var update = Builders<Article>.Update.Push("comments.$.replys", reply);

        var result = await _collection.FindOneAndUpdateAsync(
            filter,
            update,
            options: new FindOneAndUpdateOptions<Article, Article>
            {
                ReturnDocument = ReturnDocument.After
            }
        );

        return reply;
    }

    public async Task<Reply> Update(string articleId, string commentId, string replyId, Reply request)
    {
        var filter = Builders<Article>.Filter.And(
            Builders<Article>.Filter.Eq(x => x.id, articleId),
            Builders<Article>.Filter.ElemMatch(x => x.comments, Builders<Comment>.Filter.ElemMatch(x => x.replys, x => x.id == replyId))
        );

        var update = Builders<Article>.Update.Set("comments.$[c].replys.$[r].comment", request.comment);

        var arrayFilter = new List<ArrayFilterDefinition> {
            new BsonDocumentArrayFilterDefinition<Comment>(new BsonDocument("c._id", new ObjectId(commentId))),
            new BsonDocumentArrayFilterDefinition<Reply>(new BsonDocument("r._id", new ObjectId(replyId)))
        };

        var result = await _collection.FindOneAndUpdateAsync(
            filter,
            update,
            options: new FindOneAndUpdateOptions<Article, Article>
            {
                ArrayFilters = arrayFilter,
                ReturnDocument = ReturnDocument.After
            }
        );

        return result.comments.Select(x => x.replys.Where(s => s.id == replyId).FirstOrDefault()).FirstOrDefault()!;
    }

    public async Task Delete(string articleId, string commentId, string replyId)
    {
        var filter = Builders<Article>.Filter.And(
            Builders<Article>.Filter.Eq(x => x.id, articleId),
            Builders<Article>.Filter.ElemMatch(x => x.comments, x => x.id == commentId)
        );

        var update = Builders<Article>.Update.PullFilter("comments.$.replys", Builders<Reply>.Filter.Eq(x => x.id, replyId));

        await _collection.FindOneAndUpdateAsync(
            filter,
            update
        );
    }
}