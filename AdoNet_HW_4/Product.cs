using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet_HW_4
{
    public class Product
    {
        public string Product_name { get; set; }
        public string Firm_name { get; set; }
        public double Price { get; set; }

        public Product()
        {
            Product_name = "";
            Firm_name = "";
            Price = 0;
        }
        public Product(string Product_name, string Firm_name, double Price)
        {
            this.Product_name = Product_name;
            this.Firm_name = Firm_name;
            this.Price = Price;
        }
    }
}
