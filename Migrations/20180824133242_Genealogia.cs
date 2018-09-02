using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace historianproductionservice.Migrations
{
    public partial class Genealogia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genealogy",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    endDate = table.Column<long>(nullable: false),
                    orderId = table.Column<long>(nullable: false),
                    productionOrderNumber = table.Column<string>(nullable: true),
                    recipeCode = table.Column<string>(nullable: true),
                    recipeid = table.Column<string>(nullable: true),
                    startDate = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genealogy", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    order = table.Column<string>(nullable: true),
                    productionOrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "EndRoll",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Genealogyid = table.Column<long>(nullable: true),
                    endDate = table.Column<long>(nullable: false),
                    productionOrderId = table.Column<long>(nullable: false),
                    quantity = table.Column<string>(nullable: true),
                    startDate = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndRoll", x => x.id);
                    table.ForeignKey(
                        name: "FK_EndRoll_Genealogy_Genealogyid",
                        column: x => x.Genealogyid,
                        principalTable: "Genealogy",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductTraceabilities",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Orderid = table.Column<int>(nullable: true),
                    Orderid1 = table.Column<int>(nullable: true),
                    batch = table.Column<string>(nullable: true),
                    code = table.Column<string>(nullable: true),
                    date = table.Column<long>(nullable: false),
                    product = table.Column<string>(nullable: true),
                    productId = table.Column<int>(nullable: false),
                    productType = table.Column<string>(nullable: true),
                    quantity = table.Column<double>(nullable: false),
                    unity = table.Column<string>(nullable: true),
                    username = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Aco",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EndRollid = table.Column<long>(nullable: true),
                    batch = table.Column<string>(nullable: true),
                    code = table.Column<string>(nullable: true),
                    endDate = table.Column<long>(nullable: false),
                    quantity = table.Column<string>(nullable: true),
                    startDate = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aco", x => x.id);
                    table.ForeignKey(
                        name: "FK_Aco_EndRoll_EndRollid",
                        column: x => x.EndRollid,
                        principalTable: "EndRoll",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Liga",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EndRollid = table.Column<long>(nullable: true),
                    batch = table.Column<string>(nullable: true),
                    code = table.Column<string>(nullable: true),
                    endDate = table.Column<long>(nullable: false),
                    orderId = table.Column<long>(nullable: false),
                    orderNumber = table.Column<string>(nullable: true),
                    quantity = table.Column<string>(nullable: true),
                    startDate = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Liga", x => x.id);
                    table.ForeignKey(
                        name: "FK_Liga_EndRoll_EndRollid",
                        column: x => x.EndRollid,
                        principalTable: "EndRoll",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tool",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EndRollid = table.Column<long>(nullable: true),
                    group = table.Column<string>(nullable: true),
                    serialNumber = table.Column<string>(nullable: true),
                    toolId = table.Column<string>(nullable: true),
                    typeName = table.Column<string>(nullable: true),
                    vidaUtil = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tool", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tool_EndRoll_EndRollid",
                        column: x => x.EndRollid,
                        principalTable: "EndRoll",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Elemento",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Ligaid = table.Column<long>(nullable: true),
                    batch = table.Column<string>(nullable: true),
                    date = table.Column<long>(nullable: false),
                    product = table.Column<string>(nullable: true),
                    quantity = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elemento", x => x.id);
                    table.ForeignKey(
                        name: "FK_Elemento_Liga_Ligaid",
                        column: x => x.Ligaid,
                        principalTable: "Liga",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aco_EndRollid",
                table: "Aco",
                column: "EndRollid");

            migrationBuilder.CreateIndex(
                name: "IX_Elemento_Ligaid",
                table: "Elemento",
                column: "Ligaid");

            migrationBuilder.CreateIndex(
                name: "IX_EndRoll_Genealogyid",
                table: "EndRoll",
                column: "Genealogyid");

            migrationBuilder.CreateIndex(
                name: "IX_Liga_EndRollid",
                table: "Liga",
                column: "EndRollid");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTraceabilities_Orderid",
                table: "ProductTraceabilities",
                column: "Orderid");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTraceabilities_Orderid1",
                table: "ProductTraceabilities",
                column: "Orderid1");

            migrationBuilder.CreateIndex(
                name: "IX_Tool_EndRollid",
                table: "Tool",
                column: "EndRollid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aco");

            migrationBuilder.DropTable(
                name: "Elemento");

            migrationBuilder.DropTable(
                name: "ProductTraceabilities");

            migrationBuilder.DropTable(
                name: "Tool");

            migrationBuilder.DropTable(
                name: "Liga");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "EndRoll");

            migrationBuilder.DropTable(
                name: "Genealogy");
        }
    }
}
