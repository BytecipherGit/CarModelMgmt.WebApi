using CarModelMgmt.Core.Entities;
using CarModelMgmt.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarModelMgmt.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperHelper _dapperHelper;
        public UserRepository(DapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }
        public async Task<IEnumerable<Menu>> GetMenusForUser(string roleName)
        {
            try
            {
                var parameters = new
                {
                    RoleName = roleName
                };
                return await _dapperHelper.QueryAsync<Menu>("GetMenusForRole", parameters);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<User>> Login(LoginViewModel model)
        {
            try
            {
                var parameters = new
                {
                    UserName = model.UserName,
                    Password = model.Password,
                };
                var users= await _dapperHelper.QueryAsync<User>("AuthenticateUser", parameters);

                foreach (var user in users)
                {
                    string userRole = user.Role;
                    // Do something with userRole, for example, add it to a list or check its value
                }

                return users;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
