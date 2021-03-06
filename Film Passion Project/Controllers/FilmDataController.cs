using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Film_Passion_Project.Models;
using System.Diagnostics;

namespace Film_Passion_Project.Controllers
{
    public class FilmDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/FilmData/ListFilms
        [HttpGet]
        public IEnumerable<FilmDto> ListFilms()
        {
            List<Film> Films = db.Films.ToList();
            List<FilmDto> FilmDtos = new List<FilmDto>();

            Films.ForEach(f => FilmDtos.Add(new FilmDto()
            {
                FilmId = f.FilmId,
                FilmName = f.FilmName,
                FilmYear = f.FilmYear,
                DirectorName = f.DirectorName,
                FilmPlot = f.FilmPlot,
                StudioId = f.Studio.StudioId
            }));

            return FilmDtos;
        }

        // GET: api/FilmData/FindFilm/5
        [ResponseType(typeof(Film))]
        [HttpGet]
        public IHttpActionResult FindFilm(int id)
        {
            Film Film = db.Films.Find(id);
            FilmDto FilmDto = new FilmDto()
            {
                FilmId = Film.FilmId,
                FilmName = Film.FilmName,
                FilmYear = Film.FilmYear,
                DirectorName = Film.DirectorName,
                FilmPlot = Film.FilmPlot,
                StudioId= Film.Studio.StudioId
            };
            if (Film == null)
            {
                return NotFound();
            }

            return Ok(FilmDto);
        }

        // POST: api/FilmData/UpdateFilm/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateFilm(int id, Film film)
        {
            Debug.WriteLine("I have reached the update film method");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != film.FilmId)
            {
                return BadRequest();
            }

            db.Entry(film).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/FilmData/AddFilm
        [ResponseType(typeof(Film))]
        [HttpPost]
        public IHttpActionResult AddFilm(Film film)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Films.Add(film);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = film.FilmId }, film);
        }

        // POST: api/FilmData/DeleteFilm/5
        [ResponseType(typeof(Film))]
        [HttpPost]
        public IHttpActionResult DeleteFilm(int id)
        {
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return NotFound();
            }

            db.Films.Remove(film);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FilmExists(int id)
        {
            return db.Films.Count(e => e.FilmId == id) > 0;
        }
    }
}