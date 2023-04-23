﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoppingCartApp.App.Infrastructure;

#nullable disable

namespace ShoppingCartApp.Migrations
{
    [DbContext(typeof(ShoppingCartContext))]
    [Migration("20230423100908_Init DB")]
    partial class InitDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShoppingCartApp.App.Domain.DiscountData", b =>
                {
                    b.Property<string>("DiscountId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DiscountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DiscountQuantity")
                        .HasColumnType("int");

                    b.HasKey("DiscountId");

                    b.ToTable("Discount");
                });

            modelBuilder.Entity("ShoppingCartApp.App.Domain.OrderItemData", b =>
                {
                    b.Property<string>("OrderItemId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("ShoppingCartDataShoppingCartId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("OrderItemId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ShoppingCartDataShoppingCartId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("ShoppingCartApp.App.Domain.ProductData", b =>
                {
                    b.Property<string>("ProductId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ProductPrice")
                        .HasColumnType("float");

                    b.HasKey("ProductId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("ShoppingCartApp.App.Domain.ShoppingCartData", b =>
                {
                    b.Property<string>("ShoppingCartId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ShoppingCartName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ShoppingCartId");

                    b.ToTable("ShoppingCart");
                });

            modelBuilder.Entity("ShoppingCartApp.App.Domain.OrderItemData", b =>
                {
                    b.HasOne("ShoppingCartApp.App.Domain.ProductData", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoppingCartApp.App.Domain.ShoppingCartData", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("ShoppingCartDataShoppingCartId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ShoppingCartApp.App.Domain.ShoppingCartData", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
