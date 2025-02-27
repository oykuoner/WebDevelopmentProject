using LSDCS.Core.Entities;

namespace LSDCS.Entity.Entities
{
    public class Clients : EntityBase
    {

        public string ClientName { get; set; }
 
        public List<MailLog> MailLog { get; set; }
    }
}
