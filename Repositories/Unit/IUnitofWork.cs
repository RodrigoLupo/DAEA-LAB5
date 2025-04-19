namespace LAB05_Lupo.Repositories.Unit;

public interface IUnitofWork
{
    IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    Task<int> Complete();
}