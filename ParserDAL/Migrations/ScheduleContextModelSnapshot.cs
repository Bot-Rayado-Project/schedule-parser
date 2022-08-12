﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ParserDAL.DataAccess;

#nullable disable

namespace ParserDAL.Migrations
{
    [DbContext(typeof(ScheduleContext))]
    partial class ScheduleContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ParserDAL.Models.HeadmanAnnotation", b =>
                {
                    b.Property<string>("stream_group")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("day")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("parity")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("annotation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("stream_group", "day", "parity");

                    b.ToTable("HeadmanAnnotations");
                });

            modelBuilder.Entity("ParserDAL.Models.HeadmanChange", b =>
                {
                    b.Property<string>("stream_group")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("day")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("parity")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("changes")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("pair_number")
                        .HasColumnType("integer");

                    b.HasKey("stream_group", "day", "parity");

                    b.ToTable("HeadmanChanges");
                });

            modelBuilder.Entity("ParserDAL.Models.HeadmanSchedule", b =>
                {
                    b.Property<string>("stream_group")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("day")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("parity")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("schedule")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("stream_group", "day", "parity");

                    b.ToTable("HeadmanSchedules");
                });

            modelBuilder.Entity("ParserDAL.Models.PersonalAnnotation", b =>
                {
                    b.Property<int>("id")
                        .HasColumnType("integer");

                    b.Property<string>("stream_group")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("day")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("parity")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("annotation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id", "stream_group", "day", "parity");

                    b.ToTable("PersonalAnnotations");
                });

            modelBuilder.Entity("ParserDAL.Models.PersonalChange", b =>
                {
                    b.Property<int>("id")
                        .HasColumnType("integer");

                    b.Property<string>("stream_group")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("day")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("parity")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("changes")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("pair_number")
                        .HasColumnType("integer");

                    b.HasKey("id", "stream_group", "day", "parity");

                    b.ToTable("PersonalChanges");
                });

            modelBuilder.Entity("ParserDAL.Models.PersonalSchedule", b =>
                {
                    b.Property<int>("id")
                        .HasColumnType("integer");

                    b.Property<string>("stream_group")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("day")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("parity")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("schedule")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id", "stream_group", "day", "parity");

                    b.ToTable("PersonalSchedules");
                });

            modelBuilder.Entity("ParserDAL.Models.SharedSchedule", b =>
                {
                    b.Property<string>("stream_group")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("day")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("parity")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("schedule")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("stream_group", "day", "parity");

                    b.ToTable("SharedSchedules");
                });
#pragma warning restore 612, 618
        }
    }
}
