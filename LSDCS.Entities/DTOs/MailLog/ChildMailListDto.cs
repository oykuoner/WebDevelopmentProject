using LSDCS.Entity.DTOs.Clients;
using LSDCS.Entity.DTOs.MailRecipient;
using LSDCS.Entity.DTOs.Matter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Entity.DTOs.MailLog
{
    public class ChildMailListDto
    {
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string HUKUKI_TALEP_KONUSU { get; set; }
        public int Id { get; set; }
        public List<string> ALICI_TO { get; set; } = new List<string>();
        public List<string> ALICI_CC { get; set; } = new List<string>();
        public string GONDERICI_MAIL { get; set; }
        public string MAIL_KONUSU { get; set; }
        public List<MailRecipientDto> MailRecipients { get; set; }
        public string HUKUKI_DEGERLENDIRME { get; set; }
        public string OLAY_OZETI { get; set; }
        public string DOKUMAN_OZETI { get; set; }
        public DateTime MAIL_GONDERIM_TARIHI { get; set; }
        public string MAIL_YONU { get; set; }
        public MatterDto Matter { get; set; }
        public ClientDto Client { get; set; }
        public bool Deleted { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public string? DeletedBy { get; set; }


        public DateTime? MadifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
