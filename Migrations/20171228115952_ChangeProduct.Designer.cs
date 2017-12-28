﻿// <auto-generated />
using historianproductionservice.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace historianproductionservice.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20171228115952_ChangeProduct")]
    partial class ChangeProduct
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("historianproductionservice.Model.Order", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("order");

                    b.HasKey("id");

                    b.ToTable("ToolTypes");
                });

            modelBuilder.Entity("historianproductionservice.Model.Product", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Orderid");

                    b.Property<int?>("Orderid1");

                    b.Property<string>("batch");

                    b.Property<long>("date");

                    b.Property<string>("product");

                    b.Property<int>("productId");

                    b.Property<double>("quantity");

                    b.HasKey("id");

                    b.HasIndex("Orderid");

                    b.HasIndex("Orderid1");

                    b.ToTable("Tools");
                });

            modelBuilder.Entity("historianproductionservice.Model.Product", b =>
                {
                    b.HasOne("historianproductionservice.Model.Order")
                        .WithMany("productsInput")
                        .HasForeignKey("Orderid");

                    b.HasOne("historianproductionservice.Model.Order")
                        .WithMany("productsOutput")
                        .HasForeignKey("Orderid1");
                });
#pragma warning restore 612, 618
        }
    }
}
