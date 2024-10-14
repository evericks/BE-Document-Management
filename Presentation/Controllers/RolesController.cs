using Application.Services.Interfaces;
using Domain.Models.Creates;
using Domain.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/roles")]
[ApiController]
public class RoleController : Controller
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }
    
    // GET
    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        return await _roleService.GetRoles();
    }
    
    // GET
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetRole([FromRoute] Guid id)
    {
        return await _roleService.GetRole(id);
    }
    
    // POST
    [HttpPost]
    // [Authorize(UserRoles.Admin)]
    public async Task<IActionResult> CreateRole([FromBody] RoleCreateModel model)
    {
        return await _roleService.CreateRole(model);
    }
        
    // PUT
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateRole([FromRoute] Guid id, [FromBody] RoleUpdateModel model)
    {
        return await _roleService.UpdateRole(id, model);
    }
    
    // DELETE
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteRole([FromRoute] Guid id)
    {
        return await _roleService.DeleteRole(id);
    }
}