using Domain.Models.Creates;
using Domain.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces;

public interface IDocumentTypeService
{
    Task<IActionResult> GetDocumentTypes();
    Task<IActionResult> GetDocumentType(Guid id);
    Task<IActionResult> GetAdditionalInformationByDocumentTypeId(Guid id);
    Task<IActionResult> CreateDocumentType(DocumentTypeCreateModel model);
    Task<IActionResult> UpdateDocumentType(Guid id, DocumentTypeUpdateModel model);
    Task<IActionResult> DeleteDocumentType(Guid id);
    
}