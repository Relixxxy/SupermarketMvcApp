using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupermarketApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductDepartmentDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Department_DepartmentId",
                table: "Product");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Department_DepartmentId",
                table: "Product",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Department_DepartmentId",
                table: "Product");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Department_DepartmentId",
                table: "Product",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
