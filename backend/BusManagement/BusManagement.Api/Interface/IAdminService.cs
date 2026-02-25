using BusManagement.Api.ViewModel;

namespace BusManagement.Api.Interface
{
    public interface IAdminService
    {
        Task<DashboardStatsVM> GetDashboardData();
        Task<bool> CreateUserAsync(AdminDashboardVM model);
    }
}
