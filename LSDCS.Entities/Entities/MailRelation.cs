using LSDCS.Core.Entities;

namespace LSDCS.Entity.Entities
{
    public class MailRelation : EntityBase
    {



        public int ParentMailLogID { get; set; }
        public int ChildMailLogID { get; set; }
        public bool Deleted { get; set; } = false;

        // Navigation properties
        public MailLog ParentMailLog { get; set; }
        public MailLog ChildMailLog { get; set; }

    }
}
