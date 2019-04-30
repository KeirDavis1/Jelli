using Microsoft.EntityFrameworkCore.Migrations;

namespace Jelli.Data.Migrations
{
    public partial class ChannelEnforcements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChannelEnforcements",
                columns: table => new
                {
                    Id = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChannelId = table.Column<ulong>(nullable: false),
                    RestrictPictures = table.Column<bool>(nullable: true),
                    RestrictText = table.Column<bool>(nullable: true),
                    MinimumGuildJoinedAgeDays = table.Column<int>(nullable: true),
                    MinimumDiscordAgeDays = table.Column<int>(nullable: true),
                    MinimumCharacters = table.Column<int>(nullable: true),
                    GuildId = table.Column<ulong>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelEnforcements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChannelEnforcements_Guilds_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guilds",
                        principalColumn: "GuildId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChannelEnforcements_GuildId",
                table: "ChannelEnforcements",
                column: "GuildId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelEnforcements");
        }
    }
}
