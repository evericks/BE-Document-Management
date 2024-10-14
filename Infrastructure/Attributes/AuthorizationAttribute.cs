using Common.Constants;
using Domain.Models.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using HttpResponse = Common.HttpContexts.HttpResponse;

namespace Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private ICollection<string> Roles { get; }

        public AuthorizeAttribute(params string[] roles)
        {
            Roles = roles.Select(x => x.ToLower()).ToList();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var auth = (UserInformationModel?)context.HttpContext.Items["USER"];
            if (auth == null)
            {
                context.Result = HttpResponse.Unauthorized(AppMessages.Unauthorized);
            }
            else
            {
                var role = auth.Role;
                var isValid = (Roles.Count == 0 || Roles.Contains(role.ToLower()));

                if (!isValid)
                {
                    context.Result = HttpResponse.Forbidden(AppMessages.Forbidden);
                }
            }
        }
    }
}