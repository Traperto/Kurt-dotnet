using Microsoft.EntityFrameworkCore.Migrations;

namespace ColaTerminal.Migrations
{
    public partial class deleteTokenColumnInUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("token", "user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "token",
                table: "user",
                nullable: true);
        }
    }
}
