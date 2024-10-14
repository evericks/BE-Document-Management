using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Common.HttpContexts;

public static class HttpResponse
{
    // 200
    public static IActionResult Ok()
    {
        return new OkResult();
    }

    public static IActionResult Ok(object value)
    {
        return new ObjectResult(value)
        {
            StatusCode = StatusCodes.Status200OK
        };
    }

    // 201
    public static IActionResult Created()
    {
        return new CreatedResult();
    }

    public static IActionResult Created(object value)
    {
        return new ObjectResult(value)
        {
            StatusCode = StatusCodes.Status201Created
        };
    }

    // 400
    public static IActionResult BadRequest()
    {
        return new BadRequestResult();
    }

    public static IActionResult BadRequest(object value)
    {
        return new ObjectResult(value)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
    
    // 401
    public static IActionResult Unauthorized()
    {
        return new UnauthorizedResult();
    }

    public static IActionResult Unauthorized(object value)
    {
        return new ObjectResult(value)
        {
            StatusCode = StatusCodes.Status401Unauthorized
        };
    }
    
    // 403
    public static IActionResult Forbidden()
    {
        return new StatusCodeResult(403);
    }

    public static IActionResult Forbidden(object value)
    {
        return new ObjectResult(value)
        {
            StatusCode = StatusCodes.Status403Forbidden
        };
    }

    // 404
    public static IActionResult NotFound()
    {
        return new NotFoundResult();
    }

    public static IActionResult NotFound(object value)
    {
        return new ObjectResult(value)
        {
            StatusCode = StatusCodes.Status404NotFound
        };
    }

    // 409
    public static IActionResult Conflict()
    {
        return new ConflictResult();
    }

    public static IActionResult Conflict(object value)
    {
        return new ObjectResult(value)
        {
            StatusCode = StatusCodes.Status409Conflict
        };
    }

    // 500
    public static IActionResult InternalServerError(object value)
    {
        return new ObjectResult(value)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
}