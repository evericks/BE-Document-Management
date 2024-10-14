using Domain.Models.Creates;
using Domain.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces;

public interface IDepartmentService
{
    Task<IActionResult> GetDepartments();
    Task<IActionResult> GetDepartment(Guid id);
    Task<IActionResult> CreateDepartment(DepartmentCreateModel model);
    Task<IActionResult> UpdateDepartment(Guid id, DepartmentUpdateModel model);
    Task<IActionResult> DeleteDepartment(Guid id);
    
}