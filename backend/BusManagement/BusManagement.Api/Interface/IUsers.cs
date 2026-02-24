using BusManagement.Api.ViewModel;
using System.Data;

namespace BusManagement.Api.Interface
{
    public interface IUsers
    {
        Task<int> CreateProfileAsync(UsersVM model, IDbConnection connection, IDbTransaction transaction);
        Task<int> UpdateProfileAsync(UsersVM model);
        Task<int> DeleteProfileAsync(int id);
        Task<UsersVM?> GetProfileByUserInfoIdAsync(int userInfoId);
    }
}
