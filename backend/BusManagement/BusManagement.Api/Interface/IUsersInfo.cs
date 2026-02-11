using BusManagement.Api.ViewModel;

namespace BusManagement.Api.Interface
{
    public interface IUsersInfo
    {
        Task<dynamic> GetAllUsersAsync();
        Task<dynamic> Save(UsersInfoVM model);

        Task<dynamic> login(UsersInfoVM model);
    }
}
