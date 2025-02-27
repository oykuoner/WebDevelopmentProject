using LSDCS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Entity.Entities
{
    public class MailLogDocuments   : EntityBase
    {

        public int MailLogId { get; set; }
        public int DocumentId { get; set; }


        public MailLog MailLog { get; set; }
        public Documents Document { get; set; }

    }
}
