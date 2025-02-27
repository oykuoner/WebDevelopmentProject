using LSDCS.Core.Entities;

namespace LSDCS.Entity.Entities
{
    public class Recipients : EntityBase 
    {


        public string ALICI_MAIL { get; set; }

        public string? ALICI_ISIM { get; set; }        // Yeni alan
        // Assuming a Recipient can have multiple MailRecipients
        public ICollection<MailRecipients> MailRecipients { get; set; } = new HashSet<MailRecipients>();
    }
}
