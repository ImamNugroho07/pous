using Microsoft.AspNetCore.Mvc;
using pous.Models;
using System.Diagnostics;
using System.Text.Json;


namespace pous.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            string check = valid();
            if (check != "Valid")
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        public IActionResult panolens()
        {
            string check = valid();
            if (check != "Valid")
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        public IActionResult googlemaps()
        {
            string check = valid();
            if (check != "Valid")
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        public IActionResult leaflet()
        {
            string check = valid();
            if (check != "Valid")
            {
                return RedirectToAction("Login", "Home");
            }
            var citymaps = a();
            return View(citymaps);
        }
        public IActionResult Place()
        {
            string check = valid();
            if (check != "Valid")
            {
                return RedirectToAction("Login", "Home");
            }
            List<Data> data = HttpContext.Session.GetObject("data");
            if (data == null)
            {
                ViewBag.Data = a();
                return View();
            }
            ViewBag.Data = data;
            return View();
        }
        public List<Data> a()
        {
            List<Data> citymaps = new List<Data>{
              new Data{
                Name = "a",
                Lat =-6.370087205348886,
                Lon = 106.81302890190516
              },
              new Data{
                Name = "b",
                Lat =-6.371452015795654,
                Lon = 106.8131576479418
              },
              new Data{
                Name = "c",
                Lat =-6.368978294188398,
                Lon = 106.80851206189793
              },
              new Data{
                Name = "d",
                Lat = -6.368786367013575,
                Lon = 106.80430635832758
              },
              new Data{
                Name = "e",
                Lat = -6.371750567596989,
                Lon = 106.80857643491177
              },
              new Data{
                Name = "f",
                Lat = -6.374608126048499,
                Lon = 106.80803999312981
              },
              new Data{
                Name = "g",
                Lat = -6.366397933946767,
                Lon = 106.81340441094912
              },
                new Data{
                Name = "h",
                Lat = -6.366397933946788,
                Lon = 106.81340441094911
              }
            };
            return citymaps;
        }
        [HttpPost]
        public ActionResult Placeload(double? lat, double? lng)
        {
            string check = valid();
            if (check != "Valid")
            {
                return RedirectToAction("Login", "Home");
            }
            if (lat == null || lng == null)
            {
                HttpContext.Session.Remove("data");
            }
            else
            {
                var citymaps = a();
                var R = 6371;
                foreach (var item in citymaps)
                {
                    var dLat = rad((double)(item.Lat - lat));
                    var dLong = rad((double)(item.Lon - lng));
                    var a = Math.Pow(Math.Sin(dLat / 2), 2) + Math.Cos((double)lat) * Math.Cos(item.Lat) * Math.Pow(Math.Sin(dLong / 2), 2);
                    var c = 2 * Math.Asin(Math.Sqrt(a));
                    item.distance = c * R;
                }
                citymaps = citymaps.OrderBy(x => x.distance).ToList();
                HttpContext.Session.SetObject("data", citymaps);
                ViewBag.Data = citymaps;
            }
            return Json(new { status = "true", msg = "Redirect now" });
        }
        [HttpPost]
        public ActionResult submitLogin(string username, string password)
        {
            if (username == "hehe@gmail.com" && password == "123")
            {
                HttpContext.Session.SetString("user", "afcsgbxha;kjcakjbcahsbjhabcshchsc.bc.k");
                return Json(new { status = "true", msg = "Redirect now" });
            }
            return Json(new { status = "faield", msg = "Wrong Password" });
        }
        public double rad(double val)
        {
            return (Math.PI / 180) * val;
        }
        public class user
        {
            public string? username { get; set; }
            public string? password { get; set; }

        }
        public string valid()
        {
            var user = HttpContext.Session.GetString("user");
            if (user != null)
            {
                if (user.ToString() == "afcsgbxha;kjcakjbcahsbjhabcshchsc.bc.k")
                {
                    return "Valid";
                }
            }
            return "notValid";
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Login", "Home"); ;
        }

    }
}