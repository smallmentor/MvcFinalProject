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
    public class SUserRolesController : Controller
    {
        private DB2 db = new DB2();

        // GET: SUserRoles
        public ActionResult Index()
        {
            return View(db.SUserRoles.ToList());
        }

        // GET: SUserRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUserRole sUserRole = db.SUserRoles.Find(id);
            if (sUserRole == null)
            {
                return HttpNotFound();
            }
            return View(sUserRole);
        }

        // GET: SUserRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SUserRoles/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Account,RoleId")] SUserRole sUserRole)
        {
            if (ModelState.IsValid)
            {
                db.SUserRoles.Add(sUserRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sUserRole);
        }

        // GET: SUserRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUserRole sUserRole = db.SUserRoles.Find(id);
            if (sUserRole == null)
            {
                return HttpNotFound();
            }
            return View(sUserRole);
        }

        // POST: SUserRoles/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Account,RoleId")] SUserRole sUserRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sUserRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sUserRole);
        }

        // GET: SUserRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUserRole sUserRole = db.SUserRoles.Find(id);
            if (sUserRole == null)
            {
                return HttpNotFound();
            }
            return View(sUserRole);
        }

        // POST: SUserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SUserRole sUserRole = db.SUserRoles.Find(id);
            db.SUserRoles.Remove(sUserRole);
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
