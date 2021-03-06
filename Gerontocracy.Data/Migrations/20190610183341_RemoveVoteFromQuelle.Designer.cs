﻿// <auto-generated />
using System;
using Gerontocracy.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Gerontocracy.Data.Migrations
{
    [DbContext(typeof(GerontocracyContext))]
    [Migration("20190610183341_RemoveVoteFromQuelle")]
    partial class RemoveVoteFromQuelle
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Gerontocracy.Data.Entities.Account.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Gerontocracy.Data.Entities.Account.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<DateTime>("RegisterDate");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Gerontocracy.Data.Entities.Affair.Quelle", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Url");

                    b.Property<long>("VorfallId");

                    b.Property<string>("Zusatz");

                    b.HasKey("Id");

                    b.HasIndex("VorfallId");

                    b.ToTable("Quelle");
                });

            modelBuilder.Entity("Gerontocracy.Data.Entities.Affair.Vorfall", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Beschreibung");

                    b.Property<DateTime>("ErstelltAm");

                    b.Property<long>("PolitikerId");

                    b.Property<int>("ReputationType");

                    b.Property<string>("Titel");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PolitikerId");

                    b.HasIndex("UserId");

                    b.ToTable("Vorfall");
                });

            modelBuilder.Entity("Gerontocracy.Data.Entities.Affair.Vote", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("UserId");

                    b.Property<long>("VorfallId");

                    b.Property<int>("VoteType");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("VorfallId");

                    b.ToTable("Vote");
                });

            modelBuilder.Entity("Gerontocracy.Data.Entities.Party.Partei", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ExternalId");

                    b.Property<string>("Kurzzeichen");

                    b.Property<string>("Name");

                    b.Property<bool>("NotActive");

                    b.HasKey("Id");

                    b.ToTable("Partei");
                });

            modelBuilder.Entity("Gerontocracy.Data.Entities.Party.Politiker", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AkadGradPost");

                    b.Property<string>("AkadGradPre");

                    b.Property<string>("Bundesland");

                    b.Property<long>("ExternalId");

                    b.Property<string>("Nachname");

                    b.Property<bool>("NotActive");

                    b.Property<long?>("ParteiId");

                    b.Property<string>("Vorname");

                    b.Property<string>("Wahlkreis");

                    b.HasKey("Id");

                    b.HasIndex("ParteiId");

                    b.ToTable("Politiker");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<long>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<long>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<long>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Gerontocracy.Data.Entities.Affair.Quelle", b =>
                {
                    b.HasOne("Gerontocracy.Data.Entities.Affair.Vorfall", "Vorfall")
                        .WithMany("Quellen")
                        .HasForeignKey("VorfallId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Gerontocracy.Data.Entities.Affair.Vorfall", b =>
                {
                    b.HasOne("Gerontocracy.Data.Entities.Party.Politiker", "Politiker")
                        .WithMany("Vorfaelle")
                        .HasForeignKey("PolitikerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Gerontocracy.Data.Entities.Account.User", "User")
                        .WithMany("Vorfaelle")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Gerontocracy.Data.Entities.Affair.Vote", b =>
                {
                    b.HasOne("Gerontocracy.Data.Entities.Account.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Gerontocracy.Data.Entities.Affair.Vorfall", "Vorfall")
                        .WithMany("Legitimitaet")
                        .HasForeignKey("VorfallId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Gerontocracy.Data.Entities.Party.Politiker", b =>
                {
                    b.HasOne("Gerontocracy.Data.Entities.Party.Partei", "Partei")
                        .WithMany("Politiker")
                        .HasForeignKey("ParteiId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.HasOne("Gerontocracy.Data.Entities.Account.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.HasOne("Gerontocracy.Data.Entities.Account.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.HasOne("Gerontocracy.Data.Entities.Account.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.HasOne("Gerontocracy.Data.Entities.Account.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Gerontocracy.Data.Entities.Account.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.HasOne("Gerontocracy.Data.Entities.Account.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
