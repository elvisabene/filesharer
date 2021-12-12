using System.Security.Claims;

namespace FileSharer.Web.Helpers.LoggingHelpers
{
    public interface ILogHelper
    {
        public string GetUserActionString(
            ClaimsPrincipal user, string controller, string actionName, string method = "GET");
    }
}
