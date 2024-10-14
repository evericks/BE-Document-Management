using Data.EntityRepositories.Interfaces;

namespace Data.UnitOfWorks.Interfaces;

public interface IUnitOfWork
{
    public IRoleRepository Role { get; }
    public IDepartmentRepository Department { get; }
    public IUserRepository User { get; }
    
    void BeginTransaction();
    
    void Commit();
    
    void Rollback();
    
    void Dispose();
    
    Task<int> SaveChangesAsync();
}