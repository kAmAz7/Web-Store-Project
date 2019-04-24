using DAL;
using DAL.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Enum;
using WebProject.ModelDTO;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        ProductManager _productManager = new ProductManager();
        UserManager _userManager = new UserManager();

        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductDTO newProduct, HttpPostedFileBase pic1,
                            HttpPostedFileBase pic2, HttpPostedFileBase pic3)
        {
            if (ModelState.IsValid)
            {
                newProduct.Picture1 = ConvertEX.HttpPostToByteArr(pic1);
                newProduct.Picture2 = ConvertEX.HttpPostToByteArr(pic2);
                newProduct.Picture3 = ConvertEX.HttpPostToByteArr(pic3);

                string username = System.Web.HttpContext.Current.User.Identity.Name;
                if (_productManager.AddProduct(newProduct, username))
                {
                    ViewBag.success = "true";
                    return View();
                }
            }
            return View();
        }

        public ActionResult GetProductsBy(SearchMethod search = SearchMethod.ByDate)
        {
            ICollection<ProductDTO> products;
            products = _productManager.GetProductsBy(search);
            ViewBag.selectedMethod = search;
            return View(products);
        }

        public ActionResult AddToCart(long id)
        {
            ProductDTO product = _productManager.GetProductById(id, false);
            List<ProductDTO> products = new List<ProductDTO>();
            if (product == null) return View("Error");
            else
            {
                if (Session["Cart"] == null)
                {
                    products.Add(product);
                    Session["Cart"] = products;
                }
                else
                {
                    products = (List<ProductDTO>)Session["Cart"];
                    products.Add(product);
                    Session["Cart"] = products;
                }

                _productManager.AddToCart(id);
                TempData["cartCounter"] = products.Count;
                return RedirectToAction("GetProductsBy");
            }
        }

        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
            {
                return View();
            }
            else
            {
                List<ProductDTO> productsInCart = (List<ProductDTO>)Session["Cart"];

                double sum = 0;
                foreach (var item in productsInCart)
                {
                    sum += item.Price;
                }
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                    sum *= 0.9;
                ViewBag.sum = sum;
                return View(productsInCart);
            }
        }

        public ActionResult ShowProductDetails(long id)
        {
            if (id > 0)
            {
                if (Session["Cart"] != null)
                {
                    List<ProductDTO> products = (List<ProductDTO>)Session["Cart"];
                    if (products.FirstOrDefault((p) => p.Id == id) != null)
                        ViewBag.isProdInCart = true;
                    else
                        ViewBag.isProdInCart = false;
                }
                else
                {
                    ViewBag.isProdInCart = false;
                }

                ProductDTO details = _productManager.GetProductById(id, true);
                if (details != null)
                {
                    return View(details);
                }
                else
                {
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            return View("Error");
        }

        public ActionResult RemoveProduct(long productId)
        {
            if (Session["Cart"] != null)
            {
                List<ProductDTO> products = (List<ProductDTO>)Session["Cart"];
                var res = products.Where((p) => p.Id != productId).ToList();
                if (res.Count == 0)
                {
                    Session["Cart"] = null;
                }
                else
                {
                    Session["Cart"] = res;
                }

                _productManager.RemoveFromCart(productId);
                return RedirectToAction("ShowCart");
            }
            return View("Error");
        }

        public ActionResult CheckOut()
        {
            if (Session["Cart"] != null)
            {
                List<ProductDTO> soldProducts = (List<ProductDTO>)Session["Cart"];

                if ((System.Web.HttpContext.Current.User.Identity.IsAuthenticated))
                {
                    string username = System.Web.HttpContext.Current.User.Identity.Name;
                    if (_productManager.CheckOut(soldProducts, username))
                    {
                        Session["Cart"] = null;
                        return View();
                    }
                }
                else
                {
                    if (_productManager.CheckOut(soldProducts))
                    {
                        Session["Cart"] = null;
                        return View();
                    }
                }
            }
            return View("Error");
        }
    }
}