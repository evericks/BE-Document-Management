using Domain.Models.Creates;
using Domain.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces;

public interface IDocumentService
{
    Task<IActionResult> GetDocuments();
    Task<IActionResult> GetUserDocuments(Guid id);
    Task<IActionResult> GetDocument(Guid id);
    Task<IActionResult> CreateDocument(Guid senderId, DocumentCreateModel model);
    Task<IActionResult> UpdateDocument(Guid id, DocumentUpdateModel model);
    Task<IActionResult> DeleteDocument(Guid id);
    
}