using Application.Services.Interfaces;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    // GET
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] UserFilterModel filter)
    {
        return await _userService.GetUsers(filter);
    }
    
    // GET
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetUser([FromRoute] Guid id)
    {
        return await _userService.GetUser(id);
    }
    
    // POST
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserCreateModel model)
    {
        return await _userService.CreateUser(model);
    }
    
    // PUT
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UserUpdateModel model)
    {
        return await _userService.UpdateUser(id, model);
    }
    
    // DELETE
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        return await _userService.DeleteUser(id);
    }
}