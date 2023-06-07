using BackendServer.Data.Enums;

namespace BackendServer.Models.HopDongPhuLucVM
{
    public class CustomerRequest
    {
        public string? Cif { get; set; }
        public string? Name { get; set; }
        public CustomerType CustomerType { get; set; }
        public string? Gender { get; set; }
        public string? CCCD { get; set; }
    }
}
