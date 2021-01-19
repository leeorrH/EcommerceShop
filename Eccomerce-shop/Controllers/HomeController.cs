using Eccomerce_shop.Models.Product;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Eccomerce_shop.Models.Checkout;
using Eccomerce_shop.Models;

namespace Eccomerce_shop.Controllers
{
    public class HomeController : Controller
    {
        public static List<Product> _allProducts = new List<Product>();
        public List<Product> _displayProducts;
        public static string selectedCategory = "Poster";

        public HomeController()
        {
            //SetProductsFromjsonFile();
        }
        [Route("Index/{categoryName}")]
        public ActionResult Index(string categoryName, int? page)
        {
            if (_allProducts.Count == 0) SetProductsFromjsonFile();
            if (String.IsNullOrEmpty(categoryName)) categoryName = selectedCategory = "Poster";
            else selectedCategory = categoryName;
            ViewBag.category = selectedCategory;
            SetDisplayProducts(categoryName);
            if (categoryName == null)
            {
                page = 1;
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(_displayProducts.ToPagedList(pageNumber, pageSize));
        }

        private void SetProductsFromjsonFile()
        {

            var json = System.IO.File.ReadAllText(Server.MapPath(@"~/Models/Product/art.json"));
            _allProducts = JsonConvert.DeserializeObject<List<Product>>(json);
            for (int i = 0; i < _allProducts.Count; i++)
            {
                _allProducts[i].Id = i;
            }
            HttpContext.Session["allProducts"] = _allProducts;
        }

        public void SetDisplayProducts(string categoryName)
        {

            _displayProducts = _allProducts.Where(product => product.Category.ToLower().Equals(categoryName.ToLower())).ToList();
        }

        public void EncreaseQty(int productId)
        {
            EditCart(productId, true);

        }

        public void DecreaseQty(int productId)
        {
            EditCart(productId, false);

        }

        private void EditCart(int productId, bool isAdd)
        {
            List<CartItem> cart;
            bool isAddToCartNeeded = false;
            if (Session["cart"] == null) cart = new List<CartItem>();
            else cart = (List<CartItem>)Session["cart"];

            var item = cart.FirstOrDefault(cartItem => cartItem.Product.Id == productId);
            if (item != null)
            {
                int prevQty = item.qty;
                cart.Remove(item);
                if (!isAdd)
                {
                    if (prevQty - 1 > 1) isAddToCartNeeded = true;
                }
                else isAddToCartNeeded = true;

                if (isAddToCartNeeded)
                {
                    cart.Add(new CartItem()
                    {
                        Product = item.Product,
                        qty = isAdd ? prevQty + 1 : prevQty - 1
                    });
                }
            }
            else
            {
                if (isAdd)
                {
                    var prod = _allProducts.FirstOrDefault(prodct => prodct.Id == productId);
                    cart.Add(new CartItem()
                    {
                        Product = prod,
                        qty = 1
                    });
                }
            }
            Session["cart"] = cart;
        }

        [HttpPost]
        public void InitCart(CartRequest cart)
        {
            List<CartItem> cartFromLS = new List<CartItem>();
            foreach (PartialProduct p in cart.items)
            {
                Product fullProduct = _allProducts.FirstOrDefault(product => product.Id == Int16.Parse(p.id));
                cartFromLS.Add(new CartItem()
                {
                    Product = fullProduct,
                    qty = p.qty
                });
            }
            Session["cart"] = cartFromLS;
        }
    }
}