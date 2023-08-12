﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OnlineShop.Order.Infrastructure.Persistence;

#nullable disable

namespace OnlineShop.Order.Infrastructure.Migrations
{
    [DbContext(typeof(OrderContext))]
    [Migration("20230811021028_OnlineOrderEntityUpdate1")]
    partial class OnlineOrderEntityUpdate1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OnlineShop.Order.Domain.Entities.OnlineOrder", b =>
                {
                    b.Property<string>("OrderId")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Currency")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<bool>("IsCanceled")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsRejected")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OnlineShop.Order.Domain.Entities.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("OrderId")
                        .HasColumnType("text");

                    b.Property<string>("ProductName")
                        .HasColumnType("text");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("numeric");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<decimal>("TotalValue")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("OnlineShop.Order.Domain.Entities.OnlineOrder", b =>
                {
                    b.OwnsOne("OnlineShop.Order.Domain.Common.DateTimeRange", "DeliveryDate", b1 =>
                        {
                            b1.Property<string>("OnlineOrderOrderId")
                                .HasColumnType("text");

                            b1.Property<DateTime>("End")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("timestamp with time zone");

                            b1.HasKey("OnlineOrderOrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OnlineOrderOrderId");
                        });

                    b.OwnsOne("OnlineShop.Order.Domain.Common.Location", "DeliveryLocation", b1 =>
                        {
                            b1.Property<string>("OnlineOrderOrderId")
                                .HasColumnType("text");

                            b1.Property<string>("Latitude")
                                .HasColumnType("text");

                            b1.Property<string>("Longitude")
                                .HasColumnType("text");

                            b1.Property<string>("Name")
                                .HasColumnType("text");

                            b1.HasKey("OnlineOrderOrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OnlineOrderOrderId");
                        });

                    b.Navigation("DeliveryDate");

                    b.Navigation("DeliveryLocation");
                });

            modelBuilder.Entity("OnlineShop.Order.Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("OnlineShop.Order.Domain.Entities.OnlineOrder", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("OnlineShop.Order.Domain.Common.Money", "Price", b1 =>
                        {
                            b1.Property<Guid>("OrderItemId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric");

                            b1.Property<string>("Currency")
                                .HasColumnType("text");

                            b1.Property<string>("Unit")
                                .HasColumnType("text");

                            b1.HasKey("OrderItemId");

                            b1.ToTable("OrderItems");

                            b1.WithOwner()
                                .HasForeignKey("OrderItemId");
                        });

                    b.Navigation("Price");
                });

            modelBuilder.Entity("OnlineShop.Order.Domain.Entities.OnlineOrder", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
