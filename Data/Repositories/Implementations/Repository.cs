using System.Linq.Expressions;
using Data.Repositories.Interfaces;
using Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data.Repositories.Implementations;

public class Repository<TEntity>: IRepository<TEntity> where TEntity : class
{
    private readonly DocumentManagementContext _context;
    private readonly DbSet<TEntity> _dbSet;

    protected Repository(DocumentManagementContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbSet;
    }
    
    public int Count()
    {
        return _dbSet.Count();
    }
    
    public IQueryable<TEntity> SkipAndTake(int skip, int take)
    {
        return _dbSet.Skip(skip).Take(take);
    }
    
    public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.FirstOrDefault(predicate) ?? null!;
    }

    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate) ?? null!;
    }
    
    public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.SingleOrDefaultAsync(predicate) ?? null!;
    }
    
    public bool Contains(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Where(predicate).ToList().Count > 0;
    }
    
    public bool Any(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Any(predicate);
    }
    
    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }
    
    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }
    
    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
    
    public void AddRange(IEnumerable<TEntity> entities)
    {
        _dbSet.AddRange(entities);
    }
    
    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
    }
    
    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }
    
    public IQueryable<TEntity> AsNoTracking()
    {
        return _dbSet.AsNoTracking();
    }

    public IQueryable<TEntity> AsQueryable()
    {
        return _dbSet.AsQueryable();
    }
    
    public async Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken = default)
    {
        return await _context.Database.BeginTransactionAsync(cancellationToken);
    }
}