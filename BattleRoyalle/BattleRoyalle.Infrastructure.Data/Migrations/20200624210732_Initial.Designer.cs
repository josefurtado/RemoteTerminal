﻿// <auto-generated />
using System;
using BattleRoyalle.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BattleRoyalle.Infrastructure.Data.Migrations
{
    [DbContext(typeof(BattleRoyalleContext))]
    [Migration("20200624210732_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BattleRoyalle.Domain.Entities.Disk", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("FreeDiskSpace")
                        .HasColumnType("bigint");

                    b.Property<Guid>("MachineId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("TotalDiskSize")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("MachineId");

                    b.ToTable("Disks");
                });

            modelBuilder.Entity("BattleRoyalle.Domain.Entities.Machine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IpAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastConnectionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OSVersion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Machines");
                });

            modelBuilder.Entity("BattleRoyalle.Domain.Entities.Disk", b =>
                {
                    b.HasOne("BattleRoyalle.Domain.Entities.Machine", "Machine")
                        .WithMany("Disks")
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
