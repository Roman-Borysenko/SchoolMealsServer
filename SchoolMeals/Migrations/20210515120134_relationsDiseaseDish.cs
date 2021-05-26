using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolMeals.Migrations
{
    public partial class relationsDiseaseDish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiseaseDishes_Diseases_DiseaseId",
                table: "DiseaseDishes");

            migrationBuilder.DropForeignKey(
                name: "FK_DiseaseDishes_Dishes_DishId",
                table: "DiseaseDishes");

            migrationBuilder.AddForeignKey(
                name: "FK_DiseaseDishes_Diseases_DiseaseId",
                table: "DiseaseDishes",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiseaseDishes_Dishes_DishId",
                table: "DiseaseDishes",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiseaseDishes_Diseases_DiseaseId",
                table: "DiseaseDishes");

            migrationBuilder.DropForeignKey(
                name: "FK_DiseaseDishes_Dishes_DishId",
                table: "DiseaseDishes");

            migrationBuilder.AddForeignKey(
                name: "FK_DiseaseDishes_Diseases_DiseaseId",
                table: "DiseaseDishes",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiseaseDishes_Dishes_DishId",
                table: "DiseaseDishes",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
