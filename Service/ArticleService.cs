using MongoDB.Driver;
using MongoDB.Driver.Linq;

using mongodb_example.Context;
using mongodb_example.DTO;
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

    public async Task<List<ArticleListDTO>> GetAll()
    {
        return await _collection.Find(Builders<Article>.Filter.Empty).Project(
            x => new ArticleListDTO { id = x.id!, title = x.title}
        ).ToListAsync();
    }

    public async Task<ArticleDetailDTO> GetById(string id)
    {
        return await _collection.Find(Builders<Article>.Filter.Eq(x => x.id, id)).Project(
            x => new ArticleDetailDTO { 
                id = x.id!, 
                title = x.title,
                content = x.content,
                comments = x.comments.Select(s => 
                    new CommentListDTO { id = s.id!, comment = s.comment, replys = s.replys.Select(o => 
                        new ReplyListDTO { id = o.id!, comment = o.comment }
                    ).ToList() }
                ).ToList()
            }
        ).FirstOrDefaultAsync();
    }

    public async Task<ArticleDetailDTO> Create(ArticleWriteDTO request)
    {
        var article = new Article(request);

        await _collection.InsertOneAsync(article);

        return new ArticleDetailDTO { id = article.id!, title = article.title, content = article.content, comments = null };
    }

    public async Task<ArticleDetailDTO> Update(string id, ArticleWriteDTO request)
    {
        return await _collection.FindOneAndUpdateAsync(
            Builders<Article>.Filter.Eq(x => x.id, id),
            Builders<Article>.Update.Set(x => x.title, request.title).Set(x => x.content, request.content),
            options: new FindOneAndUpdateOptions<Article, ArticleDetailDTO>
            {
                ReturnDocument = ReturnDocument.After,
                Projection = Builders<Article>.Projection.Expression(x => new ArticleDetailDTO
                {
                    id = x.id!,
                    title = x.title,
                    content = x.content
                })
            }
        );
    }

    public async Task Delete(string id)
    {
        await _collection.FindOneAndDeleteAsync(Builders<Article>.Filter.Eq(x => x.id, id));
    }
}