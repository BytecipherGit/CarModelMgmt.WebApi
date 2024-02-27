using CarModelMgmt.Core.Entities;
using CarModelMgmt.Core.Interfaces;
using CarModelMgmt.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarModelMgmt.Services.Services
{
    public class MenuService : IMenuService
    {
        private readonly IUserRepository _userRepository;
        public MenuService(IUserRepository userRepository)
        {
            _userRepository=userRepository;
        }
        public async Task<IEnumerable<Menu>> GetMenusForUser(string roleName)
        {
            try
            {
                return await _userRepository.GetMenusForUser(roleName);
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
                return await _userRepository.Login(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
