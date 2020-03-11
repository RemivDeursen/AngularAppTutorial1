using Microsoft.EntityFrameworkCore.Migrations;

namespace HeroesAPI.Migrations
{
    public partial class secret : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Secret",
                table: "Heroes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Secret",
                table: "Heroes");
        }
    }
}
