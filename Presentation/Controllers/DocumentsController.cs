﻿using Application.Services.Interfaces;
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
    [Route("users/drafting")]
    public async Task<IActionResult> GetUserDraftDocuments()
    {
        var user = this.GetAuthenticatedUser();
        return await _documentService.GetUserDraftDocuments(user.Id);
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
    [Authorize]
    [Route("users/receive")]
    public async Task<IActionResult> GetUserReceiveDocuments()
    {
        var user = this.GetAuthenticatedUser();
        return await _documentService.GetUserReceiveDocuments(user.Id);
    }
    
    // GET
    [HttpGet]
    [Authorize]
    [Route("users/unclassified")]
    public async Task<IActionResult> GetUserUnclassifiedDocuments()
    {
        var user = this.GetAuthenticatedUser();
        return await _documentService.GetUserUnClassifiedDocuments(user.Id);
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
    
    // POST
    [HttpPost]
    [Authorize]
    [Route("outgoing")]
    public async Task<IActionResult> CreateOutgoingDocument([FromForm] DocumentCreateModel model)
    {
        var user = this.GetAuthenticatedUser();
        return await _documentService.CreateOutgoingDocument(user.Id, model);
    }
    
    // POST
    [HttpPost]
    [Authorize]
    [Route("incoming")]
    public async Task<IActionResult> CreateIncomingDocument([FromForm] DocumentCreateModel model)
    {
        var user = this.GetAuthenticatedUser();
        return await _documentService.CreateIncomingDocument(user.Id, model);
    }
    
    // POST
    [HttpPost]
    [Authorize]
    [Route("drafting")]
    public async Task<IActionResult> CreateDraftDocument([FromForm] DocumentCreateModel model)
    {
        var user = this.GetAuthenticatedUser();
        return await _documentService.CreateDraftDocument(user.Id, model);
    }
        
    // PUT
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateDocument([FromRoute] Guid id, [FromBody] DocumentUpdateModel model)
    {
        return await _documentService.UpdateDocument(id, model);
    }
    
    // PUT
    [HttpPut]
    [Authorize]
    [Route("{id}/receive")]
    public async Task<IActionResult> ReceiveDocument([FromRoute] Guid id)
    {
        return await _documentService.ReceiveDocument(id);
    }
    
    // PUT
    [HttpPut]
    [Authorize]
    [Route("{id}/return")]
    public async Task<IActionResult> ReturnDocument([FromRoute] Guid id, ReturnDocumentUpdateModel model)
    {
        return await _documentService.ReturnDocument(id, model);
    }
    
    // PUT
    [HttpPut]
    [Authorize]
    [Route("{id}/classify/{documentTypeId}")]
    public async Task<IActionResult> ClassifyDocument([FromRoute] Guid id, [FromRoute] Guid documentTypeId)
    {
        return await _documentService.ClassifyDocument(id, documentTypeId);
    }
    
    // DELETE
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteDocument([FromRoute] Guid id)
    {
        return await _documentService.DeleteDocument(id);
    }
}