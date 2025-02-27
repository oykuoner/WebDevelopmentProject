using LSDCS.Entity.DTOs.MailLog;
using LSDCS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Service.Services.Abstractions

{
    public interface IMailLogService
    {

        Task<List<MailLogListDto>> GetAllMailLogWithClientMatterRecipientNoneDeletedAsync();
        //Task<List<MailLogListDto>> GetAllChildMailLogWithClientMatterRecipientNoneDeletedAsync(int parentMailLogId);

        Task<List<MailLogListDto>> GetAllParentMailLogWithClientMatterRecipientNoneDeletedAsync();
        Task<List<MailLogListDto>> GetAllDeletedParentMailLogAsync();   // Silinmiş Mailler
        Task<List<MailLogListDto>> GetAllDeletedChildMailLogAsync(int parentMailLogId);   // Silinmiş Mailler
        Task<string> UndoDeleteMailLogAsync(int mailLogId);
        Task<int> MailLogAdd(MailLogAddDto mailLogAddDto);

        Task<MailLogListDto> GetMailLogWithClientMatterRecipientNoneDeletedAsync(int mailLogId);

        Task<string> UpdateMailLogAsync(MailLogUpdateDto mailLogUpdateDto);

        Task<string> SafeDeleteMailLogAsync(int mailLogId);


        Task<List<MailLogListDto>> GetAllChildParentMailList(int childMailId);

        Task<List<MailLogListDto>> GetAllDeletedAllMailLogAsync();


    }
}
