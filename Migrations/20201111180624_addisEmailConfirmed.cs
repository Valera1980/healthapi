using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class addisEmailConfirmed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isEmailConfirmed",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isEmailConfirmed",
                table: "Users");
        }
    }
}
