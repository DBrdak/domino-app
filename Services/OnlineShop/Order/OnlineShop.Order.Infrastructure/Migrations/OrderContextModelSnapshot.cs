﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OnlineShop.Order.Infrastructure.Persistence;

#nullable disable

namespace OnlineShop.Order.Infrastructure.Migrations
{
    [DbContext(typeof(OrderContext))]
    partial class OrderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShopId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OnlineShop.Order.Domain.OrderItems.OrderItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProductName")
                        .IsRequired()
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
                                .IsRequired()
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

                    b.Navigation("DeliveryDate")
                        .IsRequired();

                    b.Navigation("DeliveryLocation")
                        .IsRequired();

                    b.Navigation("Status")
                        .IsRequired();

                    b.Navigation("TotalPrice")
                        .IsRequired();
                });

            modelBuilder.Entity("OnlineShop.Order.Domain.OrderItems.OrderItem", b =>
                {
                    b.HasOne("OnlineShop.Order.Domain.OnlineOrders.OnlineOrder", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

                    b.Navigation("Price")
                        .IsRequired();

                    b.Navigation("Quantity")
                        .IsRequired();

                    b.Navigation("TotalValue")
                        .IsRequired();
                });

            modelBuilder.Entity("OnlineShop.Order.Domain.OnlineOrders.OnlineOrder", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
