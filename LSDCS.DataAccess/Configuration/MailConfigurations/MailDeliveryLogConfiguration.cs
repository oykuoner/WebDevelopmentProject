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
    public class MailDeliveryLogConfiguration : IEntityTypeConfiguration<MailDeliveryLog>
    {
        public void Configure(EntityTypeBuilder<MailDeliveryLog> builder)
        {
            builder.ToTable("MailDeliveryLogs");

            builder.HasKey(mdl => mdl.Id);

            builder.Property(mdl => mdl.DeliveryStatus).IsRequired();

            builder.HasOne(mdl => mdl.MailLog)
                .WithMany(ml => ml.DeliveryLogs)
                .HasForeignKey(mdl => mdl.MailLogID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
