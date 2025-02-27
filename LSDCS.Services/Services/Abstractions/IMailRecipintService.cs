using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Service.Services.Abstractions
{
    public interface IMailRecipintService
    {
        Task ProcessAndAddRecipientsAsync(List<int> recipientListTO, List<int> recipientListCC, int mailLogId);



        Task ProcessAndUpdateRecipientsAsync(string recipientListTO, string recipientListCC, int mailLogId);
    }
}
