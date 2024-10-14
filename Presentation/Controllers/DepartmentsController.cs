using Application.Services.Interfaces;
using Domain.Models.Creates;
using Domain.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/departments")]
[ApiController]
public class DepartmentController : Controller
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }
    
    // GET
    [HttpGet]
    public async Task<IActionResult> GetDepartments()
    {
        return await _departmentService.GetDepartments();
    }
    
    // GET
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetDepartment([FromRoute] Guid id)
    {
        return await _departmentService.GetDepartment(id);
    }
    
    // POST
    [HttpPost]
    // [Authorize(UserDepartments.Admin)]
    public async Task<IActionResult> CreateDepartment([FromBody] DepartmentCreateModel model)
    {
        return await _departmentService.CreateDepartment(model);
    }
        
    // PUT
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateDepartment([FromRoute] Guid id, [FromBody] DepartmentUpdateModel model)
    {
        return await _departmentService.UpdateDepartment(id, model);
    }
    
    // DELETE
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteDepartment([FromRoute] Guid id)
    {
        return await _departmentService.DeleteDepartment(id);
    }
}