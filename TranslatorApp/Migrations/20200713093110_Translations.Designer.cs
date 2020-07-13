﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TranslatorApp.Data;

namespace TranslatorApp.Migrations
{
    [DbContext(typeof(TranslatorDbContext))]
    [Migration("20200713093110_Translations")]
    partial class Translations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("TranslatorApp.Models.ErrorResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<int>("QueryId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("QueryId")
                        .IsUnique();

                    b.ToTable("ErrorResponses");
                });

            modelBuilder.Entity("TranslatorApp.Models.Query", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Call")
                        .HasColumnType("text");

                    b.Property<bool>("Success")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Queries");
                });

            modelBuilder.Entity("TranslatorApp.Models.SuccessResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("QueryId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<string>("Translated")
                        .HasColumnType("text");

                    b.Property<string>("Translation")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("QueryId")
                        .IsUnique();

                    b.ToTable("SuccessResponses");
                });

            modelBuilder.Entity("TranslatorApp.Models.Translation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Language")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Translations");
                });

            modelBuilder.Entity("TranslatorApp.Models.ErrorResponse", b =>
                {
                    b.HasOne("TranslatorApp.Models.Query", "Query")
                        .WithOne("ErrorResponse")
                        .HasForeignKey("TranslatorApp.Models.ErrorResponse", "QueryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TranslatorApp.Models.SuccessResponse", b =>
                {
                    b.HasOne("TranslatorApp.Models.Query", "Query")
                        .WithOne("SuccessResponse")
                        .HasForeignKey("TranslatorApp.Models.SuccessResponse", "QueryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
