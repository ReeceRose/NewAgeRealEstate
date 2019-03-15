using Microsoft.EntityFrameworkCore.Migrations;

namespace NARE.Persistence.Migrations
{
    public partial class AddedListingFeatured : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Featured",
                table: "Listings",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Featured",
                table: "Listings");
        }
    }
}
