using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookingAppStore.Models;
using BookingAppStore.Util;

namespace BookingAppStore.Controllers
{
    public class HomeController : Controller
    {

        BookContext db = new BookContext();

        public ActionResult Index()
        {
            var books = db.Books;
            ViewBag.Books = books;
            ViewBag.Authors = db.Books.Select(s => s.Author).Distinct();
            return View();
        }

        public ActionResult BestBook()
        {
            Book book = db.Books.First();
            return PartialView(book);
        }

        //[HttpPost]
        public ActionResult BookSearch(string name)
        {
            var allbooks = db.Books.Where(a => a.Author.Contains(name)).ToList();
            return PartialView(allbooks);
        }

        public string GetContext()
        {
            string browser = HttpContext.Request.Browser.Browser;
            string user_agent = HttpContext.Request.UserAgent;
            string url = HttpContext.Request.RawUrl;
            string ip = HttpContext.Request.UserHostAddress;
            string referrer = HttpContext.Request.UrlReferrer == null ? "" : HttpContext.Request.UrlReferrer.AbsoluteUri;
            return "<p>Browser: " + browser + "</p><p>User-Agent: " + user_agent + "</p><p>Url запроса: " + url +
                    "</p><p>Реферер: " + referrer + "</p><p>IP-адрес: " + ip + "</p>";

        }

        public ActionResult GetImage()
        {
            string path = "../Content/Images/photo_2017.jpg";
            return new ImageResult(path);
        }

        public ActionResult GetHtml()
        {
            return new HtmlResult("<h2>Привет мир!<h2>");
        }

        public RedirectResult GetVoid()
        {
            return Redirect("/Home/Contact");
        }

        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BoockId = id;
            return View();
        }

        public int GetId(int id)
        {
            return id;
        }

        public string Square(int a, int b)
        {
            double s = a * b / 2;
            return "<h2>Площадь треугольника с основанием " + a + " и высотой " + b + " равна " + s + "<h2>";
        }

        [HttpGet]
        public ActionResult GetBook()
        {
            return View();
        }

        [HttpPost]
        public string GetBook(string title, string author)
        {
            return title + " " + author;
        }

        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            // добавление информации о покупке в базу данных
            db.Purchases.Add(purchase);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return "Спасибо, " + purchase.Person + ", за покупку";
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}