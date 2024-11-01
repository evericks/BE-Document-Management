using Data.EntityRepositories.Interfaces;
using Data.Repositories.Implementations;
using Domain.Context;
using Domain.Entities;

namespace Data.EntityRepositories.Implementations;

public class ProcessStepRepository: Repository<ProcessStep>, IProcessStepRepository
{
    public ProcessStepRepository(DocumentManagementContext context) : base(context)
    {
    }
}