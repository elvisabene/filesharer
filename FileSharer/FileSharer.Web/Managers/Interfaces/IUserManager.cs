using FileSharer.Common.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FileSharer.Web.Managers.Interfaces
{
    public interface IUserManager
    {
        Task Authenticate(User user, HttpContext httpContext);

        bool IsUserExists(string email);

        bool IsUserExists(string email, string password, out User user);

        User CreateUser(string name, string email, string password);
    }
}
