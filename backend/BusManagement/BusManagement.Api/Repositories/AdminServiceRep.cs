using BusManagement.Api.DataContext;
using BusManagement.Api.Interface;
using BusManagement.Api.ViewModel;
using Dapper;
using System.Data;

public class AdminServiceRep : IAdminService
{
    private readonly DapperContext _context;

    public AdminServiceRep(DapperContext context)
    {
        _context = context;
    }

    public async Task<AdminDashboardVM> GetDashboardData()
    {
        using (var connection = _context.CreateConnection())
        {
            return await connection.QueryFirstOrDefaultAsync<AdminDashboardVM>(
                "SP_AdminDashboard",
                commandType: CommandType.StoredProcedure
            );
        }
    }
}