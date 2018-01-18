using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AdminController : Controller
    {
        private DB2 db = new DB2();

        // GET: Admin
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
        //登入
        public ActionResult Logon()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Logon(string account, string password)
        {
            if (CheckPw(account, password))
            {
                LoginProcess(account, true);

                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.check = "帳號密碼錯誤";
                return View();
            }
        }
        //檢查帳密
        public bool CheckPw(string account, string password)
        {
            SUser user = db.SUsers.Where(x => x.Account == account && x.Password == password).SingleOrDefault();
            if (user != null)
            {
                Session["user"] = user.Name;
                return true;
            }
            else
                return false;
        }

        //建立使用者的Cookie及建立roles角色
        private void LoginProcess(string account, bool isRemeber)
        {

            var now = DateTime.Now;

            string roles = String.Join(",", db.SUserRoles.Where(x => x.Account == account).Select(x => x.RoleId).ToList());

            var ticket = new FormsAuthenticationTicket(
                version: 1,
                name: account,
                issueDate: now,
                expiration: now.AddMinutes(30),
                isPersistent: isRemeber,
                userData: roles,
                cookiePath: FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(cookie);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            //清除所有的 session
            Session.RemoveAll();

            //建立一個同名的 Cookie 來覆蓋原本的 Cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            //建立 ASP.NET 的 Session Cookie 同樣是為了覆蓋
            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            return RedirectToAction("Index", "Home");
        }

        //註冊帳號
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup([Bind(Include = "Id,Account,Name,Password")] SUser sUser)
        {
            if (ModelState.IsValid)
            {
                db.SUsers.Add(sUser);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(sUser);
        }
    }
}