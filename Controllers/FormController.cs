using TechnicalTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace TechnicalTest.Controllers
{

    public class FormController : Controller
    {
        [Obsolete]
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment { get; }

        [Obsolete]
        public FormController(Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment)
        {
            Environment = _environment;
        }
        // GET: FormController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FormController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FormController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FormController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                FormModel data = new FormModel();
                data.Name = collection["name"];
                data.Surname = collection["surname"];
                data.Email = collection["email"];
                data.Phone = collection["phone"];
                ViewBag.users = data;

                return View("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult UploadFile(IFormFile file)
        {

            return View("Index");
        }

        // GET: FormController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FormController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FormController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FormController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
