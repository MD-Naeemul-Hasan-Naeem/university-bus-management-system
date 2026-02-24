using BusManagement.Api.DataContext;
using BusManagement.Api.Interface;
using BusManagement.Api.ViewModel;
using Dapper;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace BusManagement.Api.Repositories
{
    public class UsersInfoRep : IUsersInfo
    {
        private readonly DapperContext _context;
        public UsersInfoRep(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UsersInfoVM>> GetAllAsync()
        {
            using var connection = _context.CreateConnection();
            connection.Open();

            var parameters = new DynamicParameters();
            parameters.Add("@flag", 1);

            return await connection.QueryAsync<UsersInfoVM>(
                "SP_UsersInfo",
                parameters,
                commandType: CommandType.StoredProcedure);
        }


        public async Task<UsersInfoVM?> GetByIdAsync(int id)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@flag", 2);
            parameters.Add("@Id", id);

            return await connection.QueryFirstOrDefaultAsync<UsersInfoVM>(
                "SP_UsersInfo",
                parameters,
                commandType: CommandType.StoredProcedure);
        }


        public async Task<int> CreateAuthAsync(UsersInfoVM model,IDbConnection connection,IDbTransaction transaction)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@flag", 3);
            parameters.Add("@Email", model.Email);
            parameters.Add("@PasswordHash", model.PasswordHash);
            parameters.Add("@Role", model.Role);
            parameters.Add("@IsActive", model.IsActive);
            parameters.Add("@IsLocked", model.IsLocked);

            return await connection.ExecuteScalarAsync<int>(
                "SP_UsersInfo",
                parameters,
                transaction,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> UpdateRoleAsync(int id, string role)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@flag", 4);
            parameters.Add("@Id", id);
            parameters.Add("@Role", role);

            return await connection.ExecuteAsync(
                "SP_UsersInfo",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> LockUnlockAsync(int id, bool isLocked)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@flag", 5);
            parameters.Add("@Id", id);
            parameters.Add("@IsLocked", isLocked);

            return await connection.ExecuteAsync(
                "SP_UsersInfo",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> SoftDeleteAsync(int id)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@flag", 6);
            parameters.Add("@Id", id);

            return await connection.ExecuteAsync(
                "SP_UsersInfo",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> ResetPasswordAsync(int id, string passwordHash)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@flag", 7);
            parameters.Add("@Id", id);
            parameters.Add("@PasswordHash", passwordHash);

            return await connection.ExecuteAsync(
                "SP_UsersInfo",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<UsersInfoVM?> GetByEmailAsync(string email)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@flag", 8);
            parameters.Add("@Email", email);

            return await connection.QueryFirstOrDefaultAsync<UsersInfoVM>(
                "SP_UsersInfo",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        

    }
}

