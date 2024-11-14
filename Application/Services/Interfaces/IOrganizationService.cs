using Domain.Models.Creates;
using Domain.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces;

public interface IOrganizationService
{
    Task<IActionResult> GetOrganizations();
    Task<IActionResult> GetOrganization(Guid id);
    Task<IActionResult> CreateOrganization(OrganizationCreateModel model);
    Task<IActionResult> UpdateOrganization(Guid id, OrganizationUpdateModel model);
    Task<IActionResult> DeleteOrganization(Guid id);
    
}