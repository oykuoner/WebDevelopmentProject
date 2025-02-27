using AutoMapper;
using LSDCS.DataAccess.UnitOfWorks;
using LSDCS.Entity.DTOs.MailLog;
using LSDCS.Entity.DTOs.MailRecipient;
using LSDCS.Entity.Entities;
using LSDCS.Service.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Service.Services.Concrete
{
    public class MailRecipintService : IMailRecipintService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MailRecipintService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task ProcessAndAddRecipientsAsync(List<int> recipientListTO, List<int> recipientListCC, int mailLogId)
        {
            // TO alıcılarını işle ve ekle
            foreach (var recipientEmail in recipientListTO)
            {
                var mailRecipientTO = new MailRecipients
                {
                    MailLogID = mailLogId,
                    ALICI_TIPI = "TO",
                    MAIL_ALICI_ID = recipientEmail,
                    // Eğer MailRecipients sınıfınızda RecipientEmail gibi bir alan varsa:
                    // RecipientEmail = recipientEmail,
                    Deleted = false
                };
                var mailRecipientTOMap = _mapper.Map<MailRecipients>(mailRecipientTO);
                await _unitOfWork.GetRepository<MailRecipients>().AddAsync(mailRecipientTOMap);
            }

            // CC alıcılarını işle ve ekle
            foreach (var recipientEmail in recipientListCC)
            {
                var mailRecipientCC = new MailRecipients
                {
                    MailLogID = mailLogId,
                    ALICI_TIPI = "CC",
                    MAIL_ALICI_ID = recipientEmail,
                    // Eğer MailRecipients sınıfınızda RecipientEmail gibi bir alan varsa:
                    // RecipientEmail = recipientEmail,
                    Deleted = false
                };
                var mailRecipientCCMap = _mapper.Map<MailRecipients>(mailRecipientCC);
                await _unitOfWork.GetRepository<MailRecipients>().AddAsync(mailRecipientCCMap);
            }
            // Tüm değişiklikleri veritabanına kaydet
            await _unitOfWork.SaveAsync();
        }

        public async Task ProcessAndUpdateRecipientsAsync(string ALICI_TO, string ALICI_CC, int mailLogId)
        {




            var recipientListTO = ALICI_TO.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                           .Select(email => email.Trim().ToLowerInvariant()).ToList();

            var recipientListCC = string.IsNullOrEmpty(ALICI_CC) ? new List<string>()
                         : ALICI_CC.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(email => email.Trim().ToLowerInvariant()).ToList();




            // Mevcut MailRecipients kayıtlarını getir
            var existingMailRecipients = await _unitOfWork.GetRepository<MailRecipients>().GetAllAsync(mr => mr.MailLogID == mailLogId);
            var existingRecipients = await _unitOfWork.GetRepository<Recipients>().GetAllAsync();

            // TO ve CC listesindeki e-posta adreslerine karşılık gelen RecipientsID'leri bul
            var recipientIdsTO = existingRecipients.Where(r => recipientListTO.Contains(r.ALICI_MAIL)).Select(r => r.Id).ToList();
            var recipientIdsCC = existingRecipients.Where(r => recipientListCC.Contains(r.ALICI_MAIL)).Select(r => r.Id).ToList();

            // Yeni eklenenleri tespit et ve MailRecipients olarak ekle
            foreach (var recipientId in recipientIdsTO.Union(recipientIdsCC))
            {
                if (!existingMailRecipients.Any(mr => mr.MAIL_ALICI_ID == recipientId))
                {
                    await _unitOfWork.GetRepository<MailRecipients>().AddAsync(new MailRecipients
                    {
                        MailLogID = mailLogId,
                        MAIL_ALICI_ID = recipientId,
                        ALICI_TIPI = recipientIdsTO.Contains(recipientId) ? "TO" : "CC"
                    });
                }
            }

            // Artık listede olmayan MailRecipients kayıtlarını sil
            foreach (var existingMailRecipient in existingMailRecipients)
            {
                if (!recipientIdsTO.Contains(existingMailRecipient.MAIL_ALICI_ID) && existingMailRecipient.ALICI_TIPI == "TO"
                    || !recipientIdsCC.Contains(existingMailRecipient.MAIL_ALICI_ID) && existingMailRecipient.ALICI_TIPI == "CC")
                {
                    await _unitOfWork.GetRepository<MailRecipients>().DeleteAsync(existingMailRecipient);
                }
            }

            await _unitOfWork.SaveAsync(); // Tüm değişiklikleri kaydet

        }

    }
}
