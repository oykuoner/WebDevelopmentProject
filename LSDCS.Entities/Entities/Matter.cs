using LSDCS.Core.Entities;

namespace LSDCS.Entity.Entities
{
    public class Matter : EntityBase
    {


        public string MatterName { get; set; }

        // Assuming a 1-to-Many relationship from Matter to MailLog
        public ICollection<MailLog> MailLogs { get; set; } = new HashSet<MailLog>();
    }
}
