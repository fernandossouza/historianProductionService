using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace historianproductionservice.Migrations
{
    public partial class ChangedProductModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.CreateTable(
                name: "ProductTraceabilities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Orderid = table.Column<int>(type: "int4", nullable: true),
                    Orderid1 = table.Column<int>(type: "int4", nullable: true),
                    batch = table.Column<string>(type: "text", nullable: true),
                    date = table.Column<long>(type: "int8", nullable: false),
                    product = table.Column<string>(type: "text", nullable: true),
                    productId = table.Column<int>(type: "int4", nullable: false),
                    quantity = table.Column<double>(type: "float8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTraceabilities", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProductTraceabilities_Orders_Orderid",
                        column: x => x.Orderid,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTraceabilities_Orders_Orderid1",
                        column: x => x.Orderid1,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductTraceabilities_Orderid",
                table: "ProductTraceabilities",
                column: "Orderid");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTraceabilities_Orderid1",
                table: "ProductTraceabilities",
                column: "Orderid1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductTraceabilities");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Orderid = table.Column<int>(nullable: true),
                    Orderid1 = table.Column<int>(nullable: true),
                    batch = table.Column<string>(nullable: true),
                    date = table.Column<long>(nullable: false),
                    product = table.Column<string>(nullable: true),
                    productId = table.Column<int>(nullable: false),
                    quantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id);
                    table.ForeignKey(
                        name: "FK_Products_Orders_Orderid",
                        column: x => x.Orderid,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Orders_Orderid1",
                        column: x => x.Orderid1,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Orderid",
                table: "Products",
                column: "Orderid");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Orderid1",
                table: "Products",
                column: "Orderid1");
        }
    }
}
