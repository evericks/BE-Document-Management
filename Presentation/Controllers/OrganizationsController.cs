using Application.Services.Interfaces;
using Domain.Models.Creates;
using Domain.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/organizations")]
[ApiController]
public class OrganizationController : Controller
{
    private readonly IOrganizationService _organizationService;

    public OrganizationController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }
    
    // GET
    [HttpGet]
    public async Task<IActionResult> GetOrganizations()
    {
        return await _organizationService.GetOrganizations();
    }
    
    // GET
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetOrganization([FromRoute] Guid id)
    {
        return await _organizationService.GetOrganization(id);
    }
    
    // POST
    [HttpPost]
    public async Task<IActionResult> CreateOrganization([FromBody] OrganizationCreateModel model)
    {
        return await _organizationService.CreateOrganization(model);
    }
        
    // PUT
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateOrganization([FromRoute] Guid id, [FromBody] OrganizationUpdateModel model)
    {
        return await _organizationService.UpdateOrganization(id, model);
    }
    
    // DELETE
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteOrganization([FromRoute] Guid id)
    {
        return await _organizationService.DeleteOrganization(id);
    }
}