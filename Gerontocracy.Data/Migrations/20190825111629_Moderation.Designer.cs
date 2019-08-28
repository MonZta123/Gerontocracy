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
    [Migration("20190825111629_Moderation")]
    partial class Moderation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Gerontocracy.Data.Entities.Account.Ban", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BanDate");

                    b.Property<DateTime?>("BanEnd");

                    b.Property<string>("BanLiftReason");

                    b.Property<DateTime?>("BanLifted");

                    b.Property<long>("BannedById");

                    b.Property<long>("BannedUserId");

                    b.Property<string>("Reason");

                    b.Property<long>("UnbannedById");

                    b.HasKey("Id");

                    b.HasIndex("BannedById");

                    b.HasIndex("BannedUserId");

                    b.HasIndex("UnbannedById");

                    b.ToTable("Ban");
                });

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

                    b.Property<string>("Beschreibung")
                        .IsRequired()
                        .HasMaxLength(4000);

                    b.Property<DateTime>("ErstelltAm");

                    b.Property<long?>("PolitikerId");

                    b.Property<int?>("ReputationType");

                    b.Property<string>("Titel")
                        .IsRequired()
                        .HasMaxLength(200);

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

            modelBuilder.Entity("Gerontocracy.Data.Entities.Board.Like", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("LikeType");

                    b.Property<long>("PostId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Like");
                });

            modelBuilder.Entity("Gerontocracy.Data.Entities.Board.Post", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(4000);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("Deleted");

                    b.Property<long?>("ParentId");

                    b.Property<long?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("UserId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("Gerontocracy.Data.Entities.Board.Thread", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Deleted");

                    b.Property<bool>("Generated");

                    b.Property<long>("InitialPostId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<long?>("UserId");

                    b.Property<long?>("VorfallId");

                    b.HasKey("Id");

                    b.HasIndex("InitialPostId");

                    b.HasIndex("UserId");

                    b.HasIndex("VorfallId");

                    b.ToTable("Thread");
                });

            modelBuilder.Entity("Gerontocracy.Data.Entities.News.Artikel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author")
                        .IsRequired();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Identifier")
                        .IsRequired();

                    b.Property<string>("Link")
                        .IsRequired();

                    b.Property<DateTime?>("PubDate");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<long?>("VorfallId");

                    b.HasKey("Id");

                    b.HasIndex("VorfallId");

                    b.ToTable("Artikel");
                });

            modelBuilder.Entity("Gerontocracy.Data.Entities.Party.Partei", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ExternalId");

                    b.Property<string>("Kurzzeichen");

                    b.Property<string>("Name");

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

                    b.Property<bool>("IsNationalrat");

                    b.Property<bool>("IsRegierung");

                    b.Property<string>("Nachname");

                    b.Property<long?>("ParteiId");

                    b.Property<string>("Vorname");

                    b.Property<string>("Wahlkreis");

                    b.HasKey("Id");

                    b.HasIndex("ParteiId");

                    b.ToTable("Politiker");
                });

            modelBuilder.Entity("Gerontocracy.Data.Entities.Task.Aufgabe", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("BearbeiterId");

                    b.Property<string>("Beschreibung");

                    b.Property<DateTime>("EingereichtAm");

                    b.Property<long>("EinreicherId");

                    b.Property<bool>("Erledigt");

                    b.Property<string>("MetaData");

                    b.Property<int>("TaskType");

                    b.HasKey("Id");

                    b.HasIndex("BearbeiterId");

                    b.HasIndex("EinreicherId");

                    b.ToTable("Aufgabe");
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

            modelBuilder.Entity("Gerontocracy.Data.Entities.Account.Ban", b =>
                {
                    b.HasOne("Gerontocracy.Data.Entities.Account.User", "BannedBy")
                        .WithMany()
                        .HasForeignKey("BannedById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Gerontocracy.Data.Entities.Account.User", "BannedUser")
                        .WithMany()
                        .HasForeignKey("BannedUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Gerontocracy.Data.Entities.Account.User", "UnbannedBy")
                        .WithMany()
                        .HasForeignKey("UnbannedById")
                        .OnDelete(DeleteBehavior.Cascade);
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
                        .HasForeignKey("PolitikerId");

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

            modelBuilder.Entity("Gerontocracy.Data.Entities.Board.Like", b =>
                {
                    b.HasOne("Gerontocracy.Data.Entities.Board.Post", "Post")
                        .WithMany("Likes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Gerontocracy.Data.Entities.Account.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Gerontocracy.Data.Entities.Board.Post", b =>
                {
                    b.HasOne("Gerontocracy.Data.Entities.Board.Post", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.HasOne("Gerontocracy.Data.Entities.Account.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Gerontocracy.Data.Entities.Board.Thread", b =>
                {
                    b.HasOne("Gerontocracy.Data.Entities.Board.Post", "InitialPost")
                        .WithMany()
                        .HasForeignKey("InitialPostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Gerontocracy.Data.Entities.Account.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("Gerontocracy.Data.Entities.Affair.Vorfall", "Vorfall")
                        .WithMany("Threads")
                        .HasForeignKey("VorfallId");
                });

            modelBuilder.Entity("Gerontocracy.Data.Entities.News.Artikel", b =>
                {
                    b.HasOne("Gerontocracy.Data.Entities.Affair.Vorfall", "Vorfall")
                        .WithMany()
                        .HasForeignKey("VorfallId");
                });

            modelBuilder.Entity("Gerontocracy.Data.Entities.Party.Politiker", b =>
                {
                    b.HasOne("Gerontocracy.Data.Entities.Party.Partei", "Partei")
                        .WithMany("Politiker")
                        .HasForeignKey("ParteiId");
                });

            modelBuilder.Entity("Gerontocracy.Data.Entities.Task.Aufgabe", b =>
                {
                    b.HasOne("Gerontocracy.Data.Entities.Account.User", "Bearbeiter")
                        .WithMany()
                        .HasForeignKey("BearbeiterId");

                    b.HasOne("Gerontocracy.Data.Entities.Account.User", "Einreicher")
                        .WithMany()
                        .HasForeignKey("EinreicherId")
                        .OnDelete(DeleteBehavior.Cascade);
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
