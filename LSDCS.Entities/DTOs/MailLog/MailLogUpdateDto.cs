using LSDCS.Entity.DTOs.Clients;
using LSDCS.Entity.DTOs.Document;
using LSDCS.Entity.DTOs.Matter;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Entity.DTOs.MailLog
{
    public class MailLogUpdateDto
    {
        public List<IFormFile> Documents { get; set; } = new List<IFormFile>();
        public List<DocumentInfo> DocumentInfos { get; set; } = new List<DocumentInfo>();
        public List<string>? ALICI_TO { get; set; }
        public List<string>? ALICI_CC { get; set; }
        public int Id { get; set; }
        public string HUKUKI_TALEP_KONUSU { get; set; }
        public string GONDERICI_MAIL { get; set; }
        public string? GONDERICI_ISIM { get; set; }
        public string MAIL_KONUSU { get; set; }

        public string? MUVEKKIL_SORULARI { get; set; }

        public string? MUVEKKILE_VERILEN_CEVAP { get; set; }

        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string HUKUKI_DEGERLENDIRME { get; set; }
        public string OLAY_OZETI { get; set; }
        public string DOKUMAN_OZETI { get; set; }
        public DateTime MAIL_GONDERIM_TARIHI { get; set; } = DateTime.Now;
        public string MAIL_YONU { get; set; }
        public int MatterId { get; set; }
        public int ClientId { get; set; }
        public bool Deleted { get; set; } = false;
        public string? CreatedBy { get; set; } = string.Empty;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public List<ClientDto> Clients { get; set; }
        public List<MatterDto> Matters { get; set; }


    }
}
