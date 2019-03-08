using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NARE.Persistence.Migrations
{
    public partial class AddedListing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
migrationBuilder.AlterColumn<short>(
                name: "AccountEnabled",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.CreateTable(
                name: "Listings",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    ProvinceCode = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    AskingPrice = table.Column<double>(nullable: false),
                    BedroomCount = table.Column<int>(nullable: false),
                    BathroomCount = table.Column<double>(nullable: false),
                    GarageSize = table.Column<double>(nullable: false),
                    SquareFeet = table.Column<int>(nullable: false),
                    LotSize = table.Column<double>(nullable: false),
                    ListingDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    AgentId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Listings_AspNetUsers_AgentId",
                        column: x => x.AgentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    Alternative = table.Column<string>(nullable: true),
                    ListingId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_ListingId",
                table: "Image",
                column: "ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_AgentId",
                table: "Listings",
                column: "AgentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Listings");

migrationBuilder.AlterColumn<short>(
                name: "AccountEnabled",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(short));
        }
    }
}
