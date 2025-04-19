namespace LAB05_Lupo.Repositories.Unit;

public interface IUnitOfWork
{
    IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    Task<int> Complete();
}