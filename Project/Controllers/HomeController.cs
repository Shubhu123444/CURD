using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;


namespace Project.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        VenderContext db = new VenderContext();
        public ActionResult Index(string SortOrder, string SortBy, int PageNumber = 1)
        {
            ViewBag.SortOrder = SortOrder;
            ViewBag.SortBy = SortBy;
            var gv = db.venders.ToList();


            switch (SortOrder)
            {
                case "Asc":
                    {
                        gv = gv.OrderBy(x => x.Organization).ToList();
                        break;
                    }
                case "Desc":
                    {
                        gv = gv.OrderByDescending(x => x.Organization).ToList();
                        break;
                    }
                default:
                    {
                        gv = gv.OrderBy(x => x.Organization).ToList();
                        break;

                    }
            }

            ViewBag.TotalPages = Math.Ceiling(gv.Count() / 5.0);

            gv = gv.Skip((PageNumber - 1) * 5).Take(5).ToList();
            return View(gv);
        }

     
        [HttpPost]
        public ActionResult Index(string searchTxt)
        {
            var gv = db.venders.ToList();
            if (searchTxt != null)
            {
                gv = db.venders.Where(x => x.Organization.Contains(searchTxt)).ToList();
                
            }
            return View(gv);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Vender model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new VenderContext())
                {


                    db.venders.Add(model);

                    db.SaveChanges();
                    ViewBag.issucess = "Data Added";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }




        public ActionResult Delete(int Id)
        {
            var VenderIdRow = db.venders.Where(model => model.Id == Id).FirstOrDefault();
            db.Entry(VenderIdRow).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //[HttpPost]
        //public ActionResult Delete(model v)
        //{
        //    db.Entry(v).State = System.Data.Entity.EntityState.Deleted;
        //    int a = db.SaveChanges();
        //    if(a > 0)
        //    {
        //        ViewBag.DeleteMsg = "<script>alert('Data Deleted !!')</script>";
        //    }
        //    else
        //    {
        //        ViewBag.DeleteMsg = "<script>alert('Data Not Deleted !!')</script>";
        //    }
        //}

        public JsonResult GetVenders(string term)
        {
            List<string> ven;
            ven = db.venders.Where(x => x.Organization.Contains(term)).Select(y => y.Organization).ToList();
            return Json(ven, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCountry(string term)
        {
            List<string> ven;
            ven = db.venders.Where(x => x.Country.Contains(term)).Select(y => y.Country).Distinct().ToList();
            return Json(ven, JsonRequestBehavior.AllowGet);
        }
    }
}