using BusManagement.Api.ViewModel;

namespace BusManagement.Api.Interface
{
    public interface IUsers
    {
        Task<IEnumerable<UsersVM>> GetAllUsersAsync();
        Task<UsersVM?> GetUsersById(int Id);
        Task<int> SaveUsers(UsersVM model);
        Task<int> DeleteUsers(int Id);
    }
}
