﻿// <auto-generated />
using System;
using Ecommerce.Order.API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ecommerce.Order.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Ecommerce.Domain.Entities.OrderEntities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("ExternalPaymentId")
                        .HasColumnType("longtext")
                        .HasColumnName("external_payment_id");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int")
                        .HasColumnName("payment_method");

                    b.Property<string>("ShippingBuildingNumber")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("shipping_building_number");

                    b.Property<string>("ShippingCity")
                        .HasColumnType("longtext")
                        .HasColumnName("shipping_city");

                    b.Property<string>("ShippingComplement")
                        .HasColumnType("longtext")
                        .HasColumnName("shipping_complement");

                    b.Property<decimal>("ShippingCost")
                        .HasPrecision(65, 2)
                        .HasColumnType("decimal(65,2)")
                        .HasColumnName("shipping_cost");

                    b.Property<string>("ShippingCountry")
                        .HasColumnType("longtext")
                        .HasColumnName("shipping_country");

                    b.Property<string>("ShippingNeighborhood")
                        .HasColumnType("longtext")
                        .HasColumnName("shipping_neighborhood");

                    b.Property<string>("ShippingPostalCode")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("shipping_postal_code");

                    b.Property<string>("ShippingState")
                        .HasColumnType("longtext")
                        .HasColumnName("shipping_state");

                    b.Property<string>("ShippingStreetName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("shipping_street_name");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_order");

                    b.ToTable("order", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.OrderEntities.OrderHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("date");

                    b.Property<string>("Notes")
                        .HasColumnType("longtext")
                        .HasColumnName("notes");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("char(36)")
                        .HasColumnName("order_id");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_order_history");

                    b.HasIndex("OrderId")
                        .HasDatabaseName("ix_order_history_order_id");

                    b.ToTable("order_history", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.OrderEntities.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("OrderId")
                        .HasColumnType("char(36)")
                        .HasColumnName("order_id");

                    b.Property<Guid>("ProductCombinationId")
                        .HasColumnType("char(36)")
                        .HasColumnName("product_combination_id");

                    b.Property<decimal?>("ProductDiscount")
                        .HasPrecision(65, 2)
                        .HasColumnType("decimal(65,2)")
                        .HasColumnName("product_discount");

                    b.Property<string>("ProductImagePath")
                        .HasColumnType("longtext")
                        .HasColumnName("product_image_path");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("product_name");

                    b.Property<string>("ProductSku")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("product_sku");

                    b.Property<decimal>("ProductUnitPrice")
                        .HasPrecision(65, 2)
                        .HasColumnType("decimal(65,2)")
                        .HasColumnName("product_unit_price");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.HasKey("Id")
                        .HasName("pk_order_item");

                    b.HasIndex("OrderId")
                        .HasDatabaseName("ix_order_item_order_id");

                    b.ToTable("order_item", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.OrderEntities.OrderHistory", b =>
                {
                    b.HasOne("Ecommerce.Domain.Entities.OrderEntities.Order", "Order")
                        .WithMany("History")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_history_orders_order_id");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.OrderEntities.OrderItem", b =>
                {
                    b.HasOne("Ecommerce.Domain.Entities.OrderEntities.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_item_orders_order_id");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.OrderEntities.Order", b =>
                {
                    b.Navigation("History");

                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
