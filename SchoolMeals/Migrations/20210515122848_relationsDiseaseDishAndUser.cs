using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolMeals.Migrations
{
    public partial class relationsDiseaseDishAndUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiseaseDishes_Dishes_DishId",
                table: "DiseaseDishes");

            migrationBuilder.DropForeignKey(
                name: "FK_DiseaseUsers_Diseases_DiseaseId",
                table: "DiseaseUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_DiseaseDishes_Dishes_DishId",
                table: "DiseaseDishes",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiseaseUsers_Diseases_DiseaseId",
                table: "DiseaseUsers",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiseaseDishes_Dishes_DishId",
                table: "DiseaseDishes");

            migrationBuilder.DropForeignKey(
                name: "FK_DiseaseUsers_Diseases_DiseaseId",
                table: "DiseaseUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_DiseaseDishes_Dishes_DishId",
                table: "DiseaseDishes",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiseaseUsers_Diseases_DiseaseId",
                table: "DiseaseUsers",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
