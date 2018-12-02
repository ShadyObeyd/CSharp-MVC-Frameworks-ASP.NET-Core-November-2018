using Microsoft.EntityFrameworkCore.Migrations;

namespace Eventures.App.Data.Migrations
{
    public partial class Removed_MyProperty_from_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }
    }
}
