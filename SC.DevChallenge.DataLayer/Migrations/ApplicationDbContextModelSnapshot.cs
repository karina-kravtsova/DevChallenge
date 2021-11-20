﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SC.DevChallenge.DataLayer.Db;

namespace SC.DevChallenge.DataLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("SC.DevChallenge.DataLayer.Tables.FinanceInstrument", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Instrument")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Owner")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Portfolio")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("TimeSlot")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("FinanceInstrument");
                });
#pragma warning restore 612, 618
        }
    }
}
