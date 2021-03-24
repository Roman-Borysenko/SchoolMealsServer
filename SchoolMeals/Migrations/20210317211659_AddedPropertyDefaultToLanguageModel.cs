using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolMeals.Migrations
{
    public partial class AddedPropertyDefaultToLanguageModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Default",
                table: "Languages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Default",
                table: "Languages");
        }
    }
}
