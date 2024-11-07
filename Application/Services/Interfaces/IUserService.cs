using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces;

public interface IUserService
{
    Task<IActionResult> GetUsers(UserFilterModel filter);
    Task<IActionResult> GetUser(Guid id);
    Task<IActionResult> CreateUser(UserCreateModel model);
    Task<IActionResult> UpdateUser(Guid id, UserUpdateModel model);
    Task<IActionResult> DeleteUser(Guid id);
}