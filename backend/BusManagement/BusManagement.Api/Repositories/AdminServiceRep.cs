using BusManagement.Api.DataContext;
using BusManagement.Api.Interface;
using BusManagement.Api.ViewModel;
using Dapper;
using System.Data;
using System.Text;

public class AdminServiceRep : IAdminService
{
    private readonly DapperContext _context;

    public AdminServiceRep(DapperContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateUserAsync(AdminDashboardVM model)
    {
        using var connection = _context.CreateConnection();
        connection.Open();

        using var transaction = connection.BeginTransaction();

        try
        {
            var authParams = new DynamicParameters();
            authParams.Add("@flag", 3);
            authParams.Add("@Email", model.Email);
            authParams.Add("@PasswordHash", HashPassword(model.Password));
            authParams.Add("@Role", model.Role);

            var userInfoId = await connection.ExecuteScalarAsync<int>(
                "SP_UsersInfo",
                authParams,
                transaction,
                commandType: CommandType.StoredProcedure);

            var profileParams = new DynamicParameters();
            profileParams.Add("@flag", 3);
            profileParams.Add("@UserInfoId", userInfoId);
            profileParams.Add("@Name", model.Name);
            profileParams.Add("@Phone", model.Phone);
            profileParams.Add("@Department", model.Department);
            profileParams.Add("@StudentId", model.StudentId);
            profileParams.Add("@EmployeeId", model.EmployeeId);

            await connection.ExecuteAsync(
                "SP_Users",
                profileParams,
                transaction,
                commandType: CommandType.StoredProcedure);

            transaction.Commit();
            return true;
        }
        catch
        {
            transaction.Rollback();
            return false;
        }
    }

    private string HashPassword(string password)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        return Convert.ToBase64String(sha256.ComputeHash(bytes));
    }

    public async Task<DashboardStatsVM> GetDashboardData()
    {
        using var connection = _context.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<DashboardStatsVM>(
            "SP_AdminDashboard",
            commandType: CommandType.StoredProcedure);
    }
}