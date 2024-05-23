﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SmileMate.Common;

#nullable disable

namespace SmileMate.Migrations
{
    [DbContext(typeof(SmileMateContext))]
    partial class SmileMateContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SmileMate.Common.Entities.Doctor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("ReceptionId")
                        .HasColumnType("bigint");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time without time zone");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ReceptionId")
                        .IsUnique();

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("SmileMate.Common.Entities.MedicalHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Diagnosis")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhotoPath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("MedicalHistories");
                });

            modelBuilder.Entity("SmileMate.Common.Entities.Patient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("BirthdayDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("ReceptionId")
                        .HasColumnType("bigint");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ReceptionId")
                        .IsUnique();

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("SmileMate.Common.Entities.Reception", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("MedicalHistoryId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("ReceptionDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("MedicalHistoryId")
                        .IsUnique();

                    b.ToTable("Receptions");
                });

            modelBuilder.Entity("SmileMate.Common.Entities.Doctor", b =>
                {
                    b.HasOne("SmileMate.Common.Entities.Reception", "Reception")
                        .WithOne("Doctor")
                        .HasForeignKey("SmileMate.Common.Entities.Doctor", "ReceptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reception");
                });

            modelBuilder.Entity("SmileMate.Common.Entities.Patient", b =>
                {
                    b.HasOne("SmileMate.Common.Entities.Reception", "Reception")
                        .WithOne("Client")
                        .HasForeignKey("SmileMate.Common.Entities.Patient", "ReceptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reception");
                });

            modelBuilder.Entity("SmileMate.Common.Entities.Reception", b =>
                {
                    b.HasOne("SmileMate.Common.Entities.MedicalHistory", "MedicalHistory")
                        .WithOne("Reception")
                        .HasForeignKey("SmileMate.Common.Entities.Reception", "MedicalHistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicalHistory");
                });

            modelBuilder.Entity("SmileMate.Common.Entities.MedicalHistory", b =>
                {
                    b.Navigation("Reception")
                        .IsRequired();
                });

            modelBuilder.Entity("SmileMate.Common.Entities.Reception", b =>
                {
                    b.Navigation("Client")
                        .IsRequired();

                    b.Navigation("Doctor")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
