using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolMeals.Migrations
{
    public partial class AddLanguageForArticleAndSlide : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Slider",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Blog",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Slider_LanguageId",
                table: "Slider",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_LanguageId",
                table: "Blog",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_Languages_LanguageId",
                table: "Blog",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Slider_Languages_LanguageId",
                table: "Slider",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_Languages_LanguageId",
                table: "Blog");

            migrationBuilder.DropForeignKey(
                name: "FK_Slider_Languages_LanguageId",
                table: "Slider");

            migrationBuilder.DropIndex(
                name: "IX_Slider_LanguageId",
                table: "Slider");

            migrationBuilder.DropIndex(
                name: "IX_Blog_LanguageId",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Slider");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Blog");
        }
    }
}
