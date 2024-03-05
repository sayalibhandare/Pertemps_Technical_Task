using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechnicalTest.Models;
using Newtonsoft.Json;

namespace TechnicalTest.Controllers
{
    public class JobController : Controller
    {
        // GET: JobController
        public ActionResult Index()
        {
            return View();
        }

        // GET: JobController/Details/5
        public ActionResult Details(string id)
        {

            // Retrieve item details based on the unique ID
            Root? allDataFromJason = getDeserialization();

            Job? item = allDataFromJason?.jobs.FirstOrDefault(i => i.id == id.ToString());
            ViewBag.job = item;

            if (item != null)
            {
                return View();
            }
            else {
                return View("Career/Index");
            }
        }
        private Root? getDeserialization()
        {
            string fileName = "jobs.json";
            string jsonString = System.IO.File.ReadAllText(fileName);
            Root? allDataFromJason = JsonConvert.DeserializeObject<Root>(jsonString);
            return allDataFromJason;
        }

    }
}
