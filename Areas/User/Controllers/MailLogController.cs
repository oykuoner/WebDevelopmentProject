using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using LSDCS.Entity.DTOs.MailLog;
using LSDCS.Entity.Entities;
using LSDCS.Service.FluenValidations;
using LSDCS.Service.Services.Abstractions;
using LSDCS.Service.Services.Concrete;
using LSDCS.Web.Consts;
using LSDCS.Web.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Runtime.InteropServices;

namespace LSDCS.Web.Areas.User.Controllers
{

    [Area("User")]

    public class MailLogController : Controller
    {
        private readonly IValidator _validator;
        private readonly IMapper _mapper;
        private readonly IMailLogService _mailLogService;
        private readonly IClientService _clientService;
        private readonly IMatterService _matterService;
        private readonly IRecipientService _recipientService;
        private readonly IMailRelationsService _mailRelationsService;
        private readonly IMailRecipintService _mailRecipintService;
        private readonly IToastNotification _toastNotification;
        private readonly IDocumentService _documentService;


        public MailLogController(IMailLogService mailLogService, IClientService clientService, IMatterService matterService, IRecipientService recipientService, IMailRelationsService mailRelationsService, IMailRecipintService mailRecipintService, IMapper mapper, IValidator<MailLog> validator, IToastNotification toastNotification, IDocumentService documentService)
        {
            _mailLogService = mailLogService;
            _clientService = clientService;
            _matterService = matterService;
            _recipientService = recipientService;
            _mailRelationsService = mailRelationsService;
            _mailRecipintService = mailRecipintService;
            _mapper = mapper;
            _validator = validator;
            _toastNotification = toastNotification;
            _documentService = documentService;
        }


        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Admin},{RoleConsts.User}")]

        //public async Task<IActionResult> Index(int clientId, int currentPage = 1, int pageSize = 3, bool isAscending = false)
        //{

        //    var mailLogs = await _mailLogService.GetAllByPagingAsync(clientId,currentPage,pageSize,isAscending);

        //    return View(mailLogs);
        //}
        public async Task<IActionResult> Index()
        {

            var milLog = await _mailLogService.GetAllMailLogWithClientMatterRecipientNoneDeletedAsync();
            return View(milLog);
        }


        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Admin}")]
        public async Task<IActionResult> DeletedParentMailLogs()
        {

            var milLog = await _mailLogService.GetAllDeletedParentMailLogAsync();
            return View(milLog);
        }

  
        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Admin},{RoleConsts.User}")]
        public async Task<IActionResult> Add()
        {

            var clients = await _clientService.GetAllClientsNoneDelete();
            var matters = await _matterService.GetAllMattersNoneDelete();

            return View(new MailLogAddDto { Clients = clients, Matters = matters });
        }


   
        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.Admin},{RoleConsts.User}")]
        public async Task<IActionResult> Add(MailLogAddDto mailLogAddDto, string ALICI_TO, string ALICI_CC,List<IFormFile> formFileMultiple)
        {
            mailLogAddDto.MatterId = 3;



            var documentsId = await _documentService.DocumentAdd(mailLogAddDto.Documents);
            // DTO'dan Entity'e dönüştür
            var mailLog = _mapper.Map<MailLog>(mailLogAddDto);

            // FluentValidation için uygun ValidationContext oluştur
            var context = new ValidationContext<MailLog>(mailLog);

            // Validator ile doğrulama yap
            var result = await _validator.ValidateAsync(context);



            if (result.IsValid)
            {


                var recipeints = await _recipientService.ProcessEmailListsAsync(ALICI_TO, ALICI_CC);
                var mailLogId = await _mailLogService.MailLogAdd(mailLogAddDto);
                await _mailRelationsService.AddMailRelationAsync(mailLogId, mailLogId);


                await _mailRecipintService.ProcessAndAddRecipientsAsync(recipeints.recipientsListIdTO, recipeints.recipientsListIdCC, mailLogId);
                await _documentService.AddMailLogDocument(mailLogId, documentsId);
                //var clients = await _clientService.GetAllClientsNoneDelete();
                //var matters = await _matterService.GetAllMattersNoneDelete();
                _toastNotification.AddSuccessToastMessage(Message.MailLog.Add(mailLogAddDto.MAIL_KONUSU), new ToastrOptions { Title = "Başarılı!" });
                return RedirectToAction("Index", "MailLog", new { Area = "User" });

            }

            else
            {
                result.AddToModelState(this.ModelState);

                var clients = await _clientService.GetAllClientsNoneDelete();
                var matters = await _matterService.GetAllMattersNoneDelete();
                return View(new MailLogAddDto { Clients = clients, Matters = matters });
            }




        }


       
        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Admin},{RoleConsts.User}")]
        public async Task<IActionResult> ChildMailList(int id)
        {

            var milLog = await _mailLogService.GetAllChildParentMailList(id);
            ViewBag.ParentMailId = id; // Parent mail'in Id'sini ViewBag'e ekleyin
            return View(milLog);
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Admin}")]
        public async Task<IActionResult> DeleteChildMailList(int id)
        {

            var milLog = await _mailLogService.GetAllDeletedChildMailLogAsync(id);
            ViewBag.ParentMailId = id; // Parent mail'in Id'sini ViewBag'e ekleyin
            return View(milLog);
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Admin},{RoleConsts.User}")]
        public async Task<IActionResult> ChildMailAdd(int? parentMailId)
        {
            var model = new MailLogAddDto
            {
                ParentMailId = (int)parentMailId
            };

            var clients = await _clientService.GetAllClientsNoneDelete();
            var matters = await _matterService.GetAllMattersNoneDelete();

            return View(new MailLogAddDto { Clients = clients, Matters = matters });
        }





        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.Admin},{RoleConsts.User}")]
        public async Task<IActionResult> ChildMailAdd(MailLogAddDto mailLogAddDto, string ALICI_TO, string ALICI_CC)
        {




            mailLogAddDto.MatterId = 3;
            // String olarak gelen e-posta adreslerini listeye dönüştür ve normalize et
            var documentsId = await _documentService.DocumentAdd(mailLogAddDto.Documents);
            var mailLog = _mapper.Map<MailLog>(mailLogAddDto);
            var context = new ValidationContext<MailLog>(mailLog);
            var result = await _validator.ValidateAsync(context);

            if (result.IsValid)
            {

                var recipeints = await _recipientService.ProcessEmailListsAsync(ALICI_TO, ALICI_CC);
                var mailLogId = await _mailLogService.MailLogAdd(mailLogAddDto);
                await _mailRelationsService.AddMailRelationAsync(mailLogAddDto.ParentMailId, mailLogId);

                await _mailRecipintService.ProcessAndAddRecipientsAsync(recipeints.recipientsListIdTO, recipeints.recipientsListIdCC, mailLogId);
                await _documentService.AddMailLogDocument(mailLogId, documentsId);
                _toastNotification.AddSuccessToastMessage(Message.MailLog.Add(mailLogAddDto.MAIL_KONUSU), new ToastrOptions { Title = "Başarılı!" });
                return RedirectToAction("Index", "MailLog", new { Area = "Admin" });

            }

            else
            {

                result.AddToModelState(this.ModelState);
                var clients = await _clientService.GetAllClientsNoneDelete();
                var matters = await _matterService.GetAllMattersNoneDelete();

                return View(new MailLogAddDto { Clients = clients, Matters = matters });
            }

        }
                                                                                     
        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Admin},{RoleConsts.User}")]
        public async Task<IActionResult> ParentMailList()
        {

            var milLog = await _mailLogService.GetAllParentMailLogWithClientMatterRecipientNoneDeletedAsync();
            return View(milLog);
        }



        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Admin},{RoleConsts.User}")]
        public async Task<IActionResult> MailLogUpdate(int mailLogId)
        {

            var mailLog = await _mailLogService.GetMailLogWithClientMatterRecipientNoneDeletedAsync(mailLogId);
            var clients = await _clientService.GetAllClientsNoneDelete();
            var matters = await _matterService.GetAllMattersNoneDelete();


            var mailLogUpdateDto = _mapper.Map<MailLogUpdateDto>(mailLog);
            mailLogUpdateDto.Clients = clients;
            mailLogUpdateDto.Matters = matters;


            return View(mailLogUpdateDto);
        }



        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.Admin},{RoleConsts.User}")]
        public async Task<IActionResult> MailLogUpdate(MailLogUpdateDto mailLogUpdateDto, string ALICI_TO, string ALICI_CC)
        {



            mailLogUpdateDto.MatterId = 3;
            var documentsId = await _documentService.DocumentAdd(mailLogUpdateDto.Documents);
            var mailLog = _mapper.Map<MailLog>(mailLogUpdateDto);
            var context = new ValidationContext<MailLog>(mailLog);
            var result = await _validator.ValidateAsync(context);

            if (result.IsValid)
            {
                var recipients = await _recipientService.ProcessEmailListsAsync(ALICI_TO, ALICI_CC);
                var subject = await _mailLogService.UpdateMailLogAsync(mailLogUpdateDto);
                await _mailRecipintService.ProcessAndUpdateRecipientsAsync(ALICI_TO, ALICI_CC, mailLogUpdateDto.Id);
                await _documentService.AddMailLogDocument(mailLogUpdateDto.Id, documentsId);
                _toastNotification.AddSuccessToastMessage(Message.MailLog.Update(subject), new ToastrOptions { Title = "Başarılı!" });
                return RedirectToAction("MailLogDetails", "MailLog", new { Area = "User", mailLogId = mailLogUpdateDto.Id }); ;
            }
            else
            {
                var clients = await _clientService.GetAllClientsNoneDelete();
                var matters = await _matterService.GetAllMattersNoneDelete();



                mailLogUpdateDto.Clients = clients;
                mailLogUpdateDto.Matters = matters;


                return View(mailLogUpdateDto);
            }

        }




        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Admin},{RoleConsts.User}")]
        public async Task<IActionResult> MailLogSafeDelete(int mailLogId)
        {
            var SUBJECT = await _mailLogService.SafeDeleteMailLogAsync(mailLogId);

            _toastNotification.AddSuccessToastMessage(Message.MailLog.Delete(SUBJECT), new ToastrOptions { Title = "Başarılı!" });

            return RedirectToAction("Index", "MailLog", new { Area = "User" });
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Admin}")]
        public async Task<IActionResult> MailLogUndoDelete(int mailLogId)
        {
            var SUBJECT = await _mailLogService.UndoDeleteMailLogAsync(mailLogId);

            _toastNotification.AddSuccessToastMessage(Message.MailLog.UndoDelete(SUBJECT), new ToastrOptions { Title = "Başarılı!" });

            return RedirectToAction("Index", "MailLog", new { Area = "Admin" });
        }





        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Admin},{RoleConsts.User}")]
        public async Task<IActionResult> MailLogDetails(int mailLogId)
        {

            var mailLog = await _mailLogService.GetMailLogWithClientMatterRecipientNoneDeletedAsync(mailLogId);
         
            var matter = await _matterService.GetAllMattersNoneDelete();

          

            var mailLogUpdateDto = _mapper.Map<MailDetailsDto>(mailLog);
            mailLogUpdateDto.Client = mailLog.Client.ClientName;
            mailLogUpdateDto.Matter = mailLog.Matter.MatterName;


            return View(mailLogUpdateDto);
        }



        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.Admin},{RoleConsts.User}")]

        public async Task<IActionResult> DocumentDelete(int documentId)
        {
            await _documentService.DocumentDelete(documentId);
            return Ok();
        }
    }
}
