using LSDCS.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.DataAccess.Configuration.IdentityUserConfigurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            // Set the composite primary key for the AppUserRole entity
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps the AppUserRole entity to the "AspNetUserRoles" table in the database
            builder.ToTable("AspNetUserRoles");

            // Configures the relationship between AppUserRole and AppUser
            builder.HasOne<AppUser>() // Navigation property in AppUserRole not explicitly mentioned, so inferred
                .WithMany() // Assuming AppUser has a collection of AppUserRoles
                .HasForeignKey(ur => ur.UserId) // Specifies the foreign key in the AppUserRole entity
                .IsRequired(); // Makes the foreign key required

            // Configures the relationship between AppUserRole and AppRole
            builder.HasOne<AppRole>() // Navigation property in AppUserRole not explicitly mentioned, so inferred
                .WithMany() // Assuming AppRole has a collection of AppUserRoles
                .HasForeignKey(ur => ur.RoleId) // Specifies the foreign key in the AppUserRole entity
                .IsRequired(); // Makes the foreign key required




            // Seed data for the AppUserRole table
            builder.HasData(
                new AppUserRole
                {
                    UserId = 1,
                    RoleId = 1,
                },
                new AppUserRole
                {
                    UserId = 2,
                    RoleId = 2,
                }
            );
        }
    }
}
