﻿// <auto-generated />
using System;
using Ecommerce.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ecommerce.Infra.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230611230211_Updating-Address")]
    partial class UpdatingAddress
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Ecommerce.Domain.Entities.Address", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("AdditionalInformation")
                        .HasColumnType("longtext")
                        .HasColumnName("additional_information");

                    b.Property<string>("BuildingNumber")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("building_number");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("city");

                    b.Property<string>("Complement")
                        .HasColumnType("longtext")
                        .HasColumnName("complement");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("country");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("Neighborhood")
                        .HasColumnType("longtext")
                        .HasColumnName("neighborhood");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("postal_code");

                    b.Property<string>("RecipientFullName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("recipient_full_name");

                    b.Property<string>("RecipientPhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("recipient_phone_number");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("state");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("street_name");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_address");

                    b.ToTable("address", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.ProductEntities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("active");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<Guid>("Guid")
                        .HasColumnType("char(36)")
                        .HasColumnName("guid");

                    b.Property<float>("Height")
                        .HasColumnType("float")
                        .HasColumnName("height");

                    b.Property<float>("Length")
                        .HasColumnType("float")
                        .HasColumnName("length");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("price");

                    b.Property<int>("ProductCategoryId")
                        .HasColumnType("int")
                        .HasColumnName("product_category_id");

                    b.Property<string>("Sku")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("sku");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.Property<bool>("Visible")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("visible");

                    b.Property<float>("Weight")
                        .HasColumnType("float")
                        .HasColumnName("weight");

                    b.Property<float>("Width")
                        .HasColumnType("float")
                        .HasColumnName("width");

                    b.HasKey("Id")
                        .HasName("pk_product");

                    b.HasIndex("ProductCategoryId")
                        .HasDatabaseName("ix_product_product_category_id");

                    b.ToTable("product", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.ProductEntities.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<Guid>("Guid")
                        .HasColumnType("char(36)")
                        .HasColumnName("guid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_product_category");

                    b.ToTable("product_category", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.ProductEntities.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("image_path");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("product_id");

                    b.HasKey("Id")
                        .HasName("pk_product_image");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_product_image_product_id");

                    b.ToTable("product_image", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.ProductEntities.ProductInventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("product_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_product_inventory");

                    b.HasIndex("ProductId")
                        .IsUnique()
                        .HasDatabaseName("ix_product_inventory_product_id");

                    b.ToTable("product_inventory", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.ProductEntities.Product", b =>
                {
                    b.HasOne("Ecommerce.Domain.Entities.ProductEntities.ProductCategory", "ProductCategory")
                        .WithMany("Products")
                        .HasForeignKey("ProductCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_product_product_categories_product_category_id");

                    b.Navigation("ProductCategory");
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.ProductEntities.ProductImage", b =>
                {
                    b.HasOne("Ecommerce.Domain.Entities.ProductEntities.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_product_image_product_product_id");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.ProductEntities.ProductInventory", b =>
                {
                    b.HasOne("Ecommerce.Domain.Entities.ProductEntities.Product", "Product")
                        .WithOne("ProductInventory")
                        .HasForeignKey("Ecommerce.Domain.Entities.ProductEntities.ProductInventory", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_product_inventory_product_product_id");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.ProductEntities.Product", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("ProductInventory");
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.ProductEntities.ProductCategory", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
