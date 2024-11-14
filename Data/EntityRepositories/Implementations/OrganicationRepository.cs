using Data.EntityRepositories.Interfaces;
using Data.Repositories.Implementations;
using Domain.Context;
using Domain.Entities;

namespace Data.EntityRepositories.Implementations;

public class OrganizationRepository: Repository<Organization>, IOrganizationRepository
{
    public OrganizationRepository(DocumentManagementContext context) : base(context)
    {
    }
}