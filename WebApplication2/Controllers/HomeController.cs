using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private DB2 db = new DB2();

        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }




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


                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.check = "帳號密碼錯誤";
                return View();
            }

            //  return Content(account + ", " + password);
        }

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

            //檢查帳密是否正確，須直接放到資料庫

            //if((account=="a1") && (password=="test"))
            //{
            //    return true;
            //}

            //if ((account == "t1") && (password == "test"))
            //{
            //    return true;
            //}

            //if ((account == "s1") && (password == "test"))
            //{
            //    return true;
            //}

            //return false;
        }

        //建立使用者的Cookie及建立roles角色
        private void LoginProcess(string account, bool isRemeber)
        {

            var now = DateTime.Now;
            //  string roles = string.Join(",", user.SRoles.Select(x => x.Name).ToArray());

            //下面的資料，實際上要讀來自資料庫的資料
            string roles = "";
            //if (account == "a1")
            //    roles = "admin,tea,stud";
            //else if (account == "t1")
            //    roles = "tea";
            //else if (account == "s1")
            //    roles = "stud";

            roles = String.Join(",", db.SUserRoles.Where(x => x.Account == account).Select(x => x.RoleId).ToList());

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
    }
}