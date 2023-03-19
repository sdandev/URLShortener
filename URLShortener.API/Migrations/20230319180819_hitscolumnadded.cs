using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URLShortener.API.Migrations
{
    public partial class hitscolumnadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Hits",
                table: "Urls",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hits",
                table: "Urls");
        }
    }
}
