using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Entity.DTOs.MailRelation
{
    public class MailRelationDto
    {

        public int ParentMailLogID { get; set; }
        public int? ChildMailLogID { get; set; }

        public bool Deleted { get; set; } = false;
    }
}
