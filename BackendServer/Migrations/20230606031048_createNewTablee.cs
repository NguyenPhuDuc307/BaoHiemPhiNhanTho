using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendServer.Migrations
{
    public partial class createNewTablee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branchs",
                columns: table => new
                {
                    BranchCode = table.Column<string>(type: "text", nullable: false),
                    BranchName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branchs", x => x.BranchCode);
                });

            migrationBuilder.CreateTable(
                name: "Collaterals",
                columns: table => new
                {
                    Ref = table.Column<string>(type: "text", nullable: false),
                    StatusCollateral = table.Column<string>(type: "text", nullable: true),
                    ValueCollateral = table.Column<decimal>(type: "numeric", nullable: true),
                    AddressCollateral = table.Column<string>(type: "text", nullable: true),
                    Relationship = table.Column<string>(type: "text", nullable: true),
                    PropertyType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaterals", x => x.Ref);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Cif = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CustomerType = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    CCCD = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Cif);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    PartnersCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.PartnersCode);
                });

            migrationBuilder.CreateTable(
                name: "InfoCBNVs",
                columns: table => new
                {
                    TVTTCode = table.Column<string>(type: "text", nullable: false),
                    NameTVTT = table.Column<string>(type: "text", nullable: true),
                    Cif = table.Column<string>(type: "text", nullable: true),
                    BranchCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoCBNVs", x => x.TVTTCode);
                    table.ForeignKey(
                        name: "FK_InfoCBNVs_Branchs_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "Branchs",
                        principalColumn: "BranchCode");
                });

            migrationBuilder.CreateTable(
                name: "InsuranceContracts",
                columns: table => new
                {
                    HDBH = table.Column<string>(type: "text", nullable: false),
                    NewOrRenewed = table.Column<string>(type: "text", nullable: true),
                    STBH = table.Column<decimal>(type: "numeric", nullable: true),
                    InsuranceFee = table.Column<decimal>(type: "numeric", nullable: true),
                    NumberOfPayments = table.Column<int>(type: "integer", nullable: true),
                    FromDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ToDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Exception = table.Column<string>(type: "text", nullable: true),
                    Beneficiaries = table.Column<string>(type: "text", nullable: true),
                    InsuranceType = table.Column<string>(type: "text", nullable: true),
                    Cif = table.Column<string>(type: "text", nullable: true),
                    CustomerCif = table.Column<string>(type: "text", nullable: true),
                    TVTTCode = table.Column<string>(type: "text", nullable: true),
                    InfoCBNVTVTTCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceContracts", x => x.HDBH);
                    table.ForeignKey(
                        name: "FK_InsuranceContracts_Customers_CustomerCif",
                        column: x => x.CustomerCif,
                        principalTable: "Customers",
                        principalColumn: "Cif");
                    table.ForeignKey(
                        name: "FK_InsuranceContracts_InfoCBNVs_InfoCBNVTVTTCode",
                        column: x => x.InfoCBNVTVTTCode,
                        principalTable: "InfoCBNVs",
                        principalColumn: "TVTTCode");
                });

            migrationBuilder.CreateTable(
                name: "AnnexContracts",
                columns: table => new
                {
                    HDPL = table.Column<string>(type: "text", nullable: false),
                    NewOrRenewed = table.Column<bool>(type: "boolean", nullable: true),
                    STBH = table.Column<decimal>(type: "numeric", nullable: true),
                    InsuranceFee = table.Column<decimal>(type: "numeric", nullable: true),
                    NumberOfPayments = table.Column<string>(type: "text", nullable: true),
                    FromDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ToDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Exception = table.Column<string>(type: "text", nullable: true),
                    HDBH = table.Column<string>(type: "text", nullable: true),
                    InsuranceContractHDBH = table.Column<string>(type: "text", nullable: true),
                    TVTTCode = table.Column<string>(type: "text", nullable: true)
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
                        name: "FK_AnnexContracts_InsuranceContracts_InsuranceContractHDBH",
                        column: x => x.InsuranceContractHDBH,
                        principalTable: "InsuranceContracts",
                        principalColumn: "HDBH");
                });

            migrationBuilder.CreateTable(
                name: "PaymentPeriods",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    TotalAmount = table.Column<int>(type: "integer", nullable: true),
                    Period = table.Column<string>(type: "text", nullable: true),
                    FeePaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Money = table.Column<decimal>(type: "numeric", nullable: true),
                    HDBH = table.Column<string>(type: "text", nullable: true),
                    InsuranceContractHDBH = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentPeriods_InsuranceContracts_InsuranceContractHDBH",
                        column: x => x.InsuranceContractHDBH,
                        principalTable: "InsuranceContracts",
                        principalColumn: "HDBH");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnexContracts_InsuranceContractHDBH",
                table: "AnnexContracts",
                column: "InsuranceContractHDBH");

            migrationBuilder.CreateIndex(
                name: "IX_AnnexContracts_TVTTCode",
                table: "AnnexContracts",
                column: "TVTTCode");

            migrationBuilder.CreateIndex(
                name: "IX_InfoCBNVs_BranchCode",
                table: "InfoCBNVs",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceContracts_CustomerCif",
                table: "InsuranceContracts",
                column: "CustomerCif");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceContracts_InfoCBNVTVTTCode",
                table: "InsuranceContracts",
                column: "InfoCBNVTVTTCode");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPeriods_InsuranceContractHDBH",
                table: "PaymentPeriods",
                column: "InsuranceContractHDBH");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnexContracts");

            migrationBuilder.DropTable(
                name: "Collaterals");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "PaymentPeriods");

            migrationBuilder.DropTable(
                name: "InsuranceContracts");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "InfoCBNVs");

            migrationBuilder.DropTable(
                name: "Branchs");
        }
    }
}
