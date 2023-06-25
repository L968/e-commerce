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
    [Migration("20230624183226_AddingProductDiscount")]
    partial class AddingProductDiscount
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

            modelBuilder.Entity("Ecommerce.Domain.Entities.CartEntities.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_cart");

                    b.ToTable("cart", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.CartEntities.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("CartId")
                        .HasColumnType("int")
                        .HasColumnName("cart_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<int>("ProductVariantId")
                        .HasColumnType("int")
                        .HasColumnName("product_variant_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_cart_item");

                    b.HasIndex("CartId")
                        .HasDatabaseName("ix_cart_item_cart_id");

                    b.HasIndex("ProductVariantId")
                        .HasDatabaseName("ix_cart_item_product_variant_id");

                    b.ToTable("cart_item", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.ProductEntities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("active");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("description");

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
                        .HasPrecision(65, 2)
                        .HasColumnType("decimal(65,2)")
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

            modelBuilder.Entity("Ecommerce.Domain.Entities.ProductEntities.ProductDiscount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("DiscountUnit")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("discount_unit");

                    b.Property<decimal>("DiscountValue")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("discount_value");

                    b.Property<decimal?>("MaximumDiscountAmount")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("maximum_discount_amount");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("char(36)")
                        .HasColumnName("product_id");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.Property<DateTime>("ValidFrom")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("valid_from");

                    b.Property<DateTime?>("ValidUntil")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("valid_until");

                    b.HasKey("Id")
                        .HasName("pk_product_discount");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_product_discount_product_id");

                    b.ToTable("product_discount", (string)null);
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

                    b.Property<Guid>("ProductId")
                        .HasColumnType("char(36)")
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

                    b.Property<Guid>("ProductId")
                        .HasColumnType("char(36)")
                        .HasColumnName("product_id");

                    b.Property<int>("Stock")
                        .HasColumnType("int")
                        .HasColumnName("stock");

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

            modelBuilder.Entity("Ecommerce.Domain.Entities.ProductEntities.ProductVariant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int?>("ParentProductId")
                        .HasColumnType("int")
                        .HasColumnName("parent_product_id");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("product_id");

                    b.Property<Guid?>("ProductId1")
                        .HasColumnType("char(36)")
                        .HasColumnName("product_id1");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("type");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_product_variant");

                    b.HasIndex("ProductId1")
                        .HasDatabaseName("ix_product_variant_product_id1");

                    b.ToTable("product_variant", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.CartEntities.CartItem", b =>
                {
                    b.HasOne("Ecommerce.Domain.Entities.CartEntities.Cart", null)
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_cart_item_cart_cart_id");

                    b.HasOne("Ecommerce.Domain.Entities.ProductEntities.ProductVariant", "ProductVariant")
                        .WithMany()
                        .HasForeignKey("ProductVariantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_cart_item_product_variant_product_variant_id");

                    b.Navigation("ProductVariant");
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.ProductEntities.Product", b =>
                {
                    b.HasOne("Ecommerce.Domain.Entities.ProductEntities.ProductCategory", "Category")
                        .WithMany()
                        .HasForeignKey("ProductCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_product_product_categories_product_category_id");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.ProductEntities.ProductDiscount", b =>
                {
                    b.HasOne("Ecommerce.Domain.Entities.ProductEntities.Product", "Product")
                        .WithMany("Discounts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_product_discount_product_product_id");

                    b.Navigation("Product");
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
                        .WithOne("Inventory")
                        .HasForeignKey("Ecommerce.Domain.Entities.ProductEntities.ProductInventory", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_product_inventory_product_product_id");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.ProductEntities.ProductVariant", b =>
                {
                    b.HasOne("Ecommerce.Domain.Entities.ProductEntities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId1")
                        .HasConstraintName("fk_product_variant_products_product_id1");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.CartEntities.Cart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.ProductEntities.Product", b =>
                {
                    b.Navigation("Discounts");

                    b.Navigation("Images");

                    b.Navigation("Inventory")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
