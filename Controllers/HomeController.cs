using NewRelicDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NewRelicDemo.Controllers
{
    public class HomeController : Controller
    {
        demodbEntities db = new demodbEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public async Task<ActionResult> Contact()
        {
            ViewBag.Message = "Your contact page.";
            string apiUrl = "http://exampleapi.com/api/values/";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.ApiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            var kbbResource = new WebClient().DownloadString("https://www.kbb.com/");

            return View();
        }

        public async Task<ActionResult> APIData()
        {

            string apiUrl = "http://exampleapi.com/api/values/1";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.ApiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            ViewBag.GoogleResponse = CallExternalService();

            return View();
        }

        public ActionResult GetDataFromDB(string zipcode = "12345")
        {
            ViewBag.SearchZipcode = SearchZipInfo(zipcode);
            return View(db.zipcodes.Select(x => x).Take(10000));
        }

        private string CallExternalService()
        {
            return new WebClient().DownloadString("http://www.google.com/");
        }

        private string SearchZipInfo(string zipcode = "12345")
        {
            using (var connection = new SqlConnection(@"Data Source=7HF10G2;Initial Catalog=demodb;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework"))
            {
                connection.Open();
                using (var command = new SqlCommand($"SELECT * FROM zipcodes WHERE ZIP = {zipcode}", connection))
                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    return $"ZipCode: {reader[1]} | City: {reader[4]} | State: {reader[4]}";
                }
            }
        }
    }
}