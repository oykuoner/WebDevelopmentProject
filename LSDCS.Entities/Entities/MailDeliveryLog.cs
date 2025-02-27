using LSDCS.Core.Entities;

namespace LSDCS.Entity.Entities
{
    public class MailDeliveryLog : EntityBase
    {


        public int? MailLogID { get; set; }
        public string? DeliveryStatus { get; set; }
        public DateTime? DeliveryTimeStamp { get; set; }

        // Navigation property
        public virtual MailLog? MailLog { get; set; }
    }
}
