using MongoDB.Bson;
using MongoDB.Driver;
using mongodb_example.Context;
using mongodb_example.Interface;
using mongodb_example.Model;

namespace mongodb_example.Service;

public abstract class Service<TEntity> : IService<TEntity> where TEntity : class
{
    protected IMongoCollection<TEntity> _collection;
    
    protected Service(MongoDBContext context) {
        _collection = context.GetCollection<TEntity>(typeof(TEntity).Name.ToLower());
    }

    public virtual async Task<List<TEntity>> GetAll()
    {
        return await _collection.Find(Builders<TEntity>.Filter.Empty).ToListAsync();
    }

    public virtual async Task<TEntity> GetById(string id)
    {
        return await _collection.Find(Builders<TEntity>.Filter.Eq("id", id)).FirstOrDefaultAsync();
    }

    public virtual async Task Create(TEntity entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public virtual async Task Update(string id, TEntity entity)
    {
        await _collection.UpdateOneAsync(Builders<TEntity>.Filter.Eq("id", id), entity.ToBsonDocument<TEntity>());
    }

    public virtual async Task Delete(string id)
    {
        await _collection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("id", id));
    }

    public void Dispose()
    {
        this.Dispose();
    }
}