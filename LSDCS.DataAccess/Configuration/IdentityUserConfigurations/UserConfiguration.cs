using LSDCS.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.DataAccess.Configuration.IdentityUserConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        [Obsolete]
        public void Configure(EntityTypeBuilder<AppUser> builder)

        {

            builder.HasKey(u => u.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            builder.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

            // Maps to the AspNetUsers table

            builder.ToTable("AspNetUsers");
            // Primary key






            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.UserName).HasMaxLength(100);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(100);
            builder.Property(u => u.Email).HasMaxLength(100);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(100);

            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each User can have many UserClaims
            builder.HasMany<AppUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            builder.HasMany<AppUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            builder.HasMany<AppUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();


            // Her Kullanıcının Kullanıcının bşr rolü olabilir.
            builder.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();


            var admin = new AppUser
            {
                Id = 1,
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                PhoneNumber = "+90 555 555 55 55",
                FirtsName = "Okan",
                LastName = "ATES",
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                SecurityStamp = "1"


            };

            admin.PasswordHash = CreatePassWordHash(admin, "123456");

            var user = new AppUser
            {
                Id = 2,
                UserName = "user@gmail.com",
                NormalizedUserName = "USER@GMAIL.COM",
                Email = "user@gmail.com",
                NormalizedEmail = "USER@GMAIL.COM",
                PhoneNumber = "+90 533 333 33 33",
                FirtsName = "Kaan",
                LastName = "ILISU",
                PhoneNumberConfirmed = false,
                EmailConfirmed = false,
                SecurityStamp = "2"


            };

            user.PasswordHash = CreatePassWordHash(user, "123456");

            builder.HasData(admin, user);
        }
        private string CreatePassWordHash(AppUser user, string password)
        {

            var passwordHasher = new PasswordHasher<AppUser>();
            return passwordHasher.HashPassword(user, password);

        }


    }
}
