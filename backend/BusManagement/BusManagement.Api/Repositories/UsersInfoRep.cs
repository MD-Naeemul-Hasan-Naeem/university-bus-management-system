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
        public async Task<dynamic> GetAllUsersAsync()
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@flag", 1);
                    var data = await connection.QueryAsync<dynamic>(
                        "SP_UsersInfo",
                        parameters,
                        commandType: CommandType.StoredProcedure
                        );
                    return (data);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<dynamic> login(UsersInfoVM model)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    

                    var parameters = new DynamicParameters();
                    parameters.Add("@flag", 3);
                    parameters.Add("@Email", model.Email);
                    parameters.Add("@PasswordHash", model.PasswordHash);

                    


                    var data = await connection.QueryFirstOrDefaultAsync<dynamic>(
                        "SP_UsersInfo",
                        parameters,
                        commandType: CommandType.StoredProcedure
                        );
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<dynamic> Save(UsersInfoVM model)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    // Step 1: Hash the plain password
                    var hasher = new PasswordHasher<UsersInfoVM>();
                    model.PasswordHash = hasher.HashPassword(model, model.Password);

                    var parameters = new DynamicParameters();
                    parameters.Add("@flag", 2);
                    parameters.Add("@Id", model.Id);
                    parameters.Add("@FullName", model.FullName);
                    parameters.Add("@Email", model.Email);
                    parameters.Add("@PasswordHash", model.PasswordHash);  // store string directly
                    parameters.Add("@UserType", model.UserType);

                    var data = await connection.QueryFirstOrDefaultAsync<dynamic>(
                        "SP_UsersInfo",
                        parameters,
                        commandType: CommandType.StoredProcedure
                        );
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
