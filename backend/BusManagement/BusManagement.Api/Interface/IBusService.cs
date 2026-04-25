using BusManagement.Api.ViewModel;

namespace BusManagement.Api.Interface
{
    public interface IBusService
    {
        Task<IEnumerable<BusVM>> GetAllBuses();

        Task<BusVM?> GetBusById(int id);

        Task<int> CreateBus(BusVM model);

        Task<int> UpdateBus(BusVM model);

        Task<bool> DeleteBus(int id);

        Task<bool> ChangeStatus(int id, string status);
    }
}
