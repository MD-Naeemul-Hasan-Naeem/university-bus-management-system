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
    public class DashboardStatsVM
    {
        public int TotalUsers { get; set; }
        public int TotalStudents { get; set; }
        public int TotalEmployees { get; set; }
        public int TotalBuses { get; set; }
        public int TotalRoutes { get; set; }
        public int TodayBookings { get; set; }
    }
}
