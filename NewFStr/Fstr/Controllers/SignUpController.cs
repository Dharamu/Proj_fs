using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MODALobj;
using BL;


namespace Store.Controllers
{
    public class SignUpController : Controller
    {
        // GET: SignUp
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            try
            {
                if (collection["password"].Equals(collection["confirm"]))
                {

                    SignUpObj signUp = new SignUpObj();
                    signUp.uname = collection["name"];
                    signUp.Email = collection["Email"];
                    signUp.passwrd = collection["password"];
                    signUp.confirmpwd = collection["confirm"];
                    BlSignup.createNew(signUp);
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {

                return View();
            }
        }
    }
}