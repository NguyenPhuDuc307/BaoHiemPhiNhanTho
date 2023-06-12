namespace BackendServer.Models.CollateralViewModel
{
    public class CollateralRequest
    {
        public string? Ref { get; set; }
        public string? StatusCollateral { get; set; }
        public Decimal? ValueCollateral { get; set; }
        public string? AddressCollateral { get; set; }
        public string? Relationship { get; set; }
        public string? PropertyType { get; set; }
        public string? HDBH { get; set; }
    }
}
