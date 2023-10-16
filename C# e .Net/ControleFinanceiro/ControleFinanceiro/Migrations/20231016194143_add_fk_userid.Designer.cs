﻿// <auto-generated />
using System;
using ControleFinanceiro.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ControleFinanceiro.Migrations
{
    [DbContext(typeof(ControleFinanceiroDbContext))]
    [Migration("20231016194143_add_fk_userid")]
    partial class add_fk_userid
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ControleFinanceiro.Models.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Cost")
                        .HasColumnType("double");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Expense");
                });

            modelBuilder.Entity("ControleFinanceiro.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Balance")
                        .HasColumnType("double");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ControleFinanceiro.Models.Expense", b =>
                {
                    b.HasOne("ControleFinanceiro.Models.User", "User")
                        .WithMany("Expenses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ControleFinanceiro.Models.User", b =>
                {
                    b.Navigation("Expenses");
                });
#pragma warning restore 612, 618
        }
    }
}
