using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ColaTerminal.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "drink",
                columns: table => new
                {
                    id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(20)", nullable: true),
                    quantity = table.Column<int>(type: "int(2)", nullable: true),
                    price = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drink", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userName = table.Column<string>(type: "varchar(50)", nullable: true),
                    firstName = table.Column<string>(type: "varchar(50)", nullable: true),
                    lastName = table.Column<string>(type: "varchar(50)", nullable: true),
                    balance = table.Column<double>(nullable: true),
                    rfId = table.Column<int>(type: "int(11)", nullable: true),
                    password = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "balanceTransaction",
                columns: table => new
                {
                    id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<uint>(nullable: true),
                    amount = table.Column<double>(nullable: true),
                    date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_balanceTransaction", x => x.id);
                    table.ForeignKey(
                        name: "balanceTransaction_user",
                        column: x => x.userId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "proceed",
                columns: table => new
                {
                    id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<uint>(nullable: true),
                    drinkId = table.Column<uint>(nullable: true),
                    date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proceed", x => x.id);
                    table.ForeignKey(
                        name: "proceed_drink",
                        column: x => x.drinkId,
                        principalTable: "drink",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "proceed_user",
                        column: x => x.userId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "refill",
                columns: table => new
                {
                    id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    date = table.Column<DateTime>(type: "datetime", nullable: true),
                    userId = table.Column<uint>(nullable: true),
                    price = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refill", x => x.id);
                    table.ForeignKey(
                        name: "refill_user",
                        column: x => x.userId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "refillContainment",
                columns: table => new
                {
                    refillId = table.Column<uint>(nullable: false),
                    drinkId = table.Column<uint>(nullable: false),
                    quantity = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.drinkId, x.refillId });
                    table.ForeignKey(
                        name: "containment_drink",
                        column: x => x.drinkId,
                        principalTable: "drink",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "conainment_refill",
                        column: x => x.refillId,
                        principalTable: "refill",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "balanceTransaction_user",
                table: "balanceTransaction",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "proceed_drink",
                table: "proceed",
                column: "drinkId");

            migrationBuilder.CreateIndex(
                name: "proceed_user",
                table: "proceed",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "refill_user",
                table: "refill",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "conainment_refill",
                table: "refillContainment",
                column: "refillId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "balanceTransaction");

            migrationBuilder.DropTable(
                name: "proceed");

            migrationBuilder.DropTable(
                name: "refillContainment");

            migrationBuilder.DropTable(
                name: "drink");

            migrationBuilder.DropTable(
                name: "refill");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
