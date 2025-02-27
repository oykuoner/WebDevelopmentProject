using LSDCS.Core.Entities;

namespace LSDCS.Entity.Entities
{
    public class MailRecipients : EntityBase
    {

        public int MAIL_ALICI_ID { get; set; }
        public int MailLogID { get; set; }
        public string ALICI_TIPI { get; set; }
        public bool Deleted { get; set; } = false;
        // Navigation properties
        public MailLog MailLog { get; set; }
        public Recipients Recipients { get; set; }
    }
}
