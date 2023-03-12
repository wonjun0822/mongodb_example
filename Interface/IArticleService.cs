using mongodb_example.Model;
using mongodb_example.DTO;

namespace mongodb_example.Interface;

public interface IArticleService
{
    Task<List<ArticleDTO>> GetAll();
    Task<Article> GetById(string id);
    Task Create(Article request);
    Task<Article> Update(string id, Article request);
    Task Delete(string id);
}