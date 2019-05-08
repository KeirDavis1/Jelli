using Microsoft.EntityFrameworkCore.Migrations;

namespace Jelli.Data.Migrations
{
    public partial class AliasCommand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AliasCommands",
                columns: table => new
                {
                    Id = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Command = table.Column<string>(nullable: true),
                    AliasTo = table.Column<string>(nullable: true),
                    GuildId = table.Column<ulong>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AliasCommands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AliasCommands_Guilds_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guilds",
                        principalColumn: "GuildId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AliasCommands_GuildId",
                table: "AliasCommands",
                column: "GuildId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AliasCommands");
        }
    }
}
