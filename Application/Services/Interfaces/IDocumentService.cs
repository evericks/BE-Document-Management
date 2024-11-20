using Domain.Models.Creates;
using Domain.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces;

public interface IDocumentService
{
    Task<IActionResult> GetDocuments();
    Task<IActionResult> GetUserReferenceDocuments(Guid userId);
    Task<IActionResult> GetUserDocuments(Guid id);
    Task<IActionResult> GetUserReturnDocuments(Guid id);
    Task<IActionResult> GetDocument(Guid id);
    Task<IActionResult> CreateDocument(Guid senderId, DocumentCreateModel model);
    Task<IActionResult> CreateOutgoingDocument(Guid senderId, DocumentCreateModel model);
    Task<IActionResult> CreateIncomingDocument(Guid senderId, DocumentCreateModel model);
    Task<IActionResult> SendDocument(Guid id, Guid senderId, SendDocumentUpdateModel model);
    Task<IActionResult> CreateDraftDocument(Guid senderId, DocumentCreateModel model);
    Task<IActionResult> GetUserUnClassifiedDocuments(Guid id);
    Task<IActionResult> GetUserDraftDocuments(Guid id);
    Task<IActionResult> GetUserPendingProcessingDocuments(Guid id);
    Task<IActionResult> GetUserReceiveDocuments(Guid id);
    Task<IActionResult> UpdateDocument(Guid id, DocumentUpdateModel model);
    Task<IActionResult> ReceiveDocument(Guid id);
    Task<IActionResult> ReturnDocument(Guid id, ReturnDocumentUpdateModel model);
    Task<IActionResult> ClassifyDocument(Guid id, Guid documentTypeId);
    Task<IActionResult> DeleteDocument(Guid id);
}