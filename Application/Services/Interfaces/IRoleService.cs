using Domain.Models.Creates;
using Domain.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces;

public interface IRoleService
{
    Task<IActionResult> GetRoles();
    Task<IActionResult> GetRole(Guid id);
    Task<IActionResult> CreateRole(RoleCreateModel model);
    Task<IActionResult> UpdateRole(Guid id, RoleUpdateModel model);
    Task<IActionResult> DeleteRole(Guid id);
    
}