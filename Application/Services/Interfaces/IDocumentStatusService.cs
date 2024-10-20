using Domain.Models.Creates;
using Domain.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces;

public interface IDocumentStatusService
{
    Task<IActionResult> GetDocumentStatuses();
    Task<IActionResult> GetDocumentStatus(Guid id);
    Task<IActionResult> CreateDocumentStatus(DocumentStatusCreateModel model);
    Task<IActionResult> UpdateDocumentStatus(Guid id, DocumentStatusUpdateModel model);
    Task<IActionResult> DeleteDocumentStatus(Guid id);
    
}