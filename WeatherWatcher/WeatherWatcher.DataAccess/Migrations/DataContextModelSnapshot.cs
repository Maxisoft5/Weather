﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherWatcher.DataAccess.EFCore;

#nullable disable

namespace WeatherWatcher.DataAccess.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WeatherWatcher.Domain.Entities.LoadArchiveStatus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("AddedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LoadStatus")
                        .HasColumnType("int");

                    b.Property<int>("RowsCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("LoadArchiveStatuses");
                });

            modelBuilder.Entity("WeatherWatcher.Domain.Entities.WeatherInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("AddedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("AtmospherePressure")
                        .HasColumnType("int");

                    b.Property<int>("BottomLineCloudiness")
                        .HasColumnType("int");

                    b.Property<int>("Cloudiness")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("DewPointTemperature")
                        .HasColumnType("float");

                    b.Property<int>("HorizontalVisibility")
                        .HasColumnType("int");

                    b.Property<long?>("LoadArchiveStatusId")
                        .HasColumnType("bigint");

                    b.Property<int>("RelativeHumidity")
                        .HasColumnType("int");

                    b.Property<string>("WeatherPhenomena")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WindDirection")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WindSpeed")
                        .HasColumnType("int");

                    b.Property<double>("WindTemperature")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("LoadArchiveStatusId");

                    b.ToTable("WeatherInfos");
                });

            modelBuilder.Entity("WeatherWatcher.Domain.Entities.WeatherInfo", b =>
                {
                    b.HasOne("WeatherWatcher.Domain.Entities.LoadArchiveStatus", "LoadArchiveStatus")
                        .WithMany("WeatherInfos")
                        .HasForeignKey("LoadArchiveStatusId");

                    b.Navigation("LoadArchiveStatus");
                });

            modelBuilder.Entity("WeatherWatcher.Domain.Entities.LoadArchiveStatus", b =>
                {
                    b.Navigation("WeatherInfos");
                });
#pragma warning restore 612, 618
        }
    }
}
