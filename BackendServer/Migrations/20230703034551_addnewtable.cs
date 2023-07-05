using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendServer.Migrations
{
    public partial class addnewtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "browserInformation",
                columns: table => new
                {
                    browserInformationId = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    position = table.Column<string>(type: "text", nullable: false),
                    area = table.Column<string>(type: "text", nullable: false),
                    branch = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_browserInformation", x => x.browserInformationId);
                });

            migrationBuilder.CreateTable(
                name: "insuranceContractBrowses",
                columns: table => new
                {
                    HDBH = table.Column<string>(type: "text", nullable: false),
                    browserInformationId = table.Column<int>(type: "integer", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false),
                    message = table.Column<string>(type: "text", nullable: false),
                    timeSpace = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    browserInformationId1 = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insuranceContractBrowses", x => new { x.browserInformationId, x.HDBH });
                    table.ForeignKey(
                        name: "FK_insuranceContractBrowses_browserInformation_browserInformat~",
                        column: x => x.browserInformationId1,
                        principalTable: "browserInformation",
                        principalColumn: "browserInformationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_insuranceContractBrowses_InsuranceContracts_HDBH",
                        column: x => x.HDBH,
                        principalTable: "InsuranceContracts",
                        principalColumn: "HDBH",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_insuranceContractBrowses_browserInformationId1",
                table: "insuranceContractBrowses",
                column: "browserInformationId1");

            migrationBuilder.CreateIndex(
                name: "IX_insuranceContractBrowses_HDBH",
                table: "insuranceContractBrowses",
                column: "HDBH");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "insuranceContractBrowses");

            migrationBuilder.DropTable(
                name: "browserInformation");
        }
    }
}
