using Application.Services.Interfaces;
using Domain.Models.Creates;
using Domain.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/processes")]
[ApiController]
public class ProcessController : Controller
{
    private readonly IProcessService _processService;

    public ProcessController(IProcessService processService)
    {
        _processService = processService;
    }
    
    // GET
    [HttpGet]
    public async Task<IActionResult> GetProcesss()
    {
        return await _processService.GetProcesses();
    }
    
    // GET
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetProcess([FromRoute] Guid id)
    {
        return await _processService.GetProcess(id);
    }
    
    // GET
    [HttpGet]
    [Route("document-types/{id}")]
    public async Task<IActionResult>? GetDocumentTypeProcess([FromRoute] Guid id)
    {
        return await _processService.GetDocumentTypeProcess(id);
    }
    
    // POST
    [HttpPost]
    public async Task<IActionResult> CreateProcess([FromBody] ProcessCreateModel model)
    {
        return await _processService.CreateProcess(model);
    }
        
    // PUT
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateProcess([FromRoute] Guid id, [FromBody] ProcessUpdateModel model)
    {
        return await _processService.UpdateProcess(id, model);
    }
    
    // DELETE
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteProcess([FromRoute] Guid id)
    {
        return await _processService.DeleteProcess(id);
    }
}