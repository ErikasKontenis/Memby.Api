﻿// <auto-generated />
using System;
using Memby.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Memby.Data.Migrations
{
    [DbContext(typeof(MembyDbContext))]
    [Migration("20181020214335_v2")]
    partial class v2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Memby.Domain.Companies.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("BrandName");

                    b.Property<string>("Logo");

                    b.Property<string>("Name");

                    b.Property<string>("RegistrationNumber");

                    b.Property<int>("UserId");

                    b.Property<string>("VatNumber");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Memby.Domain.Employees.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompanyId");

                    b.Property<string>("Position");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("UserId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Memby.Domain.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email");

                    b.Property<int>("GenderId");

                    b.Property<bool>("IsIndividualOffersEnabled");

                    b.Property<bool>("IsNewOffersEnabled");

                    b.Property<bool>("IsSystemNotificationsEnabled");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Email", "Password")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Memby.Domain.Users.UserProvider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ProviderId");

                    b.Property<int>("UserId");

                    b.Property<string>("Uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("Uuid")
                        .IsUnique();

                    b.ToTable("UserProviders");
                });

            modelBuilder.Entity("Memby.Domain.Users.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Memby.Domain.Companies.Company", b =>
                {
                    b.HasOne("Memby.Domain.Users.User", "User")
                        .WithMany("Companies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Memby.Domain.Employees.Employee", b =>
                {
                    b.HasOne("Memby.Domain.Companies.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Memby.Domain.Users.User", "User")
                        .WithMany("Employees")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Memby.Domain.Users.UserProvider", b =>
                {
                    b.HasOne("Memby.Domain.Users.User", "User")
                        .WithMany("UserProviders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Memby.Domain.Users.UserRole", b =>
                {
                    b.HasOne("Memby.Domain.Users.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
