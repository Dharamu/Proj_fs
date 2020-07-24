using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MODALobj;
using BL;

namespace Store.Controllers
{
    public class LoginController : Controller
    {

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login/Details/5
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                string emailn = collection["email"];
                string passwrdn = collection["password"];

                bool test = BlLogin.CheckValidity(emailn, passwrdn);
                if (test)
                {

                    LoginObj lg = BlLogin.GetItemsDetails(emailn, passwrdn).First();
                    FormsAuthentication.SetAuthCookie(lg.uname, false);
                    //string Uid = lg.uType.ToString();
                    Session["UserId"] = Convert.ToInt32(lg.u_id);

                    //string Type = lg.uType.ToString();
                    //if (Type.Equals("Admin"))
                    //{
                    //    string name = lg.uname.ToString();
                    //    Session["LoggedIn"] = name;
                    //    Session.Timeout = 90;
                    //    return RedirectToAction("Index", "Home", new { Area = "Admin" });
                    //}
                    //else if (Type.Equals("User"))
                    //{

                    //    //string name = lg.uname.ToString();
                    //    string name = "sunil";
                    //    Session["LoggedIn"] = name;
                    //    Session.Timeout = 90;
                    //    return RedirectToAction("Index", "Home");
                    //}
                    //else
                    //{
                    //    return View();
                    //}
                }
                else
                {
                    return View();
                }
            }

            string email = collection["email"];
            string passwrd = collection["password"];
            if (email.Equals(null) || passwrd.Equals(null))
            {
                return View();
            }
            else 
            {

                bool test = BlLogin.CheckValidity(email, passwrd);
                //if (test.Equals(true))
                if (test)
                {
                    
                    LoginObj lg = BlLogin.GetItemsDetails(email, passwrd).First();
                    FormsAuthentication.SetAuthCookie(lg.uname, false);


                    string Type = lg.uType.ToString();
                    if (Type.Equals("Admin"))
                    {
                        string name = lg.uname.ToString();
                        Session["LoggedIn"] = name;
                        Session.Timeout = 90;
                        return RedirectToAction("Index", "Home", new { Area="Admin" });
                    }
                    else if (Type.Equals("User"))
                    {

                        //string name = lg.uname.ToString();
                        string name = "sunil";
                        Session["LoggedIn"] = name;
                        Session.Timeout = 90;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }
                
            }


            //return View();



        }

        // GET: Login/Details/5
        [HttpPost]
        public ActionResult IndexOld(FormCollection collection)
        {
            if (ModelState.IsValid)
            {

            }

            string email = collection["email"];
            string passwrd = collection["password"];
            if (email.Equals(null) || passwrd.Equals(null))
            {
                return View();
            }
            else
            {

                bool test = BlLogin.CheckValidity(email, passwrd);
                //if (test.Equals(true))
                if (test)
                {

                    LoginObj lg = BlLogin.GetItemsDetails(email, passwrd).First();
                    FormsAuthentication.SetAuthCookie(lg.uname, false);


                    string Type = lg.uType.ToString();
                    if (Type.Equals("Admin"))
                    {
                        string name = lg.uname.ToString();
                        Session["LoggedIn"] = name;
                        Session.Timeout = 90;
                        return RedirectToAction("Index", "Home", new { Area = "Admin" });
                    }
                    else if (Type.Equals("User"))
                    {

                        //string name = lg.uname.ToString();
                        string name = "sunil";
                        Session["LoggedIn"] = name;
                        Session.Timeout = 90;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }

            }


            //return View();



        }

        // GET: Login
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("../");
        }

        public ActionResult Index1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index1(FormCollection collection)
        {
            return View("Index1");
        }
    }
}
