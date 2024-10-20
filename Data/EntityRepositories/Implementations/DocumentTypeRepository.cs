using Data.EntityRepositories.Interfaces;
using Data.Repositories.Implementations;
using Domain.Context;
using Domain.Entities;

namespace Data.EntityRepositories.Implementations;

public class DocumentTypeRepository: Repository<DocumentType>, IDocumentTypeRepository
{
    public DocumentTypeRepository(DocumentManagementContext context) : base(context)
    {
    }
}