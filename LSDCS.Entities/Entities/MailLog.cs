using LSDCS.Core.Entities;
using LSDCS.Entity.Entities;

namespace LSDCS.Entity.Entities
{
    public class MailLog : EntityBase
    {
  

        public MailLog()
        {

        }
        public MailLog(int clientId, string mailYonu, DateTime mailGonderimTarihi, string gondericiMail, int matterId, string mailKonusu, string hukukiDegerlendirme
            , string olayOzeti, string dokumanOzeti,string createdBy,int userId, string hukukiTalepKonusu 
            , string gondericiIsim  , double talepSuresi , string muvekkilSoruları
            
            
            )
        {

            ClientID = clientId;
            MAIL_YONU = mailYonu;
            MAIL_GONDERIM_TARIHI = mailGonderimTarihi;
            GONDERICI_MAIL = gondericiMail;
            MatterID = matterId;
            MAIL_KONUSU = mailKonusu;
            HUKUKI_DEGERLENDIRME = hukukiDegerlendirme;
            OLAY_OZETI = olayOzeti;
            DOKUMAN_OZETI = dokumanOzeti;
            CreatedBy = createdBy;
            UserId = userId;
            HUKUKI_TALEP_KONUSU = hukukiTalepKonusu;
            GONDERICI_ISIM = gondericiIsim;
            TALEP_SURESI = talepSuresi;
            MUVEKKIL_SORULARI = muvekkilSoruları;
        }


        public DateTime? CreatedDate { get; set; } = DateTime.Now;


        public int ClientID { get; set; }
        public Clients Clients { get; set; }


        public string? MAIL_YONU { get; set; }


        public string? GONDERICI_MAIL { get; set; }
        public string? GONDERICI_ISIM { get; set; }    // Yeni eklenen alan.



        public string? MAIL_KONUSU { get; set; }
        public DateTime MAIL_GONDERIM_TARIHI { get; set; } = DateTime.Now;


        public double? TALEP_SURESI { get; set; } // Yeni alan.



        public string? OLAY_OZETI { get; set; }


        public string? HUKUKI_TALEP_KONUSU { get; set; } 


        public string? HUKUKI_DEGERLENDIRME { get; set; }



        public string? MUVEKKIL_SORULARI { get; set; }    // Bu alan gelen sorulara verilen cevaplarında tutulacağı alanıda temsil etmekte.
                                                         // Yani  gelen sorular için olusturulacak her bir childmail içinde her bir sorunun cevabının yazılacağı yeri temsil edecek.

        public string? MUVEKKILE_VERILEN_CEVAP { get; set; }    // Yeni alan




        public string? DOKUMAN_OZETI { get; set; }      



        public int MatterID { get; set; } = 3;
 
        public bool Deleted { get; set; } = false;
        public string? CreatedBy { get; set; } 
        public string? ModifiedBy { get; set; } = "Undefined";
        public string? DeletedBy { get; set; } = "Undefined";

        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }


        public int UserId { get; set; } = 1;
        public AppUser User { get; set; }


        // Navigation properties
        public Matter Matter { get; set; }
     
        public List<MailDeliveryLog> DeliveryLogs { get; set; }
        public List<MailRecipients> MailRecipients { get; set; }
        public List<MailRelation> RelationsAsParent { get; set; }
        public List<MailRelation> RelationsAsChild { get; set; }
        public List<MailLogDocuments> MailLogDocuments { get; set; }

    }
}
