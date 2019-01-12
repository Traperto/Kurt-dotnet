using Microsoft.EntityFrameworkCore.Migrations;

namespace ColaTerminal.Migrations
{
    public partial class RfidAsString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "rfId",
                table: "user",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "rfId",
                table: "user",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);
        }
    }
}
