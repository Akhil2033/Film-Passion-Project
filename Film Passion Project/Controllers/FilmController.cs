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
    public class FilmController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();


        static FilmController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44397/api/FilmData/");
        }
        // GET: Film/List
        public ActionResult List()
        {
            //objective: communicate with our film data api to retrieve a list of film
            //curl https://localhost:44397/api/FilmData/ListFilms

            string url = "ListFilms";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("the response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<FilmDto> films = response.Content.ReadAsAsync<IEnumerable<FilmDto>>().Result;
            Debug.WriteLine("Number of Films received: ");
            Debug.WriteLine(films.Count());

            
            return View(films);
        }

        // GET: Film/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our film data api to retrieve a list of film
            //curl https://localhost:44397/api/FilmData/FindFilm/{id}

            string url = "FindFilm/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("the response code is ");
            Debug.WriteLine(response.StatusCode);

            FilmDto selectedfilm = response.Content.ReadAsAsync<FilmDto>().Result;
            Debug.WriteLine("Film received: ");
            Debug.WriteLine(selectedfilm.FilmName);
            return View(selectedfilm);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Film/Create
        public ActionResult New()
        {
            return View();
        }

        // POST: Film/Create
        [HttpPost]
        public ActionResult Create(Film film)
        {
            Debug.WriteLine("the inputed Film Name is:");
            Debug.WriteLine(film.FilmName);
            //objective:add a new film into the system using api
            //curl -H "Content-Type:application/json" -d @film.json https://localhost:44397/api/FilmData/addfilm
            string url = "addfilm";

            string jsonpayload = jss.Serialize(film);

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

        // GET: Film/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Film/Edit/5
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

        // GET: Film/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Film/Delete/5
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
