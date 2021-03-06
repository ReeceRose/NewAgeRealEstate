﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace NARE.Persistence.Migrations
{
    public partial class AlteredListingStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Listings",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Listings",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
