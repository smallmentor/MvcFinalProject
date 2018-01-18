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
    [Authorize(Roles = "admin")]
    public class SUsersController : Controller
    {
        private DB2 db = new DB2();

        // GET: SUsers
        public ActionResult Index()
        {
            return View(db.SUsers.ToList());
        }

        // GET: SUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUser sUser = db.SUsers.Find(id);
            if (sUser == null)
            {
                return HttpNotFound();
            }
            return View(sUser);
        }

        // GET: SUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SUsers/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Account,Name,Password")] SUser sUser)
        {
            if (ModelState.IsValid)
            {
                db.SUsers.Add(sUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sUser);
        }

        // GET: SUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUser sUser = db.SUsers.Find(id);
            if (sUser == null)
            {
                return HttpNotFound();
            }
            return View(sUser);
        }

        // POST: SUsers/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Account,Name,Password")] SUser sUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sUser);
        }

        // GET: SUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUser sUser = db.SUsers.Find(id);
            if (sUser == null)
            {
                return HttpNotFound();
            }
            return View(sUser);
        }

        // POST: SUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SUser sUser = db.SUsers.Find(id);
            db.SUsers.Remove(sUser);
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
