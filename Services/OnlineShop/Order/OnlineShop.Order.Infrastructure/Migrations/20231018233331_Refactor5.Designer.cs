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
    [Migration("20231018233331_Refactor5")]
    partial class Refactor5
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OnlineShop.Order.Domain.OnlineOrders.OnlineOrder", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CompletionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OnlineShop.Order.Domain.OrderItems.OrderItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("OrderId")
                        .HasColumnType("text");

                    b.Property<string>("ProductName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("OnlineShop.Order.Domain.OnlineOrders.OnlineOrder", b =>
                {
                    b.OwnsOne("Shared.Domain.Money.Money", "TotalPrice", b1 =>
                        {
                            b1.Property<string>("OnlineOrderId")
                                .HasColumnType("text");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Unit")
                                .HasColumnType("text");

                            b1.HasKey("OnlineOrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OnlineOrderId");
                        });

                    b.OwnsOne("OnlineShop.Order.Domain.OnlineOrders.OrderStatus", "Status", b1 =>
                        {
                            b1.Property<string>("OnlineOrderId")
                                .HasColumnType("text");

                            b1.Property<string>("StatusMessage")
                                .HasColumnType("text");

                            b1.HasKey("OnlineOrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OnlineOrderId");
                        });

                    b.OwnsOne("Shared.Domain.DateTimeRange.DateTimeRange", "DeliveryDate", b1 =>
                        {
                            b1.Property<string>("OnlineOrderId")
                                .HasColumnType("text");

                            b1.Property<DateTime>("End")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("timestamp with time zone");

                            b1.HasKey("OnlineOrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OnlineOrderId");
                        });

                    b.OwnsOne("Shared.Domain.Location.Location", "DeliveryLocation", b1 =>
                        {
                            b1.Property<string>("OnlineOrderId")
                                .HasColumnType("text");

                            b1.Property<string>("Latitude")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Longitude")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("OnlineOrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OnlineOrderId");
                        });

                    b.Navigation("DeliveryDate");

                    b.Navigation("DeliveryLocation");

                    b.Navigation("Status");

                    b.Navigation("TotalPrice");
                });

            modelBuilder.Entity("OnlineShop.Order.Domain.OrderItems.OrderItem", b =>
                {
                    b.HasOne("OnlineShop.Order.Domain.OnlineOrders.OnlineOrder", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("Shared.Domain.Money.Money", "Price", b1 =>
                        {
                            b1.Property<string>("OrderItemId")
                                .HasColumnType("text");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Unit")
                                .HasColumnType("text");

                            b1.HasKey("OrderItemId");

                            b1.ToTable("OrderItems");

                            b1.WithOwner()
                                .HasForeignKey("OrderItemId");
                        });

                    b.OwnsOne("Shared.Domain.Money.Money", "TotalValue", b1 =>
                        {
                            b1.Property<string>("OrderItemId")
                                .HasColumnType("text");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Unit")
                                .HasColumnType("text");

                            b1.HasKey("OrderItemId");

                            b1.ToTable("OrderItems");

                            b1.WithOwner()
                                .HasForeignKey("OrderItemId");
                        });

                    b.OwnsOne("Shared.Domain.Quantity.Quantity", "Quantity", b1 =>
                        {
                            b1.Property<string>("OrderItemId")
                                .HasColumnType("text");

                            b1.Property<string>("Unit")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<decimal>("Value")
                                .HasColumnType("numeric");

                            b1.HasKey("OrderItemId");

                            b1.ToTable("OrderItems");

                            b1.WithOwner()
                                .HasForeignKey("OrderItemId");
                        });

                    b.Navigation("Price");

                    b.Navigation("Quantity");

                    b.Navigation("TotalValue");
                });

            modelBuilder.Entity("OnlineShop.Order.Domain.OnlineOrders.OnlineOrder", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
