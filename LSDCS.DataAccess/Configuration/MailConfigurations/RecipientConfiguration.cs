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
    public class RecipientConfiguration : IEntityTypeConfiguration<Recipients>
    {

        public void Configure(EntityTypeBuilder<Recipients> builder)
        {
            builder.ToTable("Recipients");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.ALICI_MAIL).HasMaxLength(255);
        }


    }
}
