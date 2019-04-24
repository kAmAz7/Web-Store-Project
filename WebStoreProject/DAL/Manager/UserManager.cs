using DAL.DB;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Interface;
using WebProject.ModelDTO;

namespace DAL.Manager
{
    public class UserManager : IUser
    {
        public bool CreateUser(UserDTO newUser, out bool userExist)
        {
            userExist = false;

            if (newUser == null) return false;
            if (GetUserByUserNameFromDB(newUser.Username) != null)
            {
                userExist = true;
                return false;
            }
            else
            {
                using (var context = new StoreContextDB())
                {
                    User createdUser = ConvertEX.ToUser(newUser);
                    context.UserTable.Add(createdUser);
                    context.SaveChanges();
                    return true;
                }
            }
        }  

        public UserDTO GetUserByUserName(string userName)
        {
            if (userName == null) return null;
            else
            {
                using (var context = new StoreContextDB())
                {
                    User foundUser = context.UserTable.FirstOrDefault(u => u.Username == userName);
                    return ConvertEX.ToUserDTO(foundUser);
                }
            }
        } 

        internal User GetUserByUserNameFromDB(string userName)
        {
            if (userName == null) return null;
            else
            {
                using (var context = new StoreContextDB())
                {
                    User foundUser = context.UserTable.FirstOrDefault(u => u.Username == userName);
                    return (foundUser);
                }
            }
        }  

        public bool LoginUser(string userName, string password)
        {

            if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password))
            {
                using (var context = new StoreContextDB())
                {
                    var foundUser = context.UserTable
                        .FirstOrDefault(u => u.Username == userName && u.Password == password);
                    if (foundUser != null)
                        return true;
                }
            }
            return false;
        } 

        public bool UpdateUserDetails(UserDTO updatedDetails)
        {
            if (updatedDetails == null) return false;
            else
            {
                using (var context = new StoreContextDB())
                {
                    User userToUpdate = GetUserByUserNameFromDB(updatedDetails.Username);
                    if (userToUpdate == null) return false;
                    else
                    {
                        context.UserTable.Attach(userToUpdate);
                        userToUpdate.FirstName = updatedDetails.FirstName;
                        userToUpdate.LastName = updatedDetails.LastName;
                        userToUpdate.Password = updatedDetails.Password;
                        userToUpdate.Email = updatedDetails.Email;
                        userToUpdate.BirthDate = updatedDetails.BirthDate;
                        context.SaveChanges();
                        return true;
                    }
                }
            }
        } 
    }
}
