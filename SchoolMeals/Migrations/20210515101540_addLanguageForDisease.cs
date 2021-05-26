using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolMeals.Migrations
{
    public partial class addLanguageForDisease : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Diseases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_LanguageId",
                table: "Diseases",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diseases_Languages_LanguageId",
                table: "Diseases",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diseases_Languages_LanguageId",
                table: "Diseases");

            migrationBuilder.DropIndex(
                name: "IX_Diseases_LanguageId",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Diseases");
        }
    }
}
