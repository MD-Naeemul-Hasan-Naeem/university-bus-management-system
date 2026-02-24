using BusManagement.Api.DataContext;
using BusManagement.Api.Interface;
using BusManagement.Api.ViewModel;
using System.Data;
using Dapper;

namespace BusManagement.Api.Repositories
{
    public class UsersRep : IUsers
    {
        private readonly DapperContext _context;
        public UsersRep(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateProfileAsync(
        UsersVM model,
        IDbConnection connection,
        IDbTransaction transaction)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@flag", 3); // INSERT
            parameters.Add("@UserInfoId", model.UserInfoId);
            parameters.Add("@FullName", model.FullName);
            parameters.Add("@Phone", model.Phone);
            parameters.Add("@Department", model.Department);
            parameters.Add("@StudentId", model.StudentId);
            parameters.Add("@EmployeeId", model.EmployeeId);

            return await connection.ExecuteScalarAsync<int>(
                "SP_Users",
                parameters,
                transaction,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> UpdateProfileAsync(UsersVM model)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@flag", 4);
            parameters.Add("@Id", model.Id);
            parameters.Add("@FullName", model.FullName);
            parameters.Add("@Phone", model.Phone);
            parameters.Add("@Department", model.Department);
            parameters.Add("@StudentId", model.StudentId);
            parameters.Add("@EmployeeId", model.EmployeeId);

            return await connection.ExecuteAsync(
                "SP_Users",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> DeleteProfileAsync(int id)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@flag", 5);
            parameters.Add("@Id", id);

            return await connection.ExecuteAsync(
                "SP_Users",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<UsersVM?> GetProfileByUserInfoIdAsync(int userInfoId)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@flag", 2);
            parameters.Add("@UserInfoId", userInfoId);

            return await connection.QueryFirstOrDefaultAsync<UsersVM>(
                "SP_Users",
                parameters,
                commandType: CommandType.StoredProcedure);
        }
    }
}
