using Application.Services.Authentication.Interfaces;
using Common.Extensions;
using Domain.Models.Authorization;
using Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }


        [HttpPost]
        [Route("sign-in")]
        public async Task<IActionResult> Authenticate([FromBody] CertificateModel certificate)
        {
            try
            {
                return await _authService.Authenticate(certificate);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("sign-in-with-token")]
        [Authorize]
        public async Task<IActionResult> GetUserInformation()
        {
            try
            {
                var auth = this.GetAuthenticatedUser();
                return await _authService.GetUserInformation(auth.Id);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}