using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.ModelDTO;

namespace WebProject.Interface
{
    public interface IUser
    {
        UserDTO GetUserByUserName(string userName); 
        bool CreateUser(UserDTO newUser, out bool userExist); 
        bool LoginUser(string userName, string password);
        bool UpdateUserDetails(UserDTO updatedDetails); 
    }
}
