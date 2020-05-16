﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200516213730_PocoFix")]
    partial class PocoFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Data.Medcin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Medcins");
                });

            modelBuilder.Entity("Data.MedcinParam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("MedcinId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ParamsValueId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("MedcinParams");
                });

            modelBuilder.Entity("Data.Param", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("HasUnit")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Params");
                });

            modelBuilder.Entity("Data.ParamValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ParamId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ParamValues");
                });

            modelBuilder.Entity("Data.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Answers")
                        .HasColumnType("text");

                    b.Property<bool>("IsFirst")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsLast")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ParamId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("QuestionTreeId")
                        .HasColumnType("uuid");

                    b.Property<int>("ResponseType")
                        .HasColumnType("integer");

                    b.Property<string>("Scenario")
                        .HasColumnType("text");

                    b.Property<int>("ScenarioType")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ParamId");

                    b.HasIndex("QuestionTreeId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Data.QuestionTree", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("QuestionTrees");
                });

            modelBuilder.Entity("Data.QuestionTreeHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("MedcinId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ScenarioId")
                        .HasColumnType("uuid");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("QuestionTreeHistories");
                });

            modelBuilder.Entity("Data.Recept", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Template")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Recepts");
                });

            modelBuilder.Entity("Data.ReceptParam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ParamId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ReceptId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ReceptRowId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("ReceptParams");
                });

            modelBuilder.Entity("Data.ReceptRow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ReceptId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("ReceptRows");
                });

            modelBuilder.Entity("Data.Scenario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Finished")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Scenarios");
                });

            modelBuilder.Entity("Data.Question", b =>
                {
                    b.HasOne("Data.Param", "Param")
                        .WithMany()
                        .HasForeignKey("ParamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.QuestionTree", "QuestionTree")
                        .WithMany()
                        .HasForeignKey("QuestionTreeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.QuestionTreeHistory", b =>
                {
                    b.HasOne("Data.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
