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
    public class WriterBooksController : Controller
    {
        private BookConnection db = new BookConnection();

        // GET: WriterBooks
        public ActionResult Index()
        {
            var writerBooks = db.WriterBooks.Include(w => w.Book).Include(w => w.Writer);
            return View(writerBooks.ToList());
        }

        // GET: WriterBooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WriterBook writerBook = db.WriterBooks.Find(id);
            if (writerBook == null)
            {
                return HttpNotFound();
            }
            return View(writerBook);
        }

        // GET: WriterBooks/Create
        public ActionResult Create()
        {
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookName");
            ViewBag.WriterID = new SelectList(db.Writers, "WriterID", "WriterName");
            return View();
        }

        // POST: WriterBooks/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,WriterID,BookID")] WriterBook writerBook)
        {
            if (ModelState.IsValid)
            {
                db.WriterBooks.Add(writerBook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookName", writerBook.BookID);
            ViewBag.WriterID = new SelectList(db.Writers, "WriterID", "WriterName", writerBook.WriterID);
            return View(writerBook);
        }

        // GET: WriterBooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WriterBook writerBook = db.WriterBooks.Find(id);
            if (writerBook == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookName", writerBook.BookID);
            ViewBag.WriterID = new SelectList(db.Writers, "WriterID", "WriterName", writerBook.WriterID);
            return View(writerBook);
        }

        // POST: WriterBooks/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,WriterID,BookID")] WriterBook writerBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(writerBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookName", writerBook.BookID);
            ViewBag.WriterID = new SelectList(db.Writers, "WriterID", "WriterName", writerBook.WriterID);
            return View(writerBook);
        }

        // GET: WriterBooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WriterBook writerBook = db.WriterBooks.Find(id);
            if (writerBook == null)
            {
                return HttpNotFound();
            }
            return View(writerBook);
        }

        // POST: WriterBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WriterBook writerBook = db.WriterBooks.Find(id);
            db.WriterBooks.Remove(writerBook);
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
