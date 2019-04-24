using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Enum;
using WebProject.ModelDTO;

namespace WebProject.Interface
{
    public interface IProduct
    {
        bool AddProduct(ProductDTO newProductDTO, string userName); 
        ProductDTO GetProductById(long productId, bool withOwner); 
        ICollection<ProductDTO> GetProductsBy(SearchMethod searchBy = SearchMethod.ByDate); 
        bool AddToCart(long productId); 
        void ChangeProductStatus(); 
        bool CheckOut(ICollection<ProductDTO> soldProducts, string userName=null); 
        bool RemoveFromCart(long productId); 
    }
}

