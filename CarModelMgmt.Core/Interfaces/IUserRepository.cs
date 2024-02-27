using CarModelMgmt.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarModelMgmt.Core.Interfaces
{
    public interface IUserRepository
    {
        
        Task<IEnumerable<Menu>> GetMenusForUser(string roleName);
        Task <IEnumerable<User>> Login(LoginViewModel model);
    }
}
