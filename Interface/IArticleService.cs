using mongodb_example.Model;
using mongodb_example.DTO;

namespace mongodb_example.Interface;

public interface IArticleService
{
    Task<List<ArticleListDTO>> GetAll();
    Task<ArticleDetailDTO> GetById(string id);
    Task<ArticleDetailDTO> Create(ArticleWriteDTO request);
    Task<ArticleDetailDTO> Update(string id, ArticleWriteDTO request);
    Task Delete(string id);
}