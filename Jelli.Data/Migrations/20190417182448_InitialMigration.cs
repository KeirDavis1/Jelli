using Microsoft.EntityFrameworkCore.Migrations;

namespace Jelli.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guilds",
                columns: table => new
                {
                    GuildId = table.Column<ulong>(nullable: false),
                    CommandPrefix = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guilds", x => x.GuildId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guilds");
        }
    }
}
