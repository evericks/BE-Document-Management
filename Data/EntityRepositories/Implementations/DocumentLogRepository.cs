using Data.EntityRepositories.Interfaces;
using Data.Repositories.Implementations;
using Domain.Context;
using Domain.Entities;

namespace Data.EntityRepositories.Implementations;

public class DocumentLogRepository: Repository<DocumentLog>, IDocumentLogRepository
{
    public DocumentLogRepository(DocumentManagementContext context) : base(context)
    {
    }
}