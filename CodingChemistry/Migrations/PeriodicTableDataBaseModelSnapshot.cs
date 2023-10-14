﻿// <auto-generated />
using System;
using CodingChemistry;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CodingChemistry.Migrations
{
    [DbContext(typeof(PeriodicTableDataBase))]
    partial class PeriodicTableDataBaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("Element", b =>
                {
                    b.Property<int>("AtomicNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AbbreviatedElectronConfiguration")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("AtomicMass")
                        .HasColumnType("REAL");

                    b.Property<string>("ElectronConfiguration")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float?>("Electronegativity")
                        .HasColumnType("REAL");

                    b.Property<int>("Group")
                        .HasColumnType("INTEGER");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Period")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("AtomicNumber");

                    b.ToTable("elements");
                });
#pragma warning restore 612, 618
        }
    }
}
