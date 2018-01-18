using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{

    public class HomeController : Controller
    {
        private StoreConnection db = new StoreConnection();

        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Publisher).Include(b => b.Writer);
            return View(books.ToList());
        }

        // GET: Home/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Home/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.PubID = new SelectList(db.Publishers, "PubID", "PubName");
            ViewBag.WriterID = new SelectList(db.Writers, "WriterID", "WriterName");
            return View();
        }

        // POST: Home/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "BookID,BookName,Price,ISBN,PubID,WriterID")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PubID = new SelectList(db.Publishers, "PubID", "PubName", book.PubID);
            ViewBag.WriterID = new SelectList(db.Writers, "WriterID", "WriterName", book.WriterID);
            return View(book);
        }

        // GET: Home/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.PubID = new SelectList(db.Publishers, "PubID", "PubName", book.PubID);
            ViewBag.WriterID = new SelectList(db.Writers, "WriterID", "WriterName", book.WriterID);
            return View(book);
        }

        // POST: Home/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "BookID,BookName,Price,ISBN,PubID,WriterID")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PubID = new SelectList(db.Publishers, "PubID", "PubName", book.PubID);
            ViewBag.WriterID = new SelectList(db.Writers, "WriterID", "WriterName", book.WriterID);
            return View(book);
        }

        // GET: Home/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
    }
}
