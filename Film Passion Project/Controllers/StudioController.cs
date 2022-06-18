using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using Film_Passion_Project.Models;
using System.Web.Script.Serialization;


namespace Film_Passion_Project.Controllers
{
    public class StudioController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();

        static StudioController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44397/api/StudioData/");
        }

        // GET: Studio/List
        public ActionResult List()
        {
            //Objective: Communicate with our studio data api to retrieve a list of studios
            //curl https://localhost:44397/api/StudioData/ListStudios

            string url = "ListStudios";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<StudioDto> studios = response.Content.ReadAsAsync<IEnumerable<StudioDto>>().Result;
            Debug.WriteLine("Number of Studios received: ");
            Debug.WriteLine(studios.Count());

            return View(studios);
        }

        // GET: Studio/Details/5
        public ActionResult Details(int id)
        {
            //Objective: Communicate with our studio data api to retrieve a details about one studio
            //curl https://localhost:44397/api/StudioData/FindStudio/{id}

            string url = "findstudio/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is");
            Debug.WriteLine(response.StatusCode);

            StudioDto SelectedStudio = response.Content.ReadAsAsync<StudioDto>().Result;
            Debug.WriteLine("Studio received: ");
            Debug.WriteLine(SelectedStudio.StudioName);
             

            return View(SelectedStudio);
        }

        // GET: Studio/Create
        public ActionResult New()
        {
            return View();
        }

        // POST: Studio/Create
        [HttpPost]
        public ActionResult Create(Studio studio)
        {
            Debug.WriteLine("the inputed Film Name is:");
            Debug.WriteLine(studio.StudioName);
            //objective:add a new film into the system using api
            //curl -H "Content-Type:application/json" -d @film.json https://localhost:44397/api/StudioData/addstudio
            string url = "addstudio";

            string jsonpayload = jss.Serialize(studio);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Errors");
            }



            return RedirectToAction("List");
        }

        // GET: Studio/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Studio/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Studio/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Studio/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
