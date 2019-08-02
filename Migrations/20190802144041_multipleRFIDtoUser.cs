using Microsoft.EntityFrameworkCore.Migrations;

namespace ColaTerminal.Migrations
{
    public partial class multipleRFIDtoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("rfid", "user");
            
            migrationBuilder.CreateTable(
                name: "rfid",
                columns: table => new
                {
                    userId = table.Column<uint>(nullable: true),
                    rfId = table.Column<int>(type: "int(11)", nullable: true),
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "rfid_user",
                        column: x => x.userId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
