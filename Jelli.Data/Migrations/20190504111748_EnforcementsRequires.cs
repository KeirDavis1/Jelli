using Microsoft.EntityFrameworkCore.Migrations;

namespace Jelli.Data.Migrations
{
    public partial class EnforcementsRequires : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RequirePictures",
                table: "ChannelEnforcements",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RequireText",
                table: "ChannelEnforcements",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequirePictures",
                table: "ChannelEnforcements");

            migrationBuilder.DropColumn(
                name: "RequireText",
                table: "ChannelEnforcements");
        }
    }
}
