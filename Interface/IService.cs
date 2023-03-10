namespace mongodb_example.Interface;

public interface IService<TEntity> : IDisposable where TEntity : class 
{
    Task<List<TEntity>> GetAll();
    Task<TEntity> GetById(string id);
    Task Create(TEntity entity);
    Task Update(string id, TEntity entity);
    Task Delete(string id);
}