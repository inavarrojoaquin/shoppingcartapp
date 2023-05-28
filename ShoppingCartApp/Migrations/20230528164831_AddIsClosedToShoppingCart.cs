﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCartApp.Migrations
{
    /// <inheritdoc />
    public partial class AddIsClosedToShoppingCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "ShoppingCarts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "ShoppingCarts");
        }
    }
}