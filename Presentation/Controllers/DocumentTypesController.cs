using Application.Services.Interfaces;
using Domain.Models.Creates;
using Domain.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/document-types")]
[ApiController]
public class DocumentTypeController : Controller
{
    private readonly IDocumentTypeService _documentTypeService;

    public DocumentTypeController(IDocumentTypeService documentTypeService)
    {
        _documentTypeService = documentTypeService;
    }
    
    // GET
    [HttpGet]
    public async Task<IActionResult> GetDocumentTypes()
    {
        return await _documentTypeService.GetDocumentTypes();
    }
    
    // GET
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetDocumentType([FromRoute] Guid id)
    {
        return await _documentTypeService.GetDocumentType(id);
    }
    
    [HttpGet]
    [Route("additional-informations/{id}")]
    public async Task<IActionResult> GetAdditionalInformationByDocumentTypeId([FromRoute] Guid id)
    {
        return await _documentTypeService.GetAdditionalInformationByDocumentTypeId(id);
    }
    
    // POST
    [HttpPost]
    public async Task<IActionResult> CreateDocumentType([FromBody] DocumentTypeCreateModel model)
    {
        return await _documentTypeService.CreateDocumentType(model);
    }
        
    // PUT
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateDocumentType([FromRoute] Guid id, [FromBody] DocumentTypeUpdateModel model)
    {
        return await _documentTypeService.UpdateDocumentType(id, model);
    }
    
    // DELETE
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteDocumentType([FromRoute] Guid id)
    {
        return await _documentTypeService.DeleteDocumentType(id);
    }
}