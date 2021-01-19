using Eccomerce_shop.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eccomerce_shop.Controllers
{
    public class ProductDetailsController : Controller
    {
        private List<Product> _allProducts = new List<Product>();

        public ProductDetailsController()
        {
            //GetProductsData();
        }

        private void GetProductsData()
        {
           
        }

        // GET: ProductDetails
        [Route("ProductData/{productId}")]
        public ActionResult ProductData(int productId)
        {
            List<Product> sameCategoryProducts;
            try
            {
                _allProducts = HttpContext.Session["allProducts"] as List<Product>;
                if (_allProducts.Count == 0) throw new Exception("Somthing went wrong on getting all products from tempdata");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Product data = _allProducts.FirstOrDefault(product => product.Id == productId);
            if (data != null)
            {
              sameCategoryProducts = _allProducts.Where(product => product.Category.Equals(data.Category)).ToList();
            }

            return View(data);
        }
    }
}