using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eccomerce_shop.Models.Checkout
{
    public class CartItem
    {
        public Product.Product Product { get; set; }
        public int qty { get; set; }
    }
}