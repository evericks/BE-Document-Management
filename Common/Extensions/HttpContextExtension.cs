using Domain.Models.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Common.Extensions
{
    public static class HttpContextExtension
    {

        public static UserInformationModel GetAuthenticatedUser(this ControllerBase controller)
        {
            if (controller.HttpContext.Items.TryGetValue("USER", out var userObject))
            {
                return (UserInformationModel)userObject!;
            }

            return null!;
        }
    }
}
