﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.DAL;

#nullable disable

namespace WebAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230326133623_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Server.DAL.Models.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeparture")
                        .HasColumnType("bit");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PassengersCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("Server.DAL.Models.Leg", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("ChangePlaneStatus")
                        .HasColumnType("bit");

                    b.Property<int>("CrossingTime")
                        .HasColumnType("int");

                    b.Property<int?>("CurrFlightId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDepartureLeg")
                        .HasColumnType("bit");

                    b.Property<bool>("IsStartingLeg")
                        .HasColumnType("bit");

                    b.Property<int>("NextLegs")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CurrFlightId");

                    b.ToTable("Legs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ChangePlaneStatus = false,
                            CrossingTime = 0,
                            IsDepartureLeg = false,
                            IsStartingLeg = true,
                            NextLegs = 1,
                            Number = 256
                        },
                        new
                        {
                            Id = 2,
                            ChangePlaneStatus = false,
                            CrossingTime = 1,
                            IsDepartureLeg = false,
                            IsStartingLeg = false,
                            NextLegs = 2,
                            Number = 1
                        },
                        new
                        {
                            Id = 3,
                            ChangePlaneStatus = false,
                            CrossingTime = 2,
                            IsDepartureLeg = false,
                            IsStartingLeg = false,
                            NextLegs = 4,
                            Number = 2
                        },
                        new
                        {
                            Id = 4,
                            ChangePlaneStatus = false,
                            CrossingTime = 3,
                            IsDepartureLeg = false,
                            IsStartingLeg = false,
                            NextLegs = 8,
                            Number = 4
                        },
                        new
                        {
                            Id = 5,
                            ChangePlaneStatus = false,
                            CrossingTime = 5,
                            IsDepartureLeg = true,
                            IsStartingLeg = false,
                            NextLegs = 16,
                            Number = 8
                        },
                        new
                        {
                            Id = 6,
                            ChangePlaneStatus = false,
                            CrossingTime = 3,
                            IsDepartureLeg = false,
                            IsStartingLeg = false,
                            NextLegs = 96,
                            Number = 16
                        },
                        new
                        {
                            Id = 7,
                            ChangePlaneStatus = true,
                            CrossingTime = 10,
                            IsDepartureLeg = false,
                            IsStartingLeg = false,
                            NextLegs = 128,
                            Number = 32
                        },
                        new
                        {
                            Id = 8,
                            ChangePlaneStatus = true,
                            CrossingTime = 10,
                            IsDepartureLeg = false,
                            IsStartingLeg = false,
                            NextLegs = 128,
                            Number = 64
                        },
                        new
                        {
                            Id = 9,
                            ChangePlaneStatus = false,
                            CrossingTime = 5,
                            IsDepartureLeg = false,
                            IsStartingLeg = false,
                            NextLegs = 8,
                            Number = 128
                        });
                });

            modelBuilder.Entity("Server.DAL.Models.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("FlightId")
                        .HasColumnType("int");

                    b.Property<DateTime>("In")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LegId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Out")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.HasIndex("LegId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Server.DAL.Models.Leg", b =>
                {
                    b.HasOne("Server.DAL.Models.Flight", "CurrFlight")
                        .WithMany()
                        .HasForeignKey("CurrFlightId");

                    b.Navigation("CurrFlight");
                });

            modelBuilder.Entity("Server.DAL.Models.Log", b =>
                {
                    b.HasOne("Server.DAL.Models.Flight", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightId");

                    b.HasOne("Server.DAL.Models.Leg", "Leg")
                        .WithMany()
                        .HasForeignKey("LegId");

                    b.Navigation("Flight");

                    b.Navigation("Leg");
                });
#pragma warning restore 612, 618
        }
    }
}
