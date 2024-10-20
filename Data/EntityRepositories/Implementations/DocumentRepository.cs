using Data.EntityRepositories.Interfaces;
using Data.Repositories.Implementations;
using Domain.Context;
using Domain.Entities;

namespace Data.EntityRepositories.Implementations;

public class DocumentRepository: Repository<Document>, IDocumentRepository
{
    public DocumentRepository(DocumentManagementContext context) : base(context)
    {
    }
}