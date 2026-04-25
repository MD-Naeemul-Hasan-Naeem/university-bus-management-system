namespace BusManagement.Api.ViewModel
{
    public class BusVM
    {
        public int? Id { get; set; }

        public string? BusNumber { get; set; }

        public int? Capacity { get; set; }

        public int? DriverId { get; set; }

        public string? Status { get; set; }

        // From DB
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // For SP control
        public int Flag { get; set; }
    }
}
