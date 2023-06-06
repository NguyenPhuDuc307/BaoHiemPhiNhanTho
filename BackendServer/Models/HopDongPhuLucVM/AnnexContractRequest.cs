﻿using BaoHiemPhiNhanTho.BackendServer.Models;

namespace BackendServer.Models.HopDongPhuLucVM
{
    public class AnnexContractRequest
    {
        public string? HDPL { get; set; }
        public bool? NewOrRenewed { get; set; }
        public decimal? STBH { get; set; }
        public decimal? InsuranceFee { get; set; }
        public string? NumberOfPayments { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? Exception { get; set; }
        public string? HDBH { get; set; }
        public string? TVTTCode { get; set; }
        public string? Status { get; set; }
    }
}