using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCartApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>("Stock", "Products", defaultValue: 20);
            /*
            migrationBuilder.Sql(
                @"UPDATE Products SET Stock = 20 ");
        */
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
