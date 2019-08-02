using Microsoft.EntityFrameworkCore.Migrations;

namespace ColaTerminal.Migrations
{
    public partial class lowerCaseColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("Price", "refill");
            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "refill",
                nullable: false,
                defaultValue: 0.0);
            
            migrationBuilder.DropColumn("Price", "proceed");
            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "proceed",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.DropColumn("Token", "user");
            migrationBuilder.AddColumn<string>(
                name: "token",
                table: "user",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "token",
                table: "user");
            
            migrationBuilder.DropColumn(
                name: "price",
                table: "proceed");
            
            migrationBuilder.DropColumn(
                name: "price",
                table: "refill");
        }
    }
}
