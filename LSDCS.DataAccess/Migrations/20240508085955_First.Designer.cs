﻿// <auto-generated />
using System;
using LSDCS.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LSDCS.DataAccess.Migrations
{
    [DbContext(typeof(LSDCSDbContext))]
    [Migration("20240508085955_First")]
    partial class First
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LSDCS.Entity.Entities.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConcurrencyStamp = "1",
                            Name = "Admin",
                            NormalizedName = "ADMİN"
                        },
                        new
                        {
                            Id = 2,
                            ConcurrencyStamp = "2",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.AppRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirtsName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "047f8c4a-2900-4769-8db1-7fc286a5edcd",
                            Email = "admin@gmail.com",
                            EmailConfirmed = true,
                            FirtsName = "Okan",
                            LastName = "ATES",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            NormalizedUserName = "ADMIN@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEJg0qAiOqbY95OgcZAxlDAOjn2aIYhjzwIpDEOoiFFruF7+JQ5J9mUgtqRwi5eL06Q==",
                            PhoneNumber = "+90 555 555 55 55",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "1",
                            TwoFactorEnabled = false,
                            UserName = "admin@gmail.com"
                        },
                        new
                        {
                            Id = 2,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "d8821cfe-6be1-421b-922a-b4aeb3d6a93f",
                            Email = "user@gmail.com",
                            EmailConfirmed = false,
                            FirtsName = "Kaan",
                            LastName = "ILISU",
                            LockoutEnabled = false,
                            NormalizedEmail = "USER@GMAIL.COM",
                            NormalizedUserName = "USER@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEIVoscsBm0ogLf7SzKrSPF4s36UIhYkGFCIOOjhylE3EjzFieggIi2WAv0wlwcxXvw==",
                            PhoneNumber = "+90 533 333 33 33",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "2",
                            TwoFactorEnabled = false,
                            UserName = "user@gmail.com"
                        });
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.AppUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.AppUserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.AppUserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1
                        },
                        new
                        {
                            UserId = 2,
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.AppUserToken", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.Clients", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Clients", (string)null);
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.Documents", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("DOKUMAN_ADI")
                        .HasColumnType("text");

                    b.Property<string>("DOKUMAN_ADI_GUID")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.MailDeliveryLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("DeliveryStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DeliveryTimeStamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("MailLogID")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MailLogID");

                    b.ToTable("MailDeliveryLogs", (string)null);
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.MailLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientID")
                        .HasColumnType("integer");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DOKUMAN_OZETI")
                        .HasColumnType("text");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("GONDERICI_ISIM")
                        .HasColumnType("text");

                    b.Property<string>("GONDERICI_MAIL")
                        .HasColumnType("text");

                    b.Property<string>("HUKUKI_DEGERLENDIRME")
                        .HasColumnType("text");

                    b.Property<string>("HUKUKI_TALEP_KONUSU")
                        .HasColumnType("text");

                    b.Property<DateTime>("MAIL_GONDERIM_TARIHI")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("MAIL_KONUSU")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("MAIL_YONU")
                        .HasColumnType("text");

                    b.Property<string>("MUVEKKILE_VERILEN_CEVAP")
                        .HasColumnType("text");

                    b.Property<string>("MUVEKKIL_SORULARI")
                        .HasColumnType("text");

                    b.Property<int>("MatterID")
                        .HasColumnType("integer");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("OLAY_OZETI")
                        .HasColumnType("text");

                    b.Property<double?>("TALEP_SURESI")
                        .HasColumnType("double precision");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClientID");

                    b.HasIndex("MatterID");

                    b.HasIndex("UserId");

                    b.ToTable("MailLogs", (string)null);
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.MailLogDocuments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DocumentId")
                        .HasColumnType("integer");

                    b.Property<int>("MailLogId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("MailLogId");

                    b.ToTable("MailLogDocuments", (string)null);
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.MailRecipients", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ALICI_TIPI")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<int>("MAIL_ALICI_ID")
                        .HasColumnType("integer");

                    b.Property<int>("MailLogID")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MAIL_ALICI_ID");

                    b.HasIndex("MailLogID");

                    b.ToTable("MailRecipients", (string)null);
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.MailRelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChildMailLogID")
                        .HasColumnType("integer");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<int>("ParentMailLogID")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ChildMailLogID");

                    b.HasIndex("ParentMailLogID");

                    b.ToTable("MailRelations", (string)null);
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.Matter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("MatterName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Matters", (string)null);
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.Recipients", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ALICI_ISIM")
                        .HasColumnType("text");

                    b.Property<string>("ALICI_MAIL")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Recipients", (string)null);
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.AppRoleClaim", b =>
                {
                    b.HasOne("LSDCS.Entity.Entities.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.AppUserClaim", b =>
                {
                    b.HasOne("LSDCS.Entity.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.AppUserLogin", b =>
                {
                    b.HasOne("LSDCS.Entity.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.AppUserRole", b =>
                {
                    b.HasOne("LSDCS.Entity.Entities.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LSDCS.Entity.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.AppUserToken", b =>
                {
                    b.HasOne("LSDCS.Entity.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.MailDeliveryLog", b =>
                {
                    b.HasOne("LSDCS.Entity.Entities.MailLog", "MailLog")
                        .WithMany("DeliveryLogs")
                        .HasForeignKey("MailLogID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("MailLog");
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.MailLog", b =>
                {
                    b.HasOne("LSDCS.Entity.Entities.Clients", "Clients")
                        .WithMany("MailLog")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LSDCS.Entity.Entities.Matter", "Matter")
                        .WithMany("MailLogs")
                        .HasForeignKey("MatterID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LSDCS.Entity.Entities.AppUser", "User")
                        .WithMany("MailLog")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clients");

                    b.Navigation("Matter");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.MailLogDocuments", b =>
                {
                    b.HasOne("LSDCS.Entity.Entities.Documents", "Document")
                        .WithMany("MailLogDocuments")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LSDCS.Entity.Entities.MailLog", "MailLog")
                        .WithMany("MailLogDocuments")
                        .HasForeignKey("MailLogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("MailLog");
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.MailRecipients", b =>
                {
                    b.HasOne("LSDCS.Entity.Entities.Recipients", "Recipients")
                        .WithMany("MailRecipients")
                        .HasForeignKey("MAIL_ALICI_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LSDCS.Entity.Entities.MailLog", "MailLog")
                        .WithMany("MailRecipients")
                        .HasForeignKey("MailLogID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MailLog");

                    b.Navigation("Recipients");
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.MailRelation", b =>
                {
                    b.HasOne("LSDCS.Entity.Entities.MailLog", "ChildMailLog")
                        .WithMany("RelationsAsChild")
                        .HasForeignKey("ChildMailLogID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LSDCS.Entity.Entities.MailLog", "ParentMailLog")
                        .WithMany("RelationsAsParent")
                        .HasForeignKey("ParentMailLogID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ChildMailLog");

                    b.Navigation("ParentMailLog");
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.AppUser", b =>
                {
                    b.Navigation("MailLog");
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.Clients", b =>
                {
                    b.Navigation("MailLog");
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.Documents", b =>
                {
                    b.Navigation("MailLogDocuments");
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.MailLog", b =>
                {
                    b.Navigation("DeliveryLogs");

                    b.Navigation("MailLogDocuments");

                    b.Navigation("MailRecipients");

                    b.Navigation("RelationsAsChild");

                    b.Navigation("RelationsAsParent");
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.Matter", b =>
                {
                    b.Navigation("MailLogs");
                });

            modelBuilder.Entity("LSDCS.Entity.Entities.Recipients", b =>
                {
                    b.Navigation("MailRecipients");
                });
#pragma warning restore 612, 618
        }
    }
}
