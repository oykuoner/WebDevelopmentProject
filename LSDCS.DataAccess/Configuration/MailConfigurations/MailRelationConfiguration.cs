using LSDCS.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LSDCS.DataAccess.Configuration.MailConfigurations
{
    public class MailRelationConfiguration : IEntityTypeConfiguration<MailRelation>
    {
        public void Configure(EntityTypeBuilder<MailRelation> builder)
        {

            builder.ToTable("MailRelations");

            builder.HasKey(mr => mr.Id);

            // builder.HasIndex(x => new { x.ParentMailLogID, x.ChildMailLogID }).IsUnique(true);


            builder.HasOne(mr => mr.ParentMailLog)
                .WithMany(ml => ml.RelationsAsParent)
                .HasForeignKey(mr => mr.ParentMailLogID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(mr => mr.ChildMailLog)
                .WithMany(ml => ml.RelationsAsChild)
                .HasForeignKey(mr => mr.ChildMailLogID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
