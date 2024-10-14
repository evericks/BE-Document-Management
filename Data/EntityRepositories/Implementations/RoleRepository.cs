using Data.EntityRepositories.Interfaces;
using Data.Repositories.Implementations;
using Domain.Context;
using Domain.Entities;

namespace Data.EntityRepositories.Implementations;

public class RoleRepository: Repository<Role>, IRoleRepository
{
    public RoleRepository(DocumentManagementContext context) : base(context)
    {
    }
}