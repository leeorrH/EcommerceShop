using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eccomerce_shop.Models
{
    public class CartRequest
    {
        public string id { get; set; }
        public List<PartialProduct> items {get; set; }
    }

    public class PartialProduct
    {
        public string id { get; set; }
        public int qty { get; set; }
    }

}

