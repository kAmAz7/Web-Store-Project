using DAL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebProject;
using WebProject.ModelDTO;

namespace DAL
{
    public static class ConvertEX
    {

        public static User ToUser(UserDTO userDTO)
        {
            if (userDTO == null) return null;
            else
            {
                User userDB = new User(
                    userDTO.FirstName,
                    userDTO.LastName,
                    userDTO.BirthDate,
                    userDTO.Email,
                    userDTO.Username,
                     userDTO.Password);
                userDB.Id = userDTO.Id;
                return userDB;
            }
        }

        public static UserDTO ToUserDTO(User userDB)
        {
            if (userDB == null) return null;
            else
            {
                UserDTO userDTO = new UserDTO();
                userDTO.Id = userDB.Id;
                userDTO.FirstName = userDB.FirstName;
                userDTO.LastName = userDB.LastName;
                userDTO.Username = userDB.Username;
                userDTO.Password = userDB.Password;
                userDTO.ConfirmPassword = userDTO.Password;
                userDTO.Email = userDB.Email;
                userDTO.BirthDate = userDB.BirthDate;
                return userDTO;
            }
        }

        public static Product ToProduct(ProductDTO productDTO)
        {
            if (productDTO == null) return null;
            else
            {
                Product productDB = new Product(
                    productDTO.OwnerId,
                    productDTO.Title,
                    productDTO.ShortDescription,
                    productDTO.LongDescription,
                    productDTO.Price,
                    productDTO.Picture1,
                    productDTO.Picture2,
                    productDTO.Picture3);
                productDB.State = productDTO.State;
                return productDB;
            }
        }

        public static ProductDTO ToProductDTO(Product productDB)
        {
            if (productDB == null) return null;
            else
            {
                ProductDTO productDTO = new ProductDTO();

                productDTO.Id = productDB.Id;
                productDTO.OwnerId = productDB.OwnerId;
                productDTO.UserId = productDB.UserId;
                productDTO.Title = productDB.Title;
                productDTO.ShortDescription = productDB.ShortDescription;
                productDTO.LongDescription = productDB.LongDescription;
                productDTO.Price = productDB.Price;
                productDTO.Picture1 = productDB.Picture1;
                productDTO.Picture2 = productDB.Picture2;
                productDTO.Picture3 = productDB.Picture3;
                productDTO.State = productDB.State;
                productDTO.Date = productDB.Date;
                return productDTO;
            }
        }

        public static byte[] HttpPostToByteArr(HttpPostedFileBase image)
        {
            if (image == null) return null;
            else
            {
                MemoryStream target = new MemoryStream();
                image.InputStream.CopyTo(target);
                byte[] data = target.ToArray();
                return data;
            }
        }
    }
}