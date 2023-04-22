using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPetProject.Migrations
{
    /// <inheritdoc />
    public partial class NewOrderSystem2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductCount",
                table: "Order_Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCount",
                table: "Order_Products");
        }
    }
}
