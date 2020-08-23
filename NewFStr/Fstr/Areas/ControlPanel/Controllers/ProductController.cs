using BL;
using MODALobj;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fstr.Areas.ControlPanel.Controllers
{
    public class ProductController : Controller
    {
        // GET: ControlPanel/Product
        public ActionResult Index()
        {
            List<newProduct> list = new List<newProduct>();
            list = Blproduct.NewProductGetItems();

            return View(list);
        }

        // GET: ControlPanel/Product/Details/5
        public ActionResult Details(int id)
        {
            newProduct Prod = Blproduct.GetDetails(id).First();
           
            return View(Prod);
        }

        // GET: ControlPanel/Product/Create
        public ActionResult Create()
        {

            ViewData["Category"] = getCategory();
            return View();
        }

        // POST: ControlPanel/Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, HttpPostedFileBase file)
        {
            try
            {
                // TODO: Add insert logic here
                newProduct create = new newProduct();
                create.p_Ename = collection[1];
                //create.p_hname = collection[2];
                create.p_price = Convert.ToInt32(collection[3]);
                create.p_weight_type = collection[4];
                create.p_imge = file.FileName.ToString();
                create.Category = collection[5];
                Blproduct.Add(create);
                return RedirectToAction("Index");
            }
            catch
            {
                //List<newProduct> list = new List<newProduct>();

                //list = Blproduct.NewProductGetItems();
                //var list1 = (from v in list where v.Product_id == id select v).ToList();
                ViewData["Category"] = getCategory();

                return View();
            }
        }

        // GET: ControlPanel/Product/Edit/5
        public ActionResult Edit(int id)
        {
            newProduct Prod = Blproduct.GetDetails(id).First();
            TempData["fileimg"] = Prod.p_imge;
            TempData["Category"] = Prod.Category;
            ViewData["Category"] = getCategory();
            return View(Prod);
        }



        // POST: ControlPanel/Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection,HttpPostedFileBase file)
        {
            try
            {
                newProduct update = new newProduct();
                update.Product_id = id;
                update.p_Ename = collection["p_Ename"];
                //update.p_hname = uploadImg(file);
                update.p_imge = uploadImg(file);
                update.p_price = Convert.ToDouble(collection["p_price"]);
                update.p_weight_type = collection["p_weight_type"];
                update.Category = collection["Category"];

                Blproduct.edit(update);
                return RedirectToAction("Index");
            }
            catch
            {
                newProduct Prod = Blproduct.GetDetails(id).First();
                TempData["Category"] = Prod.Category;
                ViewData["Category"] = getCategory();
                return View(Prod);
            }
        }

        // GET: ControlPanel/Product/Delete/5
        public ActionResult Toggle(int id)
        {
            newProduct Prod = Blproduct.GetDetails(id).First();
            return View(Prod);
        }

        // POST: ControlPanel/Product/Delete/5
        [HttpPost]
        public ActionResult Toggle(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                //newProduct Prod = Blproduct.deleteOne(id).First();
                int Prod = Blproduct.deleteOne(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private List<SelectListItem> getCategory()
        {

            var temp = TempData["Category"];
            List<SelectListItem> Category = new List<SelectListItem>();
            var dt = BlCategory.getDataTable("select cid,c_name from Category");
            foreach (System.Data.DataRow item in dt.Rows)
            {
                if (temp == null)
                {
                    Category.Add(new SelectListItem() { Text = item["c_Name"].ToString(), Value = item["cid"].ToString() });
                }
                else
                {
                    if (temp.Equals(item["c_Name"]))
                    {
                        Category.Add(new SelectListItem() { Selected = true, Text = item["c_Name"].ToString(), Value = item["cid"].ToString() });
                    }
                    else
                    {
                        Category.Add(new SelectListItem() { Text = item["c_Name"].ToString(), Value = item["cid"].ToString() });
                    }
                }

            }
            return Category;
        }

        public string uploadImg(HttpPostedFileBase file)
        {
            Random r = new Random();
            string fpath = "Failed";
            int random = r.Next();
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        fpath = Path.Combine(Server.MapPath("~/assets/images"), random + "_" + Path.GetFileName(file.FileName));
                        file.SaveAs(fpath);
                        fpath = "~/image" + random + "_" + Path.GetFileName(file.FileName);
                        //    ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        fpath = "Failed";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please select a file'); </script>");
                fpath = "Failed";
            }
            return fpath;
        }









    }
}
