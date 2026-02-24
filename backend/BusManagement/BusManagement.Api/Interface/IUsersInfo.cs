using BusManagement.Api.ViewModel;
using System.Data;

namespace BusManagement.Api.Interface
{
    public interface IUsersInfo
    {
        Task<IEnumerable<UsersInfoVM>> GetAllAsync();
        Task<UsersInfoVM?> GetByEmailAsync(string email);
        Task<UsersInfoVM?> GetByIdAsync(int id);

        Task<int> CreateAuthAsync(
            UsersInfoVM model,
            IDbConnection connection,
            IDbTransaction transaction);

        Task<int> UpdateRoleAsync(int id, string role);
        Task<int> LockUnlockAsync(int id, bool isLocked);
        Task<int> SoftDeleteAsync(int id);
        Task<int> ResetPasswordAsync(int id, string passwordHash);
    }
}
