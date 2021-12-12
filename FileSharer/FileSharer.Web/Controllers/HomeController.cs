using FileSharer.Web.Helpers.LoggingHelpers;
using FileSharer.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace FileSharer.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ILogHelper _logHelper;

        public HomeController(ILogger<HomeController> logger, ILogHelper logHelper)
        {
            _logger = logger;
            _logHelper = logHelper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation(_logHelper.GetUserActionString(User, "Home", nameof(Index)));
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
