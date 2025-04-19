namespace LAB05_Lupo.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(int id);
    Task<T?> GetByIdString(string id);
    Task<List<T>> GetByIds(IEnumerable<int> ids);
    Task Add(T entity);
    Task Update(T entity);
    Task Delete(int id);
}