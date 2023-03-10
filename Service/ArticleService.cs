using MongoDB.Driver;
using MongoDB.Driver.Linq;

using mongodb_example.Context;
using mongodb_example.Interface;
using mongodb_example.Model;

namespace mongodb_example.Service;

public class ArticleService : IArticleService
{
    protected IMongoCollection<Article> _collection;

    public ArticleService(MongoDBContext context)
    {
        _collection = context.GetCollection<Article>("article");
    }

    public async Task<List<Article>> GetAll()
    {
        return await _collection.Find(Builders<Article>.Filter.Empty).ToListAsync();
    }

    public async Task<Article> GetById(string id)
    {
        return await _collection.Find(Builders<Article>.Filter.Eq("id", id)).FirstOrDefaultAsync();
    }

    public async Task Create(Article request)
    {
        await _collection.InsertOneAsync(request);
    }

    public async Task<Article> Update(string id, Article request)
    {
        return await _collection.FindOneAndUpdateAsync(
            Builders<Article>.Filter.Eq("id", id),
            Builders<Article>.Update.Set("title", request.title).Set("content", request.content),
            options: new FindOneAndUpdateOptions<Article, Article>
            {
                ReturnDocument = ReturnDocument.After
            }
        );
    }

    public async Task Delete(string id)
    {
        await _collection.FindOneAndDeleteAsync(Builders<Article>.Filter.Eq("id", id));
    }
}