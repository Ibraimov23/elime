using Microsoft.EntityFrameworkCore.Migrations;

namespace ElimeProject.Migrations
{
    public partial class AddingDrinkCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinkes_DrinkCategory_DrinkCategoryId",
                table: "Drinkes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrinkCategory",
                table: "DrinkCategory");

            migrationBuilder.RenameTable(
                name: "DrinkCategory",
                newName: "DrinkCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrinkCategories",
                table: "DrinkCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drinkes_DrinkCategories_DrinkCategoryId",
                table: "Drinkes",
                column: "DrinkCategoryId",
                principalTable: "DrinkCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinkes_DrinkCategories_DrinkCategoryId",
                table: "Drinkes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrinkCategories",
                table: "DrinkCategories");

            migrationBuilder.RenameTable(
                name: "DrinkCategories",
                newName: "DrinkCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrinkCategory",
                table: "DrinkCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drinkes_DrinkCategory_DrinkCategoryId",
                table: "Drinkes",
                column: "DrinkCategoryId",
                principalTable: "DrinkCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
