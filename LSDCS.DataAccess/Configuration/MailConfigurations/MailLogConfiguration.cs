using LSDCS.Entity.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.DataAccess.Configuration.MailConfigurations
{
    public class MailLogConfiguration : IEntityTypeConfiguration<MailLog>
    {
        public void Configure(EntityTypeBuilder<MailLog> builder)
        {

            builder.ToTable("MailLogs");

            builder.HasKey(ml => ml.Id);


            builder.Property(ml => ml.MAIL_KONUSU).IsRequired().HasMaxLength(255);


            //builder.Property(ml => ml.HUKUKI_DEGERLENDIRME).IsRequired().HasMaxLength(255);
            //builder.Property(ml => ml.OLAY_OZETI).IsRequired().HasMaxLength(255);
            //builder.Property(ml => ml.DOKUMAN_OZETI).IsRequired().HasMaxLength(255);
            //builder.Property(ml => ml.MAIL_YONU).IsRequired();
            //builder.Property(ml => ml.MAIL_GONDERIM_TARIHI).IsRequired();
            builder.Property(ml => ml.MatterID).IsRequired();

            builder.HasOne(ml => ml.Matter)
                .WithMany(m => m.MailLogs)
                .HasForeignKey(ml => ml.MatterID);

            builder.HasOne(ml => ml.Clients)
               .WithMany(m => m.MailLog)
               .HasForeignKey(ml => ml.ClientID);




        }
    }
}
