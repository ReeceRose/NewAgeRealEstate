﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace NARE.Persistence.Migrations
{
    public partial class AddedAccountEnabled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "AccountEnabled",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: (short)1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountEnabled",
                table: "AspNetUsers");
        }
    }
}
