﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using infraAlerta.Data;

#nullable disable

namespace infraAlerta.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    partial class ApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("infraAlerta.Models.Comment", b =>
                {
                    b.Property<int>("comment_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("comment_id"));

                    b.Property<string>("comment_text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("created_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<DateTime>("created_at"));

                    b.Property<int>("problem_id")
                        .HasColumnType("int");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("comment_id");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("infraAlerta.Models.Problem", b =>
                {
                    b.Property<int>("pro_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("pro_id"));

                    b.Property<int>("pro_admin")
                        .HasColumnType("int");

                    b.Property<string>("pro_classification")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("pro_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("pro_photo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("pro_status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("pro_user")
                        .HasColumnType("int");

                    b.HasKey("pro_id");

                    b.ToTable("Problem");
                });

            modelBuilder.Entity("infraAlerta.Models.Problem_address", b =>
                {
                    b.Property<int>("pa_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("pa_id"));

                    b.Property<string>("pa_address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("pa_city")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("pa_neighborhood")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("pa_number")
                        .HasColumnType("int");

                    b.Property<int>("pa_problem_id")
                        .HasColumnType("int");

                    b.Property<string>("pa_state")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("pa_id");

                    b.ToTable("Problem_Address");
                });

            modelBuilder.Entity("infraAlerta.Models.User", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("user_id"));

                    b.Property<bool>("admin")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("birthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("cpf")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("user_id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("infraAlerta.Models.User_address", b =>
                {
                    b.Property<int>("ua_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ua_id"));

                    b.Property<string>("ua_address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ua_city")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ua_neighborhood")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ua_number")
                        .HasColumnType("int");

                    b.Property<string>("ua_state")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ua_user_id")
                        .HasColumnType("int");

                    b.HasKey("ua_id");

                    b.ToTable("User_Address");
                });
#pragma warning restore 612, 618
        }
    }
}
