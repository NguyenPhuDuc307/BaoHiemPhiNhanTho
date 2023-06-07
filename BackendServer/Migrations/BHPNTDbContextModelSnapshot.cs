﻿// <auto-generated />
using System;
using BackendServer.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BackendServer.Migrations
{
    [DbContext(typeof(BHPNTDbContext))]
    partial class BHPNTDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BaoHiemPhiNhanTho.BackendServer.Models.AnnexContract", b =>
                {
                    b.Property<string>("HDPL")
                        .HasColumnType("text");

                    b.Property<string>("Exception")
                        .HasColumnType("text");

                    b.Property<DateTime?>("FromDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("HDBH")
                        .HasColumnType("text");

                    b.Property<string>("InsuranceContractHDBH")
                        .HasColumnType("text");

                    b.Property<decimal?>("InsuranceFee")
                        .HasColumnType("numeric");

                    b.Property<bool?>("NewOrRenewed")
                        .HasColumnType("boolean");

                    b.Property<int?>("NumberOfPayments")
                        .HasColumnType("integer");

                    b.Property<decimal?>("STBH")
                        .HasColumnType("numeric");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string>("TVTTCode")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ToDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("HDPL");

                    b.HasIndex("InsuranceContractHDBH");

                    b.HasIndex("TVTTCode");

                    b.ToTable("AnnexContracts");
                });

            modelBuilder.Entity("BaoHiemPhiNhanTho.BackendServer.Models.Branch", b =>
                {
                    b.Property<string>("BranchCode")
                        .HasColumnType("text");

                    b.Property<string>("BranchName")
                        .HasColumnType("text");

                    b.HasKey("BranchCode");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("BaoHiemPhiNhanTho.BackendServer.Models.Collateral", b =>
                {
                    b.Property<string>("Ref")
                        .HasColumnType("text");

                    b.Property<string>("AddressCollateral")
                        .HasColumnType("text");

                    b.Property<string>("PropertyType")
                        .HasColumnType("text");

                    b.Property<string>("Relationship")
                        .HasColumnType("text");

                    b.Property<string>("StatusCollateral")
                        .HasColumnType("text");

                    b.Property<decimal?>("ValueCollateral")
                        .HasColumnType("numeric");

                    b.HasKey("Ref");

                    b.ToTable("Collaterals");
                });

            modelBuilder.Entity("BaoHiemPhiNhanTho.BackendServer.Models.Customer", b =>
                {
                    b.Property<string>("Cif")
                        .HasColumnType("text");

                    b.Property<string>("CCCD")
                        .HasColumnType("text");

                    b.Property<int>("CustomerType")
                        .HasColumnType("integer");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Cif");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BaoHiemPhiNhanTho.BackendServer.Models.InfoCBNV", b =>
                {
                    b.Property<string>("TVTTCode")
                        .HasColumnType("text");

                    b.Property<string>("BranchCode")
                        .HasColumnType("text");

                    b.Property<string>("BranchCode1")
                        .HasColumnType("text");

                    b.Property<string>("NameTVTT")
                        .HasColumnType("text");

                    b.HasKey("TVTTCode");

                    b.HasIndex("BranchCode1");

                    b.ToTable("InfoCBNVs");
                });

            modelBuilder.Entity("BaoHiemPhiNhanTho.BackendServer.Models.InsuranceContract", b =>
                {
                    b.Property<string>("HDBH")
                        .HasColumnType("text");

                    b.Property<string>("Beneficiaries")
                        .HasColumnType("text");

                    b.Property<string>("Cif")
                        .HasColumnType("text");

                    b.Property<string>("CollateralRef")
                        .HasColumnType("text");

                    b.Property<string>("CustomerCif")
                        .HasColumnType("text");

                    b.Property<string>("Exception")
                        .HasColumnType("text");

                    b.Property<DateTime?>("FromDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("InfoCBNVTVTTCode")
                        .HasColumnType("text");

                    b.Property<decimal?>("InsuranceFee")
                        .HasColumnType("numeric");

                    b.Property<string>("InsuranceType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("NewOrRenewed")
                        .HasColumnType("text");


                    b.Property<int?>("NumberOfPayments")
                        .HasColumnType("integer");

                    b.Property<string>("OtherInsuranceType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("PartnerCode")
                        .HasColumnType("text");

                    b.Property<string>("PartnerCode1")
                        .HasColumnType("text");

                    b.Property<decimal?>("STBH")
                        .HasColumnType("numeric");

                    b.Property<string>("TVTTCode")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ToDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("HDBH");

                    b.HasIndex("CollateralRef");

                    b.HasIndex("CustomerCif");

                    b.HasIndex("InfoCBNVTVTTCode");

                    b.HasIndex("PartnerCode1");

                    b.ToTable("InsuranceContracts");
                });

            modelBuilder.Entity("BaoHiemPhiNhanTho.BackendServer.Models.Partner", b =>
                {
                    b.Property<string>("PartnerCode")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("PartnerCode");

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("BaoHiemPhiNhanTho.BackendServer.Models.PaymentPeriod", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<DateTime?>("FeePaymentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("HDBH")
                        .HasColumnType("text");

                    b.Property<string>("InsuranceContractHDBH")
                        .HasColumnType("text");

                    b.Property<decimal?>("Money")
                        .HasColumnType("numeric");

                    b.Property<string>("Period")
                        .HasColumnType("text");

                    b.Property<int?>("TotalAmount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("InsuranceContractHDBH");

                    b.ToTable("PaymentPeriods");
                });

            modelBuilder.Entity("BaoHiemPhiNhanTho.BackendServer.Models.AnnexContract", b =>
                {
                    b.HasOne("BaoHiemPhiNhanTho.BackendServer.Models.InsuranceContract", "InsuranceContract")
                        .WithMany("AnnexContracts")
                        .HasForeignKey("InsuranceContractHDBH");

                    b.HasOne("BaoHiemPhiNhanTho.BackendServer.Models.InfoCBNV", "InfoCBNV")
                        .WithMany("AnnexContracts")
                        .HasForeignKey("TVTTCode");

                    b.Navigation("InfoCBNV");

                    b.Navigation("InsuranceContract");
                });

            modelBuilder.Entity("BaoHiemPhiNhanTho.BackendServer.Models.InfoCBNV", b =>
                {
                    b.HasOne("BaoHiemPhiNhanTho.BackendServer.Models.Branch", "Branch")
                        .WithMany("InfoCBNVs")
                        .HasForeignKey("BranchCode1");

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("BaoHiemPhiNhanTho.BackendServer.Models.InsuranceContract", b =>
                {
                    b.HasOne("BaoHiemPhiNhanTho.BackendServer.Models.Collateral", "Collateral")
                        .WithMany("InsuranceContracts")
                        .HasForeignKey("CollateralRef");

                    b.HasOne("BaoHiemPhiNhanTho.BackendServer.Models.Customer", "Customer")
                        .WithMany("InsuranceContracts")
                        .HasForeignKey("CustomerCif");

                    b.HasOne("BaoHiemPhiNhanTho.BackendServer.Models.InfoCBNV", "InfoCBNV")
                        .WithMany("InsuranceContracts")
                        .HasForeignKey("InfoCBNVTVTTCode");

                    b.HasOne("BaoHiemPhiNhanTho.BackendServer.Models.Partner", "Partner")
                        .WithMany("InsuranceContracts")
                        .HasForeignKey("PartnerCode1");

                    b.Navigation("Collateral");

                    b.Navigation("Customer");

                    b.Navigation("InfoCBNV");

                    b.Navigation("Partner");
                });

            modelBuilder.Entity("BaoHiemPhiNhanTho.BackendServer.Models.PaymentPeriod", b =>
                {
                    b.HasOne("BaoHiemPhiNhanTho.BackendServer.Models.InsuranceContract", "InsuranceContract")
                        .WithMany("PaymentPeriods")
                        .HasForeignKey("InsuranceContractHDBH");

                    b.Navigation("InsuranceContract");
                });

            modelBuilder.Entity("BaoHiemPhiNhanTho.BackendServer.Models.Branch", b =>
                {
                    b.Navigation("InfoCBNVs");
                });

            modelBuilder.Entity("BaoHiemPhiNhanTho.BackendServer.Models.Collateral", b =>
                {
                    b.Navigation("InsuranceContracts");
                });

            modelBuilder.Entity("BaoHiemPhiNhanTho.BackendServer.Models.Customer", b =>
                {
                    b.Navigation("InsuranceContracts");
                });

            modelBuilder.Entity("BaoHiemPhiNhanTho.BackendServer.Models.InfoCBNV", b =>
                {
                    b.Navigation("AnnexContracts");

                    b.Navigation("InsuranceContracts");
                });

            modelBuilder.Entity("BaoHiemPhiNhanTho.BackendServer.Models.InsuranceContract", b =>
                {
                    b.Navigation("AnnexContracts");

                    b.Navigation("PaymentPeriods");
                });

            modelBuilder.Entity("BaoHiemPhiNhanTho.BackendServer.Models.Partner", b =>
                {
                    b.Navigation("InsuranceContracts");
                });
#pragma warning restore 612, 618
        }
    }
}
