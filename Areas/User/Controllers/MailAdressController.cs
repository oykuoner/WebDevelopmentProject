using AutoMapper;
using LSDCS.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace LSDCS.Web.Areas.User.Controllers
{
    public class MailAdressController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;
        private readonly IRecipientService _recipientService;

        public MailAdressController(IRecipientService recipientService, IToastNotification toastNotification, IMapper mapper)
        {
            _recipientService = recipientService;
            _toastNotification = toastNotification;
            _mapper = mapper;
        }

        public IActionResult Index( )
        {
            return View();
        }
    }
}
