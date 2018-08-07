using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace historianproductionservice.Migrations
{
    public partial class CodeProductTrac : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "ProductTraceabilities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "code",
                table: "ProductTraceabilities");
        }
    }
}
