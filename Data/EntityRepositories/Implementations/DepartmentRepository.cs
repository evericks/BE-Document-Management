using Data.EntityRepositories.Interfaces;
using Data.Repositories.Implementations;
using Domain.Context;
using Domain.Entities;

namespace Data.EntityRepositories.Implementations;

public class DepartmentRepository: Repository<Department>, IDepartmentRepository
{
    public DepartmentRepository(DocumentManagementContext context) : base(context)
    {
    }
}