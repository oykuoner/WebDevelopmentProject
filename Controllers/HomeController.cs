using LSDCS.Service.Services.Abstractions;
using LSDCS.Webuilder.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LSDCS.Webuilder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMailLogService _mailLogService;

        public HomeController(ILogger<HomeController> logger, IMailLogService mailLogService)
        {
            _logger = logger;
            _mailLogService = mailLogService;
        }

        public async Task<IActionResult> Index()
        {

            var mailLogs = await _mailLogService.GetAllMailLogWithClientMatterRecipientNoneDeletedAsync();
            return View(mailLogs);
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
