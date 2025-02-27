using AutoMapper;
using LSDCS.DataAccess.UnitOfWorks;
using LSDCS.Entity.DTOs.MailLog;
using LSDCS.Entity.Entities;
using LSDCS.Service.Extensions;
using LSDCS.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LSDCS.Entity.DTOs;
using LSDCS.DataAccess.Repositories.Concretes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using LSDCS.Entity.DTOs.Document;

namespace LSDCS.Service.Services.Concrete
{
    public class MailLogService : IMailLogService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ClaimsPrincipal _user;
        private readonly IWebHostEnvironment _environment;
        private string _documentsPath;

        public MailLogService(IUnitOfWork uow, IMapper mapper, IHttpContextAccessor contextAccessor, IWebHostEnvironment environment)
        {
            _uow = uow;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _user = contextAccessor.HttpContext.User;
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _documentsPath = Path.Combine(_environment.WebRootPath, "BTSDocuments");

        }





        public async Task<List<MailLogListDto>> GetAllMailLogWithClientMatterRecipientNoneDeletedAsync()
        {



            var mailLogs = await _uow.GetRepository<MailLog>().GetAllAsync(x => !x.Deleted, x => x.Clients, x => x.Matter, x => x.MailRecipients);

            var mailLogListDtos = new List<MailLogListDto>();

            foreach (var mailLog in mailLogs)
            {
                var dto = _mapper.Map<MailLogListDto>(mailLog); // AutoMapper kullanıyorsanız

                foreach (var mailRecipient in mailLog.MailRecipients)
                {
                    var recipient = await _uow.GetRepository<Recipients>().GetByIdAsync(mailRecipient.MAIL_ALICI_ID);

                    if (mailRecipient.ALICI_TIPI == "TO")
                    {
                        dto.ALICI_TO.Add(recipient.ALICI_MAIL);
                    }
                    else if (mailRecipient.ALICI_TIPI == "CC")
                    {
                        dto.ALICI_CC.Add(recipient.ALICI_MAIL);
                    }
                }

                mailLogListDtos.Add(dto);
            }

            return mailLogListDtos;
        }
     

        // Client Matter Recipient ve Silinmemiş olan Mailleri getir.


        public async Task<List<MailLogListDto>> GetAllParentMailLogWithClientMatterRecipientNoneDeletedAsync()
        {

            var parentMailLogIds = (await _uow.GetRepository<MailRelation>()
                                .GetAllAsync())
                                .Select(x => x.ParentMailLogID)
                                .Distinct()
                                .ToList();
            // Silinmemiş mail log'larını ve ilişkili verileri getir
            var mailLogs = await _uow.GetRepository<MailLog>().GetAllAsync(
                            x => !x.Deleted && parentMailLogIds.Contains(x.Id),
                            x => x.Clients,
                            x => x.Matter,
                            x => x.MailRecipients);


            var mailLogListDtos = new List<MailLogListDto>();
         
            foreach (var mailLog in mailLogs)
            {
                var dto = _mapper.Map<MailLogListDto>(mailLog);
           
                foreach (var mailRecipient in mailLog.MailRecipients)
                {
                    var recipient = await _uow.GetRepository<Recipients>().GetByIdAsync(mailRecipient.MAIL_ALICI_ID);
                    if (recipient != null)
                    {
                        if (mailRecipient.ALICI_TIPI == "TO")
                        {
                            dto.ALICI_TO.Add(recipient.ALICI_MAIL);
                        }
                        else if (mailRecipient.ALICI_TIPI == "CC")
                        {
                            dto.ALICI_CC.Add(recipient.ALICI_MAIL);
                        }
                    }
                }

                var mailLogDocuments = await _uow.GetRepository<MailLogDocuments>().GetAllAsync(x => x.MailLogId == mailLog.Id);


                foreach (var mailDocument in mailLogDocuments)
                {
                    var document = await _uow.GetRepository<Documents>().GetByIdAsync(mailDocument.DocumentId);
                    if (document != null)
                    {
                        // Doküman bilgilerini DTO'ya ekleme
                        dto.DocumentInfos.Add(new DocumentInfo
                        {
                            Id = document.Id,
                            DocumentName = document.DOKUMAN_ADI,
                            DocumentUrl = document.DOKUMAN_ADI_GUID
                        });
                    }
                }


                mailLogListDtos.Add(dto);
            }

            return mailLogListDtos;
        }

        public async Task<int> MailLogAdd(MailLogAddDto mailLogAddDto)
        {
            var userId = _user.GetLoggedInUserId();
            var userEmail = _user.GetLoggedInEmail();

            //var mailLog = _mapper.Map<MailLog>(mailLogAddDto);

            var mailLog = new MailLog(
                mailLogAddDto.ClientId
                , mailLogAddDto.MAIL_YONU
                , mailLogAddDto.MAIL_GONDERIM_TARIHI
                , mailLogAddDto.GONDERICI_MAIL
                , mailLogAddDto.MatterId
                , mailLogAddDto.MAIL_KONUSU
                , mailLogAddDto.HUKUKI_DEGERLENDIRME
                , mailLogAddDto.OLAY_OZETI
                , mailLogAddDto.DOKUMAN_OZETI
                , userEmail
                , userId
                , mailLogAddDto.HUKUKI_TALEP_KONUSU
                , mailLogAddDto.GONDERICI_ISIM
                , mailLogAddDto.TALEP_SURESI
                , mailLogAddDto.MUVEKKIL_SORULARI

                );
            await _uow.GetRepository<MailLog>().AddAsync(mailLog);
            await _uow.SaveAsync();

            return mailLog.Id; // Eklenen mailLog'un Id değerini geri döndür
        }



        public async Task<MailLogListDto> GetMailLogWithClientMatterRecipientNoneDeletedAsync(int mailLogId)
        {

     

            // Belirtilen mailLogId'ye ve silinmemiş olanlara sahip tek bir MailLog nesnesini al
            var mailLog = await _uow.GetRepository<MailLog>().GetAsync(
                x => x.Id == mailLogId,
                x => x.Clients,
                x => x.Matter,
                x => x.MailRecipients);
            if (mailLog == null)
            {
                return null; // veya uygun bir hata mesajı döndür
            }

            // AutoMapper kullanarak MailLog nesnesini MailLogListDto'ya dönüştür
            var mailLogListDto = _mapper.Map<MailLogListDto>(mailLog);

            var mailRecipients = await _uow.GetRepository<MailRecipients>().GetAllAsync(x => x.MailLogID == mailLogId);

            var maiLogDocuments = await _uow.GetRepository<MailLogDocuments>().GetAllAsync(x => x.MailLogId == mailLogId);

            foreach (var mailRecipient in mailRecipients)
            {
                var recipient = await _uow.GetRepository<Recipients>().GetByIdAsync(mailRecipient.MAIL_ALICI_ID);
                if (recipient != null)
                {
                    if (mailRecipient.ALICI_TIPI == "TO")
                    {

                        mailLogListDto.ALICI_TO.Add(recipient.ALICI_MAIL);
                    }
                    else if (mailRecipient.ALICI_TIPI == "CC")
                    {
                        mailLogListDto.ALICI_CC.Add(recipient.ALICI_MAIL);
                    }
                }
            }
            
            foreach (var mailDocument in maiLogDocuments)
            {

                var document = await _uow.GetRepository<Documents>().GetByIdAsync(mailDocument.DocumentId);
                if (document != null)
                {

                        // Dosyayı IFormFile nesnesine dönüştür
                       
                            mailLogListDto.DocumentInfos.Add(new DocumentInfo
                            {
                                 Id = document.Id,
                                DocumentName = document.DOKUMAN_ADI,
                                DocumentUrl = document.DOKUMAN_ADI_GUID
                            });

                }


            }

            return mailLogListDto;

        }






        public async Task<string> UpdateMailLogAsync(MailLogUpdateDto mailLogUpdateDto)
        {
            var userEmail = _user.GetLoggedInEmail();
            // MailLog'u Id'ye göre çek
            var mailLog = await _uow.GetRepository<MailLog>().GetAsync(
                x => x.Id == mailLogUpdateDto.Id && !x.Deleted,
                x => x.MailRecipients);

            mailLog.ClientID = mailLogUpdateDto.ClientId;
            mailLog.MAIL_YONU = mailLogUpdateDto.MAIL_YONU;
            mailLog.MAIL_GONDERIM_TARIHI = mailLogUpdateDto.MAIL_GONDERIM_TARIHI;
            mailLog.GONDERICI_MAIL = mailLogUpdateDto.GONDERICI_MAIL;
            mailLog.MAIL_KONUSU = mailLogUpdateDto.MAIL_KONUSU;
            mailLog.HUKUKI_TALEP_KONUSU = mailLogUpdateDto.HUKUKI_TALEP_KONUSU;
            mailLog.HUKUKI_DEGERLENDIRME = mailLogUpdateDto.HUKUKI_DEGERLENDIRME;
            mailLog.OLAY_OZETI = mailLogUpdateDto.OLAY_OZETI;
            mailLog.DOKUMAN_OZETI = mailLogUpdateDto.DOKUMAN_OZETI;
            mailLog.ModifiedDate = mailLogUpdateDto.ModifiedDate;
            mailLog.GONDERICI_ISIM = mailLogUpdateDto.GONDERICI_ISIM;


            //_mapper.Map<MailLogUpdateDto>(mailLog);

            mailLog.ModifiedBy = userEmail;
            if (mailLog == null)
            {
                // MailLog bulunamadı, hata fırlat veya uygun bir şekilde işlem yap
                throw new Exception("MailLog not found.");
            }

            //// AutoMapper veya manuel atama ile MailLog bilgilerini güncelle
            //_mapper.Map(mailLogUpdateDto, mailLog);
            await _uow.GetRepository<MailLog>().UpdateAsync(mailLog);

            await _uow.SaveAsync();

            return mailLog.MAIL_KONUSU;
        }



        public async Task<string> SafeDeleteMailLogAsync(int mailLogId)
        {
            var userEmail = _user.GetLoggedInEmail();
            var mailLog = await _uow.GetRepository<MailLog>().GetByIdAsync(mailLogId);
            if (mailLog == null)
            {
                throw new Exception("MailLog not found.");
            }

            mailLog.Deleted = true;
            mailLog.DeletedDate = DateTime.Now;
            mailLog.DeletedBy = userEmail;

            var childMailRelations = await _uow.GetRepository<MailRelation>().GetAllAsync(x => x.ParentMailLogID == mailLogId);
            foreach (var childMailRelation in childMailRelations)
            {
                var childMailLog = await _uow.GetRepository<MailLog>().GetByIdAsync(childMailRelation.ChildMailLogID);
                if (childMailLog != null)
                {
                    childMailLog.Deleted = true;
                    childMailLog.DeletedDate = DateTime.Now;
                    await _uow.GetRepository<MailLog>().UpdateAsync(childMailLog);

                    // İlgili ChildMail ile ilişkili tüm MailRecipients kayıtlarını silinmiş olarak işaretle
                    var mailRecipients = await _uow.GetRepository<MailRecipients>().GetAllAsync(mr => mr.MailLogID == childMailLog.Id);
                    foreach (var mailRecipient in mailRecipients)
                    {
                        mailRecipient.Deleted = true;

                        await _uow.GetRepository<MailRecipients>().UpdateAsync(mailRecipient);
                    }
                }

                childMailRelation.Deleted = true;

                await _uow.GetRepository<MailRelation>().UpdateAsync(childMailRelation);
            }

            // ParentMail olarak kullanılan MailRelation kayıtlarını da güncelle
            var parentMailRelations = await _uow.GetRepository<MailRelation>().GetAllAsync(x => x.ChildMailLogID == mailLogId);
            foreach (var parentMailRelation in parentMailRelations)
            {
                parentMailRelation.Deleted = true;

                await _uow.GetRepository<MailRelation>().UpdateAsync(parentMailRelation);
            }

            await _uow.GetRepository<MailLog>().UpdateAsync(mailLog);
            await _uow.SaveAsync();
            return mailLog.MAIL_KONUSU;
        }


        public async Task<List<MailLogListDto>> GetAllDeletedParentMailLogAsync()
        {

            var parentMailLogIds = (await _uow.GetRepository<MailRelation>()
                                .GetAllAsync())
                                .Select(x => x.ParentMailLogID)
                                .Distinct()
                                .ToList();
            // Silinmemiş mail log'larını ve ilişkili verileri getir
            var mailLogs = await _uow.GetRepository<MailLog>().GetAllAsync(
                            x => x.Deleted && parentMailLogIds.Contains(x.Id),
                            x => x.Clients,
                            x => x.Matter,
                            x => x.MailRecipients);


            var mailLogListDtos = new List<MailLogListDto>();

            foreach (var mailLog in mailLogs)
            {
                var dto = _mapper.Map<MailLogListDto>(mailLog);

                foreach (var mailRecipient in mailLog.MailRecipients)
                {
                    var recipient = await _uow.GetRepository<Recipients>().GetByIdAsync(mailRecipient.MAIL_ALICI_ID);
                    if (recipient != null)
                    {
                        if (mailRecipient.ALICI_TIPI == "TO")
                        {
                            dto.ALICI_TO.Add(recipient.ALICI_MAIL);
                        }
                        else if (mailRecipient.ALICI_TIPI == "CC")
                        {
                            dto.ALICI_CC.Add(recipient.ALICI_MAIL);
                        }
                    }
                }

                mailLogListDtos.Add(dto);
            }

            return mailLogListDtos;

        }   // Silinmiş Mailler

        public async Task<List<MailLogListDto>> GetAllDeletedChildMailLogAsync(int parentMailLogId)
        {
            var childMailIds = (await _uow.GetRepository<MailRelation>().GetAllAsync(
         mr => mr.ParentMailLogID == parentMailLogId))
         .Select(mr => mr.ChildMailLogID)
         .ToList();

            var mailLogs = await _uow.GetRepository<MailLog>().GetAllAsync(
                        x => x.Deleted && childMailIds.Contains(x.Id),
                        x => x.Clients, x => x.Matter, x => x.MailRecipients);


            var mailLogListDtos = new List<MailLogListDto>();

            foreach (var mailLog in mailLogs)
            {
                var dto = _mapper.Map<MailLogListDto>(mailLog); // AutoMapper kullanıyorsanız

                foreach (var mailRecipient in mailLog.MailRecipients)
                {
                    var recipient = await _uow.GetRepository<Recipients>().GetByIdAsync(mailRecipient.MAIL_ALICI_ID);

                    if (mailRecipient.ALICI_TIPI == "TO")
                    {
                        dto.ALICI_TO.Add(recipient.ALICI_MAIL);
                    }
                    else if (mailRecipient.ALICI_TIPI == "CC")
                    {
                        dto.ALICI_CC.Add(recipient.ALICI_MAIL);
                    }
                }

                mailLogListDtos.Add(dto);
            }

            return mailLogListDtos;

        }
        public async Task<string> UndoDeleteMailLogAsync(int mailLogId)
        {


            var userEmail = _user.GetLoggedInEmail();
            var mailLog = await _uow.GetRepository<MailLog>().GetByIdAsync(mailLogId);
            if (mailLog == null)
            {
                throw new Exception("MailLog not found.");
            }

            mailLog.Deleted = false;
            mailLog.DeletedDate = null;
            mailLog.DeletedBy = null;

            var childMailRelations = await _uow.GetRepository<MailRelation>().GetAllAsync(x => x.ParentMailLogID == mailLogId);
            foreach (var childMailRelation in childMailRelations)
            {
                var childMailLog = await _uow.GetRepository<MailLog>().GetByIdAsync(childMailRelation.ChildMailLogID);
                if (childMailLog != null)
                {
                    childMailLog.Deleted = false;
                    childMailLog.DeletedDate = null;
                    await _uow.GetRepository<MailLog>().UpdateAsync(childMailLog);

                    // İlgili ChildMail ile ilişkili tüm MailRecipients kayıtlarını silinmiş olarak işaretle
                    var mailRecipients = await _uow.GetRepository<MailRecipients>().GetAllAsync(mr => mr.MailLogID == childMailLog.Id);
                    foreach (var mailRecipient in mailRecipients)
                    {
                        mailRecipient.Deleted = false;

                        await _uow.GetRepository<MailRecipients>().UpdateAsync(mailRecipient);
                    }
                }

                childMailRelation.Deleted = false;

                await _uow.GetRepository<MailRelation>().UpdateAsync(childMailRelation);
            }

            // ParentMail olarak kullanılan MailRelation kayıtlarını da güncelle
            var parentMailRelations = await _uow.GetRepository<MailRelation>().GetAllAsync(x => x.ChildMailLogID == mailLogId);
            foreach (var parentMailRelation in parentMailRelations)
            {
                parentMailRelation.Deleted = false;

                await _uow.GetRepository<MailRelation>().UpdateAsync(parentMailRelation);
            }

            await _uow.GetRepository<MailLog>().UpdateAsync(mailLog);
            await _uow.SaveAsync();
            return mailLog.MAIL_KONUSU;
        }



        public async Task<List<MailLogListDto>> GetAllChildParentMailList(int childMailId)
        {
            var parentMailLogId = await _uow.GetRepository<MailRelation>().GetAsync(
                   x => x.ChildMailLogID == childMailId  // ChildMailId'ye göre filtreleme
            );
            if (parentMailLogId == null)
            {
                return new List<MailLogListDto>(); // ParentMailLogID null ise boş liste döndür
            }

            var childMailIds = (await _uow.GetRepository<MailRelation>().GetAllAsync(
                           mr => mr.ParentMailLogID == parentMailLogId.ParentMailLogID))
                           .Select(mr => mr.ChildMailLogID)
                           .ToList();

            var mailLogs = await _uow.GetRepository<MailLog>().GetAllAsync(
                        x => !x.Deleted && childMailIds.Contains(x.Id),
                        x => x.Clients, x => x.Matter, x => x.MailRecipients);

            var mailLogListDtos = new List<MailLogListDto>();

            foreach (var mailLog in mailLogs)
            {
                var dto = _mapper.Map<MailLogListDto>(mailLog); // AutoMapper kullanıyorsanız

                dto.ALICI_TO = new List<string>();
                dto.ALICI_CC = new List<string>();

                foreach (var mailRecipient in mailLog.MailRecipients)
                {
                    var recipient = await _uow.GetRepository<Recipients>().GetByIdAsync(mailRecipient.MAIL_ALICI_ID);

                    if (mailRecipient.ALICI_TIPI == "TO")
                    {
                        dto.ALICI_TO.Add(recipient.ALICI_MAIL);
                    }
                    else if (mailRecipient.ALICI_TIPI == "CC")
                    {
                        dto.ALICI_CC.Add(recipient.ALICI_MAIL);
                    }
                }

                mailLogListDtos.Add(dto);
            }

            return mailLogListDtos;
        }



        public async Task<List<MailLogListDto>> GetAllDeletedAllMailLogAsync()
        {


            var mailLogs = await _uow.GetRepository<MailLog>().GetAllAsync(x => x.Deleted, x => x.Clients, x => x.Matter, x => x.MailRecipients);

            var mailLogListDtos = new List<MailLogListDto>();

            foreach (var mailLog in mailLogs)
            {
                var dto = _mapper.Map<MailLogListDto>(mailLog); // AutoMapper kullanıyorsanız

                foreach (var mailRecipient in mailLog.MailRecipients)
                {
                    var recipient = await _uow.GetRepository<Recipients>().GetByIdAsync(mailRecipient.MAIL_ALICI_ID);

                    if (mailRecipient.ALICI_TIPI == "TO")
                    {
                        dto.ALICI_TO.Add(recipient.ALICI_MAIL);
                    }
                    else if (mailRecipient.ALICI_TIPI == "CC")
                    {
                        dto.ALICI_CC.Add(recipient.ALICI_MAIL);
                    }
                }

                mailLogListDtos.Add(dto);
            }

            return mailLogListDtos;
        }



    }
}
