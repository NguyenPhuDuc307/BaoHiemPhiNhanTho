using BackendServer.Data.Enums;

namespace BackendServer.Models.CustomerViewModel
{
    public class CustomerRequest
    {
        public string? Cif { get; set; }
        public string? Name { get; set; }
        public string? CustomerType { get; set; }
        public string? Gender { get; set; }
        public string? CCCD { get; set; }
    }
}
