using LSDCS.Entity.Entities;
using LSDCS.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace LSDCS.Webuilder.Areas.User.Controllers
{

    [Area("User")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMailLogService _mailLogService;
        private readonly IDashBoardService _boardService;

        public HomeController(IMailLogService mailLogService, IDashBoardService boardService)
        {
            _mailLogService = mailLogService;
            _boardService = boardService;
        }

        public async Task<IActionResult> Index()
        {

            var mailLogs = await _mailLogService.GetAllMailLogWithClientMatterRecipientNoneDeletedAsync();

            return View(mailLogs);
        }

        [HttpGet]
        public async Task<IActionResult> TotalMailLogCount()
        {
            var count = await _boardService.GetTotalMailLogCount();
            //return Json(JsonConvert.SerializeObject(count));    // Liste olarak gelen integer değerlerini gösterir.

            return Json(count);
        }


    }
}
