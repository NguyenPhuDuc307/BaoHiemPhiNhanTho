using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendServer.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branchs",
                columns: table => new
                {
                    BranchCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BranchName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branchs", x => x.BranchCode);
                });

            migrationBuilder.CreateTable(
                name: "Collaterals",
                columns: table => new
                {
                    Ref = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    StatusCollateral = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ValueCollateral = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    AddressCollateral = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Relationship = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PropertyType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaterals", x => x.Ref);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Cif = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CustomerType = table.Column<int>(type: "integer", nullable: false),
                    Gender = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CCCD = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Cif);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    PartnerCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.PartnerCode);
                });

            migrationBuilder.CreateTable(
                name: "InfoCBNVs",
                columns: table => new
                {
                    TVTTCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameTVTT = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    BranchCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoCBNVs", x => x.TVTTCode);
                    table.ForeignKey(
                        name: "FK_InfoCBNVs_Branchs_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "Branchs",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceContracts",
                columns: table => new
                {
                    HDBH = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NewOrRenewed = table.Column<string>(type: "text", nullable: false),
                    STBH = table.Column<float>(type: "real", nullable: false),
                    InsuranceFee = table.Column<float>(type: "real", nullable: false),
                    NumberOfPayments = table.Column<int>(type: "integer", nullable: false),
                    FromDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ToDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Exception = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Beneficiaries = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    InsuranceType = table.Column<string>(type: "text", nullable: true),
                    Cif = table.Column<string>(type: "character varying(50)", nullable: true),
                    TVTTCode = table.Column<string>(type: "character varying(50)", nullable: true),
                    PartnerCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CollateralRef = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceContracts", x => x.HDBH);
                    table.ForeignKey(
                        name: "FK_InsuranceContracts_Collaterals_CollateralRef",
                        column: x => x.CollateralRef,
                        principalTable: "Collaterals",
                        principalColumn: "Ref",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsuranceContracts_Customers_Cif",
                        column: x => x.Cif,
                        principalTable: "Customers",
                        principalColumn: "Cif");
                    table.ForeignKey(
                        name: "FK_InsuranceContracts_InfoCBNVs_TVTTCode",
                        column: x => x.TVTTCode,
                        principalTable: "InfoCBNVs",
                        principalColumn: "TVTTCode");
                    table.ForeignKey(
                        name: "FK_InsuranceContracts_Partners_PartnerCode",
                        column: x => x.PartnerCode,
                        principalTable: "Partners",
                        principalColumn: "PartnerCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnnexContracts",
                columns: table => new
                {
                    HDPL = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NewOrRenewed = table.Column<bool>(type: "boolean", nullable: false),
                    STBH = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    InsuranceFee = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    NumberOfPayments = table.Column<int>(type: "integer", nullable: true),
                    FromDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ToDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Exception = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    HDBH = table.Column<string>(type: "character varying(50)", nullable: true),
                    TVTTCode = table.Column<string>(type: "character varying(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnexContracts", x => x.HDPL);
                    table.ForeignKey(
                        name: "FK_AnnexContracts_InfoCBNVs_TVTTCode",
                        column: x => x.TVTTCode,
                        principalTable: "InfoCBNVs",
                        principalColumn: "TVTTCode");
                    table.ForeignKey(
                        name: "FK_AnnexContracts_InsuranceContracts_HDBH",
                        column: x => x.HDBH,
                        principalTable: "InsuranceContracts",
                        principalColumn: "HDBH");
                });

            migrationBuilder.CreateTable(
                name: "PaymentPeriods",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TotalAmount = table.Column<string>(type: "text", nullable: false),
                    Period = table.Column<string>(type: "text", nullable: false),
                    FeePaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Money = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    HDBH = table.Column<string>(type: "character varying(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentPeriods_InsuranceContracts_HDBH",
                        column: x => x.HDBH,
                        principalTable: "InsuranceContracts",
                        principalColumn: "HDBH");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnexContracts_HDBH",
                table: "AnnexContracts",
                column: "HDBH");

            migrationBuilder.CreateIndex(
                name: "IX_AnnexContracts_TVTTCode",
                table: "AnnexContracts",
                column: "TVTTCode");

            migrationBuilder.CreateIndex(
                name: "IX_InfoCBNVs_BranchCode",
                table: "InfoCBNVs",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceContracts_Cif",
                table: "InsuranceContracts",
                column: "Cif");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceContracts_CollateralRef",
                table: "InsuranceContracts",
                column: "CollateralRef");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceContracts_PartnerCode",
                table: "InsuranceContracts",
                column: "PartnerCode");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceContracts_TVTTCode",
                table: "InsuranceContracts",
                column: "TVTTCode");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPeriods_HDBH",
                table: "PaymentPeriods",
                column: "HDBH");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnexContracts");

            migrationBuilder.DropTable(
                name: "PaymentPeriods");

            migrationBuilder.DropTable(
                name: "InsuranceContracts");

            migrationBuilder.DropTable(
                name: "Collaterals");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "InfoCBNVs");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "Branchs");
        }
    }
}
