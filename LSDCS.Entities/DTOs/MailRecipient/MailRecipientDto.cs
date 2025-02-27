using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Entity.DTOs.MailRecipient
{
    public class MailRecipientDto
    {
        public List<int> RecipientTO { get; set; }
        public List<int> RecipientCC { get; set; }
        public string RecipientsType { get; set; }
        public int RecipientsID { get; set; }
   
        public string RecipientEmail { get; set; }

      
    }
}
