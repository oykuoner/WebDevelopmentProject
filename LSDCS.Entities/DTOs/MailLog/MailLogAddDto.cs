using LSDCS.Entity.DTOs.Clients;
using LSDCS.Entity.DTOs.MailRecipient;
using LSDCS.Entity.DTOs.Matter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace LSDCS.Entity.DTOs.MailLog
{
    public class MailLogAddDto
    {
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        public int ParentMailId { get; set; }
        public List<string>? ALICI_TO { get; set; }
        // public List<string>? ALICI_TO_ISIM { get; set; } = new List<string>();
        public List<string>? ALICI_CC { get; set; }
        //  public List<string>? ALICI_CC_ISIM { get; set; } = new List<string>();
        public string GONDERICI_MAIL { get; set; }
        public string MAIL_KONUSU { get; set; }
        public string HUKUKI_TALEP_KONUSU { get; set; }
        public string HUKUKI_DEGERLENDIRME { get; set; }
        public string OLAY_OZETI { get; set; }
        public string DOKUMAN_OZETI { get; set; }
        public DateTime MAIL_GONDERIM_TARIHI { get; set; } = DateTime.Now;
        public string MAIL_YONU { get; set; }
        public int MatterId { get; set; }
        public int ClientId { get; set; }
        public bool Deleted { get; set; } = false;
        public string? CreatedBy { get; set; } = string.Empty;

        public List<ClientDto> Clients { get; set; }
        public List<MatterDto> Matters { get; set; }


        public List<IFormFile> Documents { get; set; } = new List<IFormFile>();

        public string? GONDERICI_ISIM { get; set; }
        public double TALEP_SURESI { get; set; } // Yeni alan.
        public string? MUVEKKIL_SORULARI { get; set; }
        public string? MUVEKKILE_VERILEN_CEVAP { get; set; }    // Yeni alan

    }
}
