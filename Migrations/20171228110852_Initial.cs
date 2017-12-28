using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace historianproductionservice.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToolTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    order = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tools",
                columns: table => new
                {
                    id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Orderid = table.Column<int>(type: "int4", nullable: true),
                    Orderid1 = table.Column<int>(type: "int4", nullable: true),
                    batch = table.Column<string>(type: "text", nullable: true),
                    date = table.Column<long>(type: "int8", nullable: false),
                    product = table.Column<string>(type: "text", nullable: true),
                    quantity = table.Column<double>(type: "float8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tools", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tools_ToolTypes_Orderid",
                        column: x => x.Orderid,
                        principalTable: "ToolTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tools_ToolTypes_Orderid1",
                        column: x => x.Orderid1,
                        principalTable: "ToolTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tools_Orderid",
                table: "Tools",
                column: "Orderid");

            migrationBuilder.CreateIndex(
                name: "IX_Tools_Orderid1",
                table: "Tools",
                column: "Orderid1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tools");

            migrationBuilder.DropTable(
                name: "ToolTypes");
        }
    }
}
