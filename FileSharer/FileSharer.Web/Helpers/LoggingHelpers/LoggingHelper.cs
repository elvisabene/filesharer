using FileSharer.Common.Constants;
using System.Linq;
using System.Security.Claims;

namespace FileSharer.Web.Helpers.LoggingHelpers
{
    public class LoggingHelper : ILogHelper
    {
        public string GetUserActionString(
            ClaimsPrincipal user, string controller, string action, string method = "GET")
        {
            var userName = Defaults.UserName;
            var userRole = Roles.Guest;

            if (user.Identity.IsAuthenticated)
            {
                userName = user.Identity.Name;
                userRole = user.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;
            }

            var actionString = $"{userName}[{userRole}] has performed action {controller}/{action} [{method}].";

            return actionString;
        }
    }
}
