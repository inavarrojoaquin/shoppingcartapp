﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoppingCartApp.Modules.ShoppingCartModule.Infrastructure;

#nullable disable

namespace ShoppingCartApp.Migrations
{
    [DbContext(typeof(ShoppingCartDbContext))]
    [Migration("20230529170051_InitShoppingCartDb")]
    partial class InitShoppingCartDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShoppingCartApp.App.Domain.OrderItemData", b =>
                {
                    b.Property<string>("OrderItemId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ProductPrice")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("ShoppingCartId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("OrderItemId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("OrderItems");
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

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = "5CBF54BA-BF19-40BF-B97D-4827A11720A2",
                            ProductName = "Product one",
                            ProductPrice = 30.0
                        },
                        new
                        {
                            ProductId = "7478b9ae-2e05-4c6d-afb1-3b8934edc699",
                            ProductName = "Product two",
                            ProductPrice = 40.0
                        });
                });

            modelBuilder.Entity("ShoppingCartApp.App.Domain.ShoppingCartData", b =>
                {
                    b.Property<string>("ShoppingCartId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ShoppingCartName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ShoppingCartId");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("ShoppingCartApp.App.Domain.OrderItemData", b =>
                {
                    b.HasOne("ShoppingCartApp.App.Domain.ShoppingCartData", "ShoppingCartData")
                        .WithMany("OrderItems")
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShoppingCartData");
                });

            modelBuilder.Entity("ShoppingCartApp.App.Domain.ShoppingCartData", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
