using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Models.Creates;
using Domain.Models.Update;
using Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/documents")]
[ApiController]
public class DocumentController : Controller
{
    private readonly IDocumentService _documentService;

    public DocumentController(IDocumentService documentService)
    {
        _documentService = documentService;
    }
    
    // GET
    [HttpGet]
    public async Task<IActionResult> GetDocuments()
    {
        return await _documentService.GetDocuments();
    }
    
    // GET
    [HttpGet]
    [Authorize]
    [Route("users")]
    public async Task<IActionResult> GetUserDocuments()
    {
        var user = this.GetAuthenticatedUser();
        return await _documentService.GetUserDocuments(user.Id);
    }
    
    // GET
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetDocument([FromRoute] Guid id)
    {
        return await _documentService.GetDocument(id);
    }
    
    // POST
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateDocument([FromForm] DocumentCreateModel model)
    {
        var user = this.GetAuthenticatedUser();
        return await _documentService.CreateDocument(user.Id, model);
    }
        
    // PUT
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateDocument([FromRoute] Guid id, [FromBody] DocumentUpdateModel model)
    {
        return await _documentService.UpdateDocument(id, model);
    }
    
    // DELETE
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteDocument([FromRoute] Guid id)
    {
        return await _documentService.DeleteDocument(id);
    }
}