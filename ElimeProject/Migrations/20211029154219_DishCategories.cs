using Microsoft.EntityFrameworkCore.Migrations;

namespace ElimeProject.Migrations
{
    public partial class DishCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_DishCategory_DishCategoryId",
                table: "Dishes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DishCategory",
                table: "DishCategory");

            migrationBuilder.RenameTable(
                name: "DishCategory",
                newName: "DishCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DishCategories",
                table: "DishCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_DishCategories_DishCategoryId",
                table: "Dishes",
                column: "DishCategoryId",
                principalTable: "DishCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_DishCategories_DishCategoryId",
                table: "Dishes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DishCategories",
                table: "DishCategories");

            migrationBuilder.RenameTable(
                name: "DishCategories",
                newName: "DishCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DishCategory",
                table: "DishCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_DishCategory_DishCategoryId",
                table: "Dishes",
                column: "DishCategoryId",
                principalTable: "DishCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
