using BL;
using MODALobj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Security;
using System.Net.Mail;
using System.Net;

namespace Fstr.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<newProduct> list = new List<newProduct>();

            list = Blproduct.NewProductGetItems();

            ViewBag.tfive = (from li in list
                            where li.Category.Equals("Vegetable")
                            select li).Take(5).ToList();
            foreach (var item in ViewBag.tfive)
            {
                var lis = item.Product_id;

            }

            if (User.Identity.IsAuthenticated)
            {
                //int Uid = 2;
                int Uid = Convert.ToInt32(Session["UserId"]);
                if (Uid != 0)
                {
                    List<Cart> Clist = new List<Cart>();
                    Clist = BlCart.Cart(Uid);
                    ViewBag.list = null;
                    Session["cartList"] = ViewBag.list;
                    if (Clist.Count() == 0)
                    {
                        ViewBag.empty = "Your Cart Is Empty";
                    }
                    else
                    {
                        ViewBag.list = Clist.ToList();
                        ViewBag.count = Clist.Count();
                    }

                }
                else
                {
                    FormsAuthentication.SignOut();
                    return RedirectToAction("../");
                }


            }
            else
            {
                ViewBag.empty = "Your Cart Is Empty";
            }

            //if (Clist.Count() == 0)
            //{     
            //      sunil882671@gmail.com
            //      Sunil@123
            //    ViewBag.empty = "Your Cart Is Empty";
            //}
            //else
            //{
            //    ViewBag.list = Clist.ToList();
            //    Session["cartList"] = ViewBag.list;
            //}

            //foreach (var item in ViewBag.list)
            //{
            //   var prive = item.p_price;
            //}
            //ViewBag.count = Clist.Count();
            //int i, j, k, n = 13, m = 13;
            //int[] a = { 11, 22, 55, 11, 33, 11, 22, 55, 55, 44, 11, 22, 44 };
            //int[] b = { 11, 22, 55, 11, 33, 11, 22, 55, 55, 44, 11, 22, 44 };






            //for (i = 0; i < n; i++)
            //{
            //    for (j = i + 1; j < n;)
            //    {
            //        int aj = a[j];
            //        int ai = a[i];
            //        if (a[j] == a[i])
            //        {
            //            for (k = j; k < n - 1; k++)
            //            {
            //                int ak = a[k];
            //                int ak1 = a[k + 1];
            //                a[k] = a[k + 1];
            //            }
            //            n--;
            //        }
            //        else
            //        {
            //            j++;

            //        }
            //    }
            //}

            //for (i = 0; i < n; i++)
            //{
            //    int show = a[i];
            //    int count = 0;

            //    for (j = 0; j < m; j++)
            //    {
            //        int old = b[j];
            //        if (a[i] == b[j])
            //        {
            //            count++;
            //        }

            //    }

            //    int tcount = count;
            //}
            // 11 = 4t
            //22 = 3t
            //33 = 3t
            //55 = 3t
            //44 = 2t
            return View(list);
        }

        [HttpPost]
        public ActionResult Index(int id, int Quantity)
        {
            int Uid = Convert.ToInt32(Session["UserId"]);
            int Product_id = id;
            double quntity = Quantity;
            if (Uid != 0)
            {
                bool test = BlCart.CheckIsValid(Uid, Product_id);
                if (test)
                {
                    ViewBag.alert = "Already Added in your Cart";
                }
                else
                {
                    BlCart.SaveNewProductInCart(Uid, Product_id, quntity);
                    ViewBag.alert = "Item Added in your Cart";




                }
                //  Testing
                List<newProduct> list = new List<newProduct>();
                list = Blproduct.NewProductGetItems();

                List<Cart> Clist = new List<Cart>();
                Clist = BlCart.Cart(Uid);
                ViewBag.list = null;
                Session["cartList"] = ViewBag.list;
                if (Clist.Count() == 0)
                {
                    ViewBag.empty = "Your Cart Is Empty";
                }
                else
                {
                    ViewBag.list = Clist.ToList();
                    ViewBag.count = Clist.Count();
                }

                return View(list);
                // End  Testing

                //return RedirectToAction("../");
                //return View();
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("/Login");
            }
        }

        [ActionName("SendMails")]
        //[HttpPost]
        public ActionResult Index(string name)
        {
            return RedirectToAction("Index");
        }

        public JsonResult get()
        {
            //List<newProduct> list = new List<newProduct>();
            //list = Blproduct.NewProductGetItems();
            int Uid = Convert.ToInt32(Session["UserId"]);
            if (Uid != 0)
            {
                List<Cart> Clist = new List<Cart>();
                Clist = BlCart.Cart(Uid);
                var js = JsonConvert.SerializeObject(Clist);
                return Json(js, JsonRequestBehavior.AllowGet);
            }
            else
            {
                FormsAuthentication.SignOut();
                //return RedirectToAction("../");
                return Json(null);

            }
        }

        public ActionResult Cart()
        {

            int Uid = Convert.ToInt32(Session["UserId"]);

            if (Uid != 0)
            {
                List<Cart> Clist = new List<Cart>();
                Clist = BlCart.Cart(Uid);

                if (Clist.Count() == 0)
                {
                    ViewBag.empty = "Your Cart Is Empty";
                }
                else
                {
                    ViewBag.li = Clist.ToList();
                }
                return View();
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("../");
            }



        }
        [HttpPost]
        public ActionResult Cart(int id, int Quantity)
        {
            int Uid = Convert.ToInt32(Session["UserId"]);
            int Product_id = id;
            int quntity = Quantity;
            if (Uid != 0)
            {
                BlCart.EditProductInCart(Uid, Product_id, quntity);
                return RedirectToAction("Cart");
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("../");
            }


        }

        [HttpPost]
        public ActionResult Remove(int id)
        {
            int Uid = Convert.ToInt32(Session["UserId"]);
            int Product_id = id;
            if (Uid != 0)
            {
                BlCart.RemoveProductFromCart(Uid, Product_id);
                return View("Index");
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("../");
            }
        }




        //public ActionResult OrderNow(FormCollection collection)
        public ActionResult OrderNow()
        {
            //var name = collection[0];
            int Uid = Convert.ToInt32(Session["UserId"]);
            if (Uid != 0)
            {
                BlCart.SaveNewOrder(Uid);
                return RedirectToAction("../");
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("../");
            }
        }

        public ActionResult OrderDetails()
        {
            int Uid = Convert.ToInt32(Session["UserId"]);
            if (Uid != 0)
            {
                List<OrderDetailObj> Od_list = new List<OrderDetailObj>();
                Od_list = Blproduct.GetOrderDetail(Uid);
                List<CheckOrd> Od_id = new List<CheckOrd>();
                Od_id = Blproduct.ChecMykOrd(Uid);
                ViewBag.list = Od_list.ToList();
                ViewBag.OrdList = Od_id.ToList();
                return View(Od_list);
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("../");
            }
        }

        public ActionResult CheckOrder()
        {
            //int Uid = Convert.ToInt32(Session["UserId"]);



            List<CheckOrd> Ord_list = new List<CheckOrd>();
            Ord_list = Blproduct.GetCheckOrd();
            var list = (from ord in Ord_list
                        where ord.Status == "Not Delevered"
                        select ord).ToList();


            return View(Ord_list);
        }


        public ActionResult DeshBoard()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult CheckOut()
        {

            int Uid = Convert.ToInt32(Session["UserId"]);

            if (Uid != 0)
            {
                List<Cart> Clist = new List<Cart>();
                Clist = BlCart.Cart(Uid);

                if (Clist.Count() == 0)
                {
                    ViewBag.empty = "Your Cart Is Empty";
                }
                else
                {
                    ViewBag.count = Clist.Count();
                    ViewBag.li = Clist.ToList();
                }
                return View();
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("../");
            }



            //return View();
        }

        public ActionResult SendMail(FormCollection collection)
        {

            try
            {

                //clsMail cust = new clsMail();
                //cust.Name = collection[0];
                //cust.Email = collection[1];
                //cust.PhoneNo = collection[2];
                //cust.Ord_Massage = collection[3];
                //cust.Address = collection[3];


                //MailMessage mm = new MailMessage("dhrohit256@gmail.com", "dhrohit256@gmail.com");
                ////mm.Subject = model.Subject;
                //mm.Subject = "Limbone Inquiry";
                //var msg = "This message is sent by: - " + cust.Name + ", Email Id : - " + cust.Email + " , his contact no is: - " + cust.PhoneNo + " , his Address is: - " + cust.Address + " Inquiry /Order : - '" + cust.Ord_Massage + "'";
                //mm.Body = msg;
                //mm.IsBodyHtml = true;

                //SmtpClient smtp = new SmtpClient();
                //smtp.Host = "smtp.gmail.com";
                //smtp.Port = 587;
                //smtp.EnableSsl = true;

                //NetworkCredential nc = new NetworkCredential("dhrohit256@gmail.com", "Rohit@123");
                //smtp.UseDefaultCredentials = true;
                //smtp.Credentials = nc;
                //smtp.Send(mm);

                //Enquiry enq = new Enquiry();
                //enq.Name = cust.Name;
                //enq.email_id = cust.Email;
                //enq.phone = cust.PhoneNo;
                //enq.message = cust.Massage;
                //enq.IsBlock = "No";
                //BLenquiry.AddEnquiry(enq);

                return View("Index");
            }
            catch (Exception)
            {

                return View("Index");
            }


        }


    }
}