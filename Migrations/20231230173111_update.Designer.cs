﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ResetPassword1.Database;

#nullable disable

namespace ResetPassword1.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231230173111_update")]
    partial class update
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("ResetPassword1.Models.User", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("PasswordResetToken")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("PasswordResetTokenExpires")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("VerificationToken")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("VerifiedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Guid");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
