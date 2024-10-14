using Domain.Models.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Authentication.Interfaces
{
    public interface IAuthenticationService
    {
        Task<IActionResult> GetUserInformation(Guid id);
        Task<IActionResult> Authenticate(CertificateModel certificate);
        Task<UserInformationModel?> GetUser(Guid id);
    }
}