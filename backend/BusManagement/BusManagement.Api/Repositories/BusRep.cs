using BusManagement.Api.DataContext;
using BusManagement.Api.Interface;
using BusManagement.Api.ViewModel;
using Dapper;
using System.Data;

namespace BusManagement.Api.Repository
{
    public class BusRep : IBusService
    {
        private readonly DapperContext _context;

        public BusRep(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BusVM>> GetAllBuses()
        {
            using var connection = _context.CreateConnection();

            return await connection.QueryAsync<BusVM>(
                "SP_Buses",
                new { flag = 1 },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<BusVM?> GetBusById(int id)
        {
            using var connection = _context.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<BusVM>(
                "SP_Buses",
                new { flag = 5, Id = id },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int> CreateBus(BusVM model)
        {
            using var connection = _context.CreateConnection();

            try
            {
                var result = await connection.ExecuteScalarAsync<int>(
                    "SP_Buses",
                    new
                    {
                        flag = 2,
                        model.BusNumber,
                        model.Capacity,
                        model.DriverId,
                        model.Status
                    },
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateBus(BusVM model)
        {
            using var connection = _context.CreateConnection();

            try
            {
                var result = await connection.ExecuteScalarAsync<int>(
                    "SP_Buses",
                    new
                    {
                        flag = 3,
                        model.Id,
                        model.BusNumber,
                        model.Capacity,
                        model.DriverId,
                        model.Status
                    },
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteBus(int id)
        {
            using var connection = _context.CreateConnection();

            var result = await connection.ExecuteAsync(
                "SP_Buses",
                new { flag = 4, Id = id },
                commandType: CommandType.StoredProcedure
            );

            return result > 0;
        }

        public async Task<bool> ChangeStatus(int id, string status)
        {
            using var connection = _context.CreateConnection();

            var result = await connection.ExecuteAsync(
                "SP_Buses",
                new { flag = 6, Id = id, Status = status },
                commandType: CommandType.StoredProcedure
            );

            return result > 0;
        }
    }
}