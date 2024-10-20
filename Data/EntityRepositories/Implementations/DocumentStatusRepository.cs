using Data.EntityRepositories.Interfaces;
using Data.Repositories.Implementations;
using Domain.Context;
using Domain.Entities;

namespace Data.EntityRepositories.Implementations;

public class DocumentStatusRepository: Repository<DocumentStatus>, IDocumentStatusRepository
{
    public DocumentStatusRepository(DocumentManagementContext context) : base(context)
    {
    }
}