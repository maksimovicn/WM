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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product newProduct)
        {

            if (ModelState.IsValid)
            {
                String connectionString = "Data Source=localhost;port=3306;Initial Catalog=product;User Id=root;password=";
                String sql = "INSERT INTO product (name, price, description, manufacturer, supplier, category) values (@name, @price, @description, @manufacturer, @supplier, @category)";

                var model = new List<Product>();
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", newProduct.Name);
                    cmd.Parameters.AddWithValue("@price", newProduct.Price);
                    cmd.Parameters.AddWithValue("@description", newProduct.Description);
                    cmd.Parameters.AddWithValue("@manufacturer", newProduct.Manufacterer);
                    cmd.Parameters.AddWithValue("@supplier", newProduct.Supplier);
                    cmd.Parameters.AddWithValue("@category", newProduct.Category);

                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("ProductsDB");
            }
            else
            {
                return View(newProduct);
            }
        }

        public ActionResult Edit(int id)
        {
            String connectionString = "Data Source=localhost;port=3306;Initial Catalog=product;User Id=root;password=";
            String sql = "SELECT * FROM product where ID=@id";

            var model = new List<Product>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

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
            return View(model.ElementAt(0));
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                String connectionString = "Data Source=localhost;port=3306;Initial Catalog=product;User Id=root;password=";
                String sql = "UPDATE product set name = @name, price = @price, description = @description, manufacturer = @manufacturer, supplier = @supplier, category = @category where id = @id";

                var model = new List<Product>();
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", product.Name);
                    cmd.Parameters.AddWithValue("@price", product.Price);
                    cmd.Parameters.AddWithValue("@description", product.Description);
                    cmd.Parameters.AddWithValue("@manufacturer", product.Manufacterer);
                    cmd.Parameters.AddWithValue("@supplier", product.Supplier);
                    cmd.Parameters.AddWithValue("@category", product.Category);

                    cmd.Parameters.AddWithValue("@id", product.ID);

                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("ProductsDB");
            }
            else
            {
                return View(product);
            }
        }
    }
}