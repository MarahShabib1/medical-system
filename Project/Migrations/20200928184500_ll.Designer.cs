﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.Data;

namespace Project.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200928184500_ll")]
    partial class ll
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Project.Models.Company", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("id");

                    b.ToTable("company");
                });

            modelBuilder.Entity("Project.Models.Doctor", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address1");

                    b.Property<string>("Address2");

                    b.Property<int>("Userid");

                    b.HasKey("id");

                    b.HasIndex("Userid");

                    b.ToTable("doctor");
                });

            modelBuilder.Entity("Project.Models.Employee", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<int>("Doctorid");

                    b.Property<int>("Userid");

                    b.HasKey("id");

                    b.HasIndex("Doctorid");

                    b.HasIndex("Userid");

                    b.ToTable("employee");
                });

            modelBuilder.Entity("Project.Models.Medicine", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("Prescriptionid");

                    b.Property<int?>("companyid");

                    b.HasKey("id");

                    b.HasIndex("Prescriptionid");

                    b.HasIndex("companyid");

                    b.ToTable("medicine");
                });

            modelBuilder.Entity("Project.Models.Prescription", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ExtraInfo");

                    b.Property<string>("LabTest");

                    b.HasKey("id");

                    b.ToTable("prescription");
                });

            modelBuilder.Entity("Project.Models.Records", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApprovedBy");

                    b.Property<string>("Case");

                    b.Property<int>("Employeeid");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("ExtraInfo");

                    b.Property<int>("Prescriptionid");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("Userid");

                    b.Property<int>("status");

                    b.HasKey("id");

                    b.HasIndex("Employeeid");

                    b.HasIndex("Prescriptionid");

                    b.HasIndex("Userid");

                    b.ToTable("records");
                });

            modelBuilder.Entity("Project.Models.Role", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("id");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("Project.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("_Login");

                    b.Property<string>("email");

                    b.Property<string>("phonenumber");

                    b.Property<string>("pwd");

                    b.HasKey("id");

                    b.ToTable("user1");
                });

            modelBuilder.Entity("Project.Models.UserRole", b =>
                {
                    b.Property<int>("Userid");

                    b.Property<int>("Roleid");

                    b.HasKey("Userid", "Roleid");

                    b.HasIndex("Roleid");

                    b.ToTable("user_role");
                });

            modelBuilder.Entity("Project.Models.Doctor", b =>
                {
                    b.HasOne("Project.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("Userid")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Models.Employee", b =>
                {
                    b.HasOne("Project.Models.Doctor", "doctor")
                        .WithMany()
                        .HasForeignKey("Doctorid")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Project.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("Userid")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Project.Models.Medicine", b =>
                {
                    b.HasOne("Project.Models.Prescription")
                        .WithMany("medicines")
                        .HasForeignKey("Prescriptionid");

                    b.HasOne("Project.Models.Company", "company")
                        .WithMany()
                        .HasForeignKey("companyid");
                });

            modelBuilder.Entity("Project.Models.Records", b =>
                {
                    b.HasOne("Project.Models.Employee", "employee")
                        .WithMany()
                        .HasForeignKey("Employeeid")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Models.Prescription", "prescription")
                        .WithMany()
                        .HasForeignKey("Prescriptionid")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("Userid")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Models.UserRole", b =>
                {
                    b.HasOne("Project.Models.Role", "role")
                        .WithMany("user_role")
                        .HasForeignKey("Roleid")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Models.User", "user")
                        .WithMany("user_role")
                        .HasForeignKey("Userid")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
