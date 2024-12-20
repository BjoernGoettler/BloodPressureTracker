﻿// <auto-generated />
using System;
using DatabaseService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DatabaseService.Migrations
{
    [DbContext(typeof(MeasurementsContext))]
    [Migration("20241106183513_initialmigration")]
    partial class initialmigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DatabaseService.Models.DoctorModel", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("DoctorId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("DatabaseService.Models.MeasurementsModel", b =>
                {
                    b.Property<int>("MeasurementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Diastolic")
                        .HasColumnType("int");

                    b.Property<int>("SeenDoctorId")
                        .HasColumnType("int");

                    b.Property<int>("Systolic")
                        .HasColumnType("int");

                    b.HasKey("MeasurementId");

                    b.HasIndex("SeenDoctorId");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("DatabaseService.Models.PatientModel", b =>
                {
                    b.Property<string>("Ssn")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("MeasurementsMeasurementId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Ssn");

                    b.HasIndex("MeasurementsMeasurementId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("DatabaseService.Models.MeasurementsModel", b =>
                {
                    b.HasOne("DatabaseService.Models.DoctorModel", "Seen")
                        .WithMany()
                        .HasForeignKey("SeenDoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seen");
                });

            modelBuilder.Entity("DatabaseService.Models.PatientModel", b =>
                {
                    b.HasOne("DatabaseService.Models.MeasurementsModel", "Measurements")
                        .WithMany("Patients")
                        .HasForeignKey("MeasurementsMeasurementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Measurements");
                });

            modelBuilder.Entity("DatabaseService.Models.MeasurementsModel", b =>
                {
                    b.Navigation("Patients");
                });
#pragma warning restore 612, 618
        }
    }
}
