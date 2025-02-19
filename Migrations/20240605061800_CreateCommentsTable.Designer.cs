﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using infraAlerta.Data;

#nullable disable

namespace InfraAlerta.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20240605061800_CreateCommentsTable")]
    partial class CreateCommentsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("infraAlerta.Models.Comments", b =>
                {
                    b.Property<int>("comments_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("pro_id")
                        .HasColumnType("int");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.Property<string>("comments_text")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasAnnotation("MySql:CharSet", "utf8mb4");

                    b.Property<DateTime>("created_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.HasKey("comments_id");

                    b.HasIndex("pro_id");

                    b.HasIndex("user_id");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("infraAlerta.Models.Comments", b =>
                {
                    b.HasOne("infraAlerta.Models.Problem", "Problem")
                        .WithMany("Comments")
                        .HasForeignKey("pro_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("infraAlerta.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Problem");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
