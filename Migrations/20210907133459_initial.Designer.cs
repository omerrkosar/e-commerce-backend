﻿// <auto-generated />
using System;
using E-Commerce_Backend.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace E-Commerce_Backend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210907133459_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("E-Commerce_Backend.Model.Category", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<long?>("parentid")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("parentid");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("E-Commerce_Backend.Model.Product", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long?>("categoryid")
                        .HasColumnType("bigint");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<double>("price")
                        .HasColumnType("double precision");

                    b.HasKey("id");

                    b.HasIndex("categoryid");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("E-Commerce_Backend.Model.Category", b =>
                {
                    b.HasOne("E-Commerce_Backend.Model.Category", "parent")
                        .WithMany("childs")
                        .HasForeignKey("parentid");

                    b.Navigation("parent");
                });

            modelBuilder.Entity("E-Commerce_Backend.Model.Product", b =>
                {
                    b.HasOne("E-Commerce_Backend.Model.Category", "category")
                        .WithMany("products")
                        .HasForeignKey("categoryid");

                    b.Navigation("category");
                });

            modelBuilder.Entity("E-Commerce_Backend.Model.Category", b =>
                {
                    b.Navigation("childs");

                    b.Navigation("products");
                });
#pragma warning restore 612, 618
        }
    }
}
