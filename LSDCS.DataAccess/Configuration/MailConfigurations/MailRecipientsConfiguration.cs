using LSDCS.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LSDCS.DataAccess.Configuration.MailConfigurations
{
    public class MailRecipientsConfiguration : IEntityTypeConfiguration<MailRecipients>
    {
        public void Configure(EntityTypeBuilder<MailRecipients> builder)
        {
            builder.ToTable("MailRecipients");

            builder.HasKey(mr => mr.Id);

            builder.Property(mr => mr.ALICI_TIPI).HasMaxLength(10);



            builder.HasOne(mr => mr.MailLog)
                .WithMany(ml => ml.MailRecipients)
                .HasForeignKey(mr => mr.MailLogID);

            // Eğer Recipient ile ilişki varsa:
            builder.HasOne(mr => mr.Recipients)
                .WithMany(r => r.MailRecipients)
                .HasForeignKey(mr => mr.MAIL_ALICI_ID);
        }
    }
}
