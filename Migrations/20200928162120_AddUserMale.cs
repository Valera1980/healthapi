using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class AddUserMale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Male",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Male",
                table: "Users");
        }
    }
}
