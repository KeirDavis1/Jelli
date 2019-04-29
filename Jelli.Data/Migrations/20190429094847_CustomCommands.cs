using Microsoft.EntityFrameworkCore.Migrations;

namespace Jelli.Data.Migrations
{
    public partial class CustomCommands : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuildCustomCommands",
                columns: table => new
                {
                    Id = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Command = table.Column<string>(nullable: true),
                    Response = table.Column<string>(nullable: true),
                    GuildId = table.Column<ulong>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildCustomCommands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuildCustomCommands_Guilds_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guilds",
                        principalColumn: "GuildId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuildCustomCommands_GuildId",
                table: "GuildCustomCommands",
                column: "GuildId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuildCustomCommands");
        }
    }
}
