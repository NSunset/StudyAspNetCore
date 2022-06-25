using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nw.LiveBackgroundManagement.Business.Interface;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;
using Nw.LiveBackgroundManagement.WebSite.Models;
using System.Diagnostics;

namespace Nw.LiveBackgroundManagement.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ICSRoomService _cSRoomService;

        public HomeController(ILogger<HomeController> logger,ICSRoomService cSRoomService)
        {
            _logger = logger;
            _cSRoomService = cSRoomService;
        }

        public IActionResult Index()
        {
            CSRoom cSRoom = _cSRoomService.Find<CSRoom>(1);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
