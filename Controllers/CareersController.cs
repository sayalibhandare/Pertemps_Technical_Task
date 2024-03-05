using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechnicalTest.Models;
using Newtonsoft.Json;

namespace TechnicalTest.Controllers
{
    public class CareersController : Controller
    {
        // GET: CareersController
        public ActionResult Index()
        {
            // Deserialization Jason file into Root object
            Root? allDataFromJason = getDeserialization();

            // Send all Job data with category list
            ViewBag.users = allDataFromJason.jobs;
            ViewBag.categories = getCategories(allDataFromJason);

            // Completed serialization to update Jason file with unique id
            getSerialization(allDataFromJason);

            return View();
        }

        private Root? getDeserialization()
        {
            string fileName = "jobs.json";
            string jsonString = System.IO.File.ReadAllText(fileName);
            Root? allDataFromJason = System.Text.Json.JsonSerializer.Deserialize<Root>(jsonString);

            // Create unique ID for each item from Json
            GenerateUniqueIds(allDataFromJason);

            return allDataFromJason;
        }
        public void getSerialization(Root data)
        {
            string fileName = "jobs.json";
            string jsonString = System.IO.File.ReadAllText(fileName);
            jsonString = JsonConvert.SerializeObject(data);
            System.IO.File.WriteAllText(fileName, jsonString);
        }
        private void GenerateUniqueIds(Root? allDataFromJason)
        {
            
            foreach (var item in allDataFromJason.jobs)
            {
                DateTime postingDate = item.posted_date;
                // Generate a unique ID using Guid
                item.id = Guid.NewGuid().ToString().Substring(0, 8);

                // generate link depends on job title and unique id
                item.link = item.job_title.ToLower().Replace(" ", "-") + "-" + item.id;

                // Calculate how long ago the job was posted
                item.postAge = GetAgeString(DateTime.Now - postingDate);

            }
        }
        static string GetAgeString(TimeSpan age)
        {
            // Determine the most appropriate time unit
            if(age.TotalDays >= 1)
            {
                int days = (int)Math.Floor(age.TotalDays);
                return $"{days} {(days == 1 ? "day" : "days")}";
            }
            else if (age.TotalHours >= 1)
            {
                int hours = (int)Math.Floor(age.TotalHours);
                return $"{hours} {(hours == 1 ? "hour" : "hours")}";
            }
            else
            {
                int minutes = (int)Math.Floor(age.TotalMinutes);
                return $"{minutes} {(minutes == 1 ? "minute" : "minutes")}";
            }
        }

        static List<string> getCategories(Root allDataFromJason) {

            // Create a list to store categories
            List<string> categories = new List<string>();

            // Extract "category" property and add it to the list if it's not already present
            foreach (var item in allDataFromJason.jobs)
            {
                string category = item.category;
                if (!categories.Contains(category))
                {
                    categories.Add(category);
                }
            }
            return categories;
        }
    }
}
