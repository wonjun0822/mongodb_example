using mongodb_example.Model;

namespace mongodb_example.Interface;

public interface IArticleService
{
    Task<List<Article>> GetAll();
    Task<Article> GetById(string id);
    Task Create(Article request);
    Task<Article> Update(string id, Article request);
    Task Delete(string id);
}