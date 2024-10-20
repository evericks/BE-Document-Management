using Application.Services.Interfaces;
using Domain.Models.Creates;
using Domain.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/document-statuses")]
[ApiController]
public class DocumentStatusController : Controller
{
    private readonly IDocumentStatusService _documentStatusService;

    public DocumentStatusController(IDocumentStatusService documentStatusService)
    {
        _documentStatusService = documentStatusService;
    }
    
    // GET
    [HttpGet]
    public async Task<IActionResult> GetDocumentStatuses()
    {
        return await _documentStatusService.GetDocumentStatuses();
    }
    
    // GET
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetDocumentStatus([FromRoute] Guid id)
    {
        return await _documentStatusService.GetDocumentStatus(id);
    }
    
    // POST
    [HttpPost]
    public async Task<IActionResult> CreateDocumentStatus([FromBody] DocumentStatusCreateModel model)
    {
        return await _documentStatusService.CreateDocumentStatus(model);
    }
        
    // PUT
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateDocumentStatus([FromRoute] Guid id, [FromBody] DocumentStatusUpdateModel model)
    {
        return await _documentStatusService.UpdateDocumentStatus(id, model);
    }
    
    // DELETE
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteDocumentStatus([FromRoute] Guid id)
    {
        return await _documentStatusService.DeleteDocumentStatus(id);
    }
}