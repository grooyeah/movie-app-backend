﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace movieappbackend.Migrations
{
    [DbContext(typeof(MovieAppDbContext))]
    [Migration("20231208152732_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Models.MovieList", b =>
                {
                    b.Property<string>("MovieListId")
                        .HasColumnType("text");

                    b.Property<string>("ProfileId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("MovieListId");

                    b.HasIndex("ProfileId");

                    b.ToTable("MovieLists");
                });

            modelBuilder.Entity("Models.Profile", b =>
                {
                    b.Property<string>("ProfileId")
                        .HasColumnType("text");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ProfileId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("Models.Review", b =>
                {
                    b.Property<string>("ReviewId")
                        .HasColumnType("text");

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImdbId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MovieTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProfileId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("PublishedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ReviewTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ReviewId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Models.MovieList", b =>
                {
                    b.HasOne("Models.Profile", "Profile")
                        .WithMany("MovieLists")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Models.Profile", b =>
                {
                    b.HasOne("Models.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("Models.Profile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.Review", b =>
                {
                    b.HasOne("Models.Profile", "Profile")
                        .WithMany("Reviews")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Models.Profile", b =>
                {
                    b.Navigation("MovieLists");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Navigation("Profile")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}