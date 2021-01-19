using Eccomerce_shop.Models.Checkout;
using Eccomerce_shop.Models.Product;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Web.Mvc;

namespace Eccomerce_shop.Controllers
{
    public class CheckoutController : Controller
    {
        public List<Product> _allProducts = new List<Product>();

        public CheckoutController()
        {

        }
        // GET: Checkout
        public ActionResult CheckoutPage()
        {
            return View();
        }

        [HttpPost]
        public string SubmitOrder(UserDetails userDetails)
        {
            try
            {
                MailMessage mm = new MailMessage("onlineStore@gmail.com", userDetails.Email);
                mm.Subject = "Your Order details";
                mm.IsBodyHtml = true;
                string body = RenderPartialViewToString();
                mm.Body = body;

                SmtpClient smtp = new SmtpClient();
                smtp.Send(mm);
            }catch
            {
                return "Failure";
            }
            //save data to DB
            return "Success";
        }

        public string RenderPartialViewToString()
        {
            //need to optimize! 
            StringWriter sw = new StringWriter();
            ViewEngineResult result = ViewEngines.Engines.FindView(ControllerContext, "CheckoutPage", "_Layout");
            ViewContext context = new ViewContext(ControllerContext, result.View, ViewData, TempData, sw);
            result.View.Render(context, sw);
            result.ViewEngine.ReleaseView(ControllerContext, result.View);

            string viewString = sw.ToString();

            return viewString;
        }
    }
}


//public ActionResult SetCartProducts(SetCartProductsRequest req) 
//{
//    List<Product> result = new List<Product>();
//    try
//    {
//        _allProducts = HttpContext.Session["allProducts"] as List<Product>;
//        if (_allProducts.Count == 0) throw new Exception("Somthing went wrong on getting all products from tempdata");
//    }
//    catch (Exception e)
//    {
//        Console.WriteLine(e.Message);
//    }

//    foreach(string itemId in req.Items)
//    {
//        var item  = _allProducts.FirstOrDefault(product => product.Id == Int16.Parse(itemId));
//        if(item != null)
//        {
//            result.Add(item);
//        }
//    }


//    //think what view to set here
//    return View(result);
//}