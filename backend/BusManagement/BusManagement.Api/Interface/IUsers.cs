using BusManagement.Api.ViewModel;

namespace BusManagement.Api.Interface
{
    public interface IUsers
    {
        Task<dynamic> GetAllUsersAsync();
        Task<dynamic> GetUsersById(int Id);
        Task<dynamic> SaveUsers(UsersVM model);
        Task<dynamic> DeleteUsers(int Id);
    }
}
