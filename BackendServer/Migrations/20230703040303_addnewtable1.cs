using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendServer.Migrations
{
    public partial class addnewtable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "annexContractBrowses",
                columns: table => new
                {
                    HDPL = table.Column<string>(type: "text", nullable: false),
                    browserInformationId = table.Column<int>(type: "integer", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false),
                    message = table.Column<string>(type: "text", nullable: false),
                    timeSpace = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    browserInformationId1 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_annexContractBrowses", x => new { x.browserInformationId, x.HDPL });
                    table.ForeignKey(
                        name: "FK_annexContractBrowses_AnnexContracts_HDPL",
                        column: x => x.HDPL,
                        principalTable: "AnnexContracts",
                        principalColumn: "HDPL",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_annexContractBrowses_browserInformation_browserInformationI~",
                        column: x => x.browserInformationId1,
                        principalTable: "browserInformation",
                        principalColumn: "browserInformationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_annexContractBrowses_browserInformationId1",
                table: "annexContractBrowses",
                column: "browserInformationId1");

            migrationBuilder.CreateIndex(
                name: "IX_annexContractBrowses_HDPL",
                table: "annexContractBrowses",
                column: "HDPL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "annexContractBrowses");
        }
    }
}
