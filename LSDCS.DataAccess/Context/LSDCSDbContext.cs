using LSDCS.DataAccess.Configuration;
using LSDCS.Entity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace LSDCS.DataAccess.Context
{
    public class LSDCSDbContext : IdentityDbContext <AppUser,AppRole,int,AppUserClaim,AppUserRole,AppUserLogin,AppRoleClaim,AppUserToken>
    {
        protected LSDCSDbContext()
        {
        }
        public LSDCSDbContext(DbContextOptions<LSDCSDbContext> options) : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // DataAccess altındaki Configuration içinde yapılan Configuration 'ları burda tanımla.

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        }
        public DbSet<MailDeliveryLog> MailDeliveryLogs { get; set; }
        public DbSet<MailLog> MailLogs { get; set; }
        public DbSet<MailRecipients> MailRecipients { get; set; }
        public DbSet<MailRelation> MailRelations { get; set; }
        public DbSet<Matter> Matters { get; set; }
        public DbSet<Recipients> Recipients { get; set; }
        public DbSet<Clients> Clients { get; set; }

        public DbSet<Documents> Documents { get; set; }

        public DbSet<MailLogDocuments> MailLogDocuments { get; set; }
       


    }
}
