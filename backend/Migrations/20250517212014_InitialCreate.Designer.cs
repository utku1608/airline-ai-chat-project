﻿// <auto-generated />
using System;
using AirlineApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AirlineApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250517212014_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.5");

            modelBuilder.Entity("AirlineApi.Models.Flight", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AirportFrom")
                        .HasColumnType("TEXT");

                    b.Property<string>("AirportTo")
                        .HasColumnType("TEXT");

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Capacity")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("TEXT");

                    b.Property<int>("Duration")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("AirlineApi.Models.Passenger", b =>
                {
                    b.Property<Guid>("FlightId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("SeatNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("FlightId", "Name");

                    b.ToTable("Passengers");
                });
#pragma warning restore 612, 618
        }
    }
}
