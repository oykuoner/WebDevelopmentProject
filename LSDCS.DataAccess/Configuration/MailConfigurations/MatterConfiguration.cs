using LSDCS.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LSDCS.DataAccess.Configuration.MailConfigurations
{
    public class MatterConfiguration : IEntityTypeConfiguration<Matter>
    {
        public void Configure(EntityTypeBuilder<Matter> builder)
        {

            builder.ToTable("Matters");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.MatterName).HasMaxLength(255);


        }


    }
}
