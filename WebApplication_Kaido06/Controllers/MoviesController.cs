using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication_Kaido06.Models;

namespace WebApplication_Kaido06.Controllers
{
    public class MoviesController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        // GET: Movies
        public ActionResult Index(string movieGenre, string searchString)
        {
            var GenreLst = new List<string>();

            var GenreQry = from d in db.Movies
                           orderby d.Genre
                           select d.Genre;

            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.movieGenre = new SelectList(GenreLst);

            var movies = from m in db.Movies
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            return View(movies);
        }
        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Sort(string sort)
        {
            // ソート列／順序を判別するキー文字列を生成
            ViewBag.TitleX = (sort == "Title" ? "dTitle" : "Title");
            ViewBag.ReleaseDate = (sort == "ReleaseDate" ? "dReleaseDate" : "ReleaseDate");
            ViewBag.Published = (sort == "Genre" ? "dGenre" : "Genre");
            ViewBag.Viewcount = (sort == "Price" ? "dPrice" : "Price");
            ViewBag.Viewcount = (sort == "Rating" ? "dRating" : "Rating");

            // デフォルトではソート指定なし
            var articles = from a in db.Movies select a;

            // 渡されたキー文字列に従って、ソート式を決定
            switch (sort)
            {
                case "Title":
                    articles = articles.OrderBy(a => a.Title);
                    break;

                case "ReleaseDate":
                    articles = articles.OrderBy(a => a.ReleaseDate);
                    break;

                case "Genre":
                    articles = articles.OrderBy(a => a.Genre);
                    break;

                case "Price":
                    articles = articles.OrderBy(a => a.Price);
                    break;

                case "Rating":
                    articles = articles.OrderBy(a => a.Rating);
                    break;

                case "dTitle":
                    articles = articles.OrderByDescending(a => a.Title);
                    break;

                case "dReleaseDate":
                    articles = articles.OrderByDescending(a => a.ReleaseDate);
                    break;

                case "dGenre":
                    articles = articles.OrderByDescending(a => a.Genre);
                    break;

                case "dPrice":
                    articles = articles.OrderByDescending(a => a.Price);
                    break;

                case "dRating":
                    articles = articles.OrderByDescending(a => a.Rating);
                    break;

                default:
                    articles = articles.OrderBy(a => a.Title);
                    break;

            }

            return View(articles);

        }

    }
}
