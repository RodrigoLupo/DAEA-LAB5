using System.Linq.Expressions;
using LAB05_Lupo.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB05_Lupo.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly EscuelaDbContext _context;

    public GenericRepository(EscuelaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAll() => await _context.Set<T>().ToListAsync();
    
    public async Task<T?> GetById(int id) => await _context.Set<T>().FindAsync(id);

    public async Task<T?> GetByIdString(string id) => await _context.Set<T>().FindAsync(id);
    
    public async Task<List<T>> GetByIds(IEnumerable<int> ids)
    {
        var idProperty = typeof(T)
            .GetProperties()
            .FirstOrDefault(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase)
                                 || p.Name.StartsWith("Id", StringComparison.OrdinalIgnoreCase));

        if (idProperty == null || idProperty.PropertyType != typeof(int))
            throw new InvalidOperationException($"No se encontró una propiedad 'Id' válida en el tipo {typeof(T).Name}");

        var parameter = Expression.Parameter(typeof(T), "e");
        var propertyAccess = Expression.Property(parameter, idProperty.Name);
        var containsMethod = typeof(Enumerable).GetMethods()
            .First(m => m.Name == "Contains" &&
                        m.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(int));
        var idsConstant = Expression.Constant(ids);
        var containsCall = Expression.Call(containsMethod, idsConstant, propertyAccess);
        var lambda = Expression.Lambda<Func<T, bool>>(containsCall, parameter);

        return await _context.Set<T>().Where(lambda).ToListAsync();
       
    }
    
    public async Task Add(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var entityEntry = await _context.Set<T>().FindAsync(id);
        if (entityEntry != null)
        {
            _context.Set<T>().Remove(entityEntry);
            await _context.SaveChangesAsync();
        }
    }
}