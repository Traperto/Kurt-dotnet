using Microsoft.EntityFrameworkCore.Migrations;

namespace ColaTerminal.Migrations
{
    public partial class jwttoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "user",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "user");
        }
    }
}
