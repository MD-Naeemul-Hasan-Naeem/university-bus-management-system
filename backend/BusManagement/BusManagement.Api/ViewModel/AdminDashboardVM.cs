namespace BusManagement.Api.ViewModel
{
    public class AdminDashboardVM
    {
        // Auth Fields
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        // Profile Fields
        public string Name { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Department { get; set; }
        public string? StudentId { get; set; }
        public string? EmployeeId { get; set; }
    }
}
