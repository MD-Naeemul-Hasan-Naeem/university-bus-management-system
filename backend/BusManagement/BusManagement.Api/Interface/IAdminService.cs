using BusManagement.Api.ViewModel;

namespace BusManagement.Api.Interface
{
    public interface IAdminService
    {
        Task<AdminDashboardVM> GetDashboardData();
    }
}
