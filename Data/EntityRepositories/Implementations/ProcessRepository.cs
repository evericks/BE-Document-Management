using Data.EntityRepositories.Interfaces;
using Data.Repositories.Implementations;
using Domain.Context;
using Domain.Entities;

namespace Data.EntityRepositories.Implementations;

public class ProcessRepository: Repository<Process>, IProcessRepository
{
    public ProcessRepository(DocumentManagementContext context) : base(context)
    {
    }
}