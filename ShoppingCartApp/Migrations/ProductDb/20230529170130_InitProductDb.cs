using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCartApp.Migrations.ProductDb
{
    /// <inheritdoc />
    public partial class InitProductDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>("Stock", "Products", defaultValue: 20);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("Stock", "Products");
        }
    }
}
