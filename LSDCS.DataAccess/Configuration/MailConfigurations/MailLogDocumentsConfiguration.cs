using LSDCS.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.DataAccess.Configuration.MailConfigurations
{
    public class MailLogDocumentsConfiguration : IEntityTypeConfiguration<MailLogDocuments>
    {
        public void Configure(EntityTypeBuilder<MailLogDocuments> builder)
        {


            builder.ToTable("MailLogDocuments");

            builder.HasKey(md => md.Id);


            builder.HasOne(md => md.MailLog)
              .WithMany(m => m.MailLogDocuments)
              .HasForeignKey(md => md.MailLogId);

            builder.HasOne(md => md.Document)
                .WithMany(d => d.MailLogDocuments)
                .HasForeignKey(md => md.DocumentId);


        }
    }
}
