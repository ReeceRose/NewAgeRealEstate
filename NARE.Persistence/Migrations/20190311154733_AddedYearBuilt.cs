using Microsoft.EntityFrameworkCore.Migrations;

namespace NARE.Persistence.Migrations
{
    public partial class AddedYearBuilt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YearBuilt",
                table: "Listings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearBuilt",
                table: "Listings");
        }
    }
}
