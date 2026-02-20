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

        public async Task<int> DeleteUsers(int Id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", 3);
                parameters.Add("@Id", Id);
                using (var connection = _context.CreateConnection())
                {
                    var data = await connection.ExecuteAsync(
                        "SP_Users",
                        parameters,
                        commandType: CommandType.StoredProcedure
                        );
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UsersVM>> GetAllUsersAsync()
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@flag", 1);
                    var data = await connection.QueryAsync<UsersVM>(
                        "SP_Users",
                        parameters,
                        commandType: CommandType.StoredProcedure
                        );
                    return data;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UsersVM?> GetUsersById(int Id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", 4);
                parameters.Add("@Id", Id);
                using (var connection = _context.CreateConnection())
                {
                    var data = await connection.QueryFirstOrDefaultAsync<UsersVM>(
                        "SP_Users",
                        parameters,
                        commandType: CommandType.StoredProcedure
                        );
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> SaveUsers(UsersVM model)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var parameters = new DynamicParameters();
                parameters.Add("@flag", 2);
                parameters.Add("@Id", model.Id);
                parameters.Add("@Name", model.Name);
                parameters.Add("@Email", model.Email);
                parameters.Add("@Role", model.Role);
                return await connection.ExecuteAsync(
                     "SP_Users",
                     parameters,
                     commandType: CommandType.StoredProcedure
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
