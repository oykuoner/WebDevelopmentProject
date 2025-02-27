using FluentValidation;
using LSDCS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Service.FluenValidations
{
    public class MailLogValidator : AbstractValidator<MailLog>
    {

        public MailLogValidator()
        {
            RuleFor(x => x.GONDERICI_MAIL).NotEmpty().WithMessage("Gönderen e-posta adresi gereklidir.").NotNull().EmailAddress().WithMessage("Gönderen geçerli bir e-posta adresi olmalıdır.");

            RuleFor(x => x.MAIL_KONUSU).NotEmpty().WithMessage("MAİL KONUSU , bu alan boş olamaz").NotNull();

            //RuleFor(x => x.HUKUKI_DEGERLENDIRME).NotEmpty().WithMessage("Bu alana mesaj yazmak zorunludur").NotNull().WithMessage("Bu alan boş olamaz");

            RuleFor(x => x.MAIL_GONDERIM_TARIHI).NotEmpty().WithMessage("Mail gönderim tarihini seçin").NotNull();

            RuleFor(x => x.MAIL_YONU).NotEmpty().WithMessage("Mail gönderim yönünü seçin (Gelen ya da Giden olarak.)").NotNull();

            RuleFor(x => x.ClientID).NotEmpty().WithMessage("Client seçin").NotNull();

            RuleFor(x => x.MatterID).NotEmpty().WithMessage("Matter seçin").NotNull();

          
            RuleFor( x=>x.HUKUKI_TALEP_KONUSU).NotEmpty().WithMessage("HUKUKI TALEP KONUSU, bu alan boş olamaz").NotNull();

            RuleFor(x => x.OLAY_OZETI).NotEmpty().WithMessage("OLAY ÖZETİ, bu alan boş olamaz").NotNull();

            RuleFor(x => x.GONDERICI_ISIM).NotEmpty().WithMessage("GÖNDERİCİ İSİM-SOYİSİM, bu alan boş olamaz").NotNull();







        }
    }
}
