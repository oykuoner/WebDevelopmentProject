using LSDCS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Entity.Entities
{
    public class Documents    : EntityBase
    {


        public string? DOKUMAN_ADI_GUID { get; set; }
        public string? DOKUMAN_ADI { get; set; }
        public List<MailLogDocuments> MailLogDocuments { get; set; }

    }
}
