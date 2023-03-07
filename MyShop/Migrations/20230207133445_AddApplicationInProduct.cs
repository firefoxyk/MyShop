using Microsoft.EntityFrameworkCore.Migrations;

namespace MyShop.Migrations
{
    public partial class AddApplicationInProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ApplicationId",
                table: "Product",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Application_ApplicationId",
                table: "Product",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "ApplicationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Application_ApplicationId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ApplicationId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Product");
        }
    }
}
