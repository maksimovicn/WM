using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WM.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Manufacterer { get; set; }
        public decimal Price { get; set; }
        public string Supplier { get; set; }
        public string Category { get; set; }

    }
}