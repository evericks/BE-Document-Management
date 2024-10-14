using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();
    
    int Count();
    
    IQueryable<TEntity> SkipAndTake(int skip, int take);
    
    TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
    
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
    
    bool Contains(Expression<Func<TEntity, bool>> predicate);

    bool Any(Expression<Func<TEntity, bool>> predicate);

    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
    
    void Add(TEntity entity);
    
    void Update(TEntity entity);
    
    void Delete(TEntity entity);
    
    void AddRange(IEnumerable<TEntity> entities);
    
    void UpdateRange(IEnumerable<TEntity> entities);
    
    void DeleteRange(IEnumerable<TEntity> entities);

    IQueryable<TEntity> AsNoTracking();
    
    IQueryable<TEntity> AsQueryable();
    
    public Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken = default);
}