using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WM.Models;

namespace WM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductsDB()
        {
            String connectionString = "Data Source=localhost;port=3306;Initial Catalog=product;User Id=root;password=";
            String sql = "SELECT * FROM product";

            var model = new List<Product>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var product = new Product();
                    product.ID = (int)rdr["id"];
                    product.Name = (string)rdr["name"];
                    product.Description = (string)rdr["description"];
                    product.Manufacterer = (string)rdr["manufacturer"];
                    product.Price = (decimal)rdr["price"];
                    product.Category = (string)rdr["category"];
                    product.Supplier = (string)rdr["supplier"];

                    model.Add(product);
                }

            }
            return View(model);
        }

        public ActionResult ProductsJSON()
        {
            var model = new List<Product>();
            List<Models.Product> items;
            using (System.IO.StreamReader r = new System.IO.StreamReader("file.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Models.Product>>(json);
            }

            foreach (Product i in items)
            {
                var product = new Product();
                product.ID = i.ID;
                product.Name = i.Name;
                product.Description = i.Description;
                product.Manufacterer = i.Manufacterer;
                product.Price = i.Price;
                product.Category = i.Category;
                product.Supplier = i.Supplier;

                model.Add(product);
            }
            return View(model);
        }
    }
}