using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace historianproductionservice.Migrations
{
    public partial class UpdateModelOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tools_ToolTypes_Orderid",
                table: "Tools");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_ToolTypes_Orderid1",
                table: "Tools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToolTypes",
                table: "ToolTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tools",
                table: "Tools");

            migrationBuilder.RenameTable(
                name: "ToolTypes",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "Tools",
                newName: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_Tools_Orderid1",
                table: "Products",
                newName: "IX_Products_Orderid1");

            migrationBuilder.RenameIndex(
                name: "IX_Tools_Orderid",
                table: "Products",
                newName: "IX_Products_Orderid");

            migrationBuilder.AddColumn<int>(
                name: "productionOrderId",
                table: "Orders",
                type: "int4",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_Orderid",
                table: "Products",
                column: "Orderid",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_Orderid1",
                table: "Products",
                column: "Orderid1",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_Orderid",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_Orderid1",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "productionOrderId",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Tools");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "ToolTypes");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Orderid1",
                table: "Tools",
                newName: "IX_Tools_Orderid1");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Orderid",
                table: "Tools",
                newName: "IX_Tools_Orderid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tools",
                table: "Tools",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToolTypes",
                table: "ToolTypes",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_ToolTypes_Orderid",
                table: "Tools",
                column: "Orderid",
                principalTable: "ToolTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_ToolTypes_Orderid1",
                table: "Tools",
                column: "Orderid1",
                principalTable: "ToolTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
