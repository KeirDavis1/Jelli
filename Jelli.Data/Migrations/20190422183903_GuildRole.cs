using Microsoft.EntityFrameworkCore.Migrations;

namespace Jelli.Data.Migrations
{
    public partial class GuildRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuildRoles",
                columns: table => new
                {
                    GuildId = table.Column<ulong>(nullable: false),
                    RoleId = table.Column<ulong>(nullable: false),
                    RoleDisplayName = table.Column<string>(nullable: true),
                    GuildId1 = table.Column<ulong>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildRoles", x => x.GuildId);
                    table.ForeignKey(
                        name: "FK_GuildRoles_Guilds_GuildId1",
                        column: x => x.GuildId1,
                        principalTable: "Guilds",
                        principalColumn: "GuildId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuildRoles_GuildId1",
                table: "GuildRoles",
                column: "GuildId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuildRoles");
        }
    }
}
