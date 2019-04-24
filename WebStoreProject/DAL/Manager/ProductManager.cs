using DAL.DB;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Enum;
using WebProject.Interface;
using WebProject.ModelDTO;

namespace DAL.Manager
{
    public class ProductManager : IProduct
    {
        UserManager _userManger = new UserManager();

        public bool AddProduct(ProductDTO newProductDTO, string userName)
        {
            if (newProductDTO == null || userName == null) return false;
            else
            {
                User owner = _userManger.GetUserByUserNameFromDB(userName);
                newProductDTO.OwnerId = owner.Id;
                Product newProductDB = ConvertEX.ToProduct(newProductDTO);
                using (var context = new StoreContextDB())
                {
                    context.ProductTable.Add(newProductDB);
                    context.SaveChanges();
                    return true;
                }
            }
        }

        public void ChangeProductStatus() 
        {
            using (var context = new StoreContextDB())
            {
                DateTime now = DateTime.Now;
                TimeSpan deleteTime = new TimeSpan(0, 5, 0);
                now -= deleteTime;

                var ProductsToChange = context.ProductTable.Where((p) => p.State == ProductState.InCart && p.DateInCart.CompareTo(now) <= 0).ToList();

                foreach (var item in ProductsToChange)
                {
                    item.State = ProductState.Available;
                }

                context.SaveChanges();
            }
        } 

        public ProductDTO GetProductById(long productId, bool withOwner)
        {
            if (productId <= 0) return null;
            else
            {
                using (var context = new StoreContextDB())
                {
                    Product productFromDB = context.ProductTable.FirstOrDefault((p) => p.Id == productId);
                    if (productFromDB != null)
                    {
                        ProductDTO prodToReturn = ConvertEX.ToProductDTO(productFromDB);
                        if (withOwner)
                            prodToReturn.Owner = ConvertEX.ToUserDTO(context.UserTable.FirstOrDefault((u) => u.Id == prodToReturn.OwnerId));
                        return prodToReturn;
                    }
                    else return null;
                }
            }
        } 

        internal Product GetProductByIdFromDB(long productId)
        {
            if (productId <= 0) return null;
            else
            {
                using (var context = new StoreContextDB())
                {
                    Product productFromDB = context.ProductTable.FirstOrDefault((p) => p.Id == productId);
                    if (productFromDB != null) return (productFromDB);
                    else return null;
                }
            }
        } 

        public ICollection<ProductDTO> GetProductsBy(SearchMethod searchBy = SearchMethod.ByDate)
        {
            List<Product> productsDB = new List<Product>();
            List<ProductDTO> productsDTO = new List<ProductDTO>();
            using (var context = new StoreContextDB())
            {
                switch (searchBy)
                {
                    case SearchMethod.ByDate:
                        {
                            productsDB = context.ProductTable.Where((p) => p.State == ProductState.Available).OrderByDescending((p) => p.Date).ToList();
                            break;
                        }
                    case SearchMethod.ByName:
                        {
                            productsDB = context.ProductTable.Where((p) => p.State == ProductState.Available).OrderBy((p) => p.Title).ToList();
                            break;
                        }
                    case SearchMethod.ByPrice:
                        {
                            productsDB = context.ProductTable.Where((p) => p.State == ProductState.Available).OrderBy((p) => p.Price).ToList();
                            break;
                        }
                }

                foreach (var product in productsDB)
                {
                    productsDTO.Add(ConvertEX.ToProductDTO(product));
                }
                return productsDTO;
            }
        } 

        public bool AddToCart(long productId)
        {
            if (productId <= 0) return false;
            else
            {
                using (var context = new StoreContextDB())
                {
                    Product productToAdd = GetProductByIdFromDB(productId);
                    productToAdd.State = ProductState.InCart;
                    productToAdd.DateInCart = DateTime.Now;
                    context.Entry(productToAdd).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
            }
        } 

        public bool RemoveFromCart(long productId)
        {
            Product tmpProd = GetProductByIdFromDB(productId);
            if (tmpProd == null) return false;
            using (var context = new StoreContextDB())
            {
                tmpProd.State = ProductState.Available;
                context.Entry(tmpProd).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
        } 

        public bool CheckOut(ICollection<ProductDTO> soldProducts, string userName = null)
        {
            if (soldProducts != null)
            {
                foreach (var item in soldProducts)
                {
                    using (var context = new StoreContextDB())
                    {
                        Product tmpProd = GetProductByIdFromDB(item.Id);
                        tmpProd.State = ProductState.Sold;
                        if (userName != null)
                        {
                            User buyer = _userManger.GetUserByUserNameFromDB(userName);
                            tmpProd.UserId = buyer.Id;
                        }
                        else
                        {
                            tmpProd.UserId = 400;
                        }
                        context.Entry(tmpProd).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                    }
                }
                return true;
            }
            return false;
        }  
    }
}
