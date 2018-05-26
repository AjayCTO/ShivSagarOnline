using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SHIVAM_ECommerce.Models;
using System.Linq.Dynamic;
using SHIVAM_ECommerce.Attributes;
namespace SHIVAM_ECommerce.Controllers
{
    [CustomAuthorize]
    public class PlanController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Plan/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult LoadData()
        {

            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var searchitem = Request["search[value]"];
            int _searchInt = -1;
            if (int.TryParse(searchitem, out _searchInt))
            {
                _searchInt = int.Parse(searchitem);
            }
            else
            {
                _searchInt = -1;
            }
            //Find Order Column
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();


            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var v = (from a in db.Plans select a);
            if (!string.IsNullOrEmpty(searchitem))
            {

                v = v.Where(b => b.Plancode.Contains(searchitem) || b.PlanName.Contains(searchitem) || b.Description.Contains(searchitem) || b.MonthlyPrice == _searchInt || b.YearlyPrice == _searchInt || b.ProductBucketCount == _searchInt || b.UserBucketCount == _searchInt);
            }
            //SORT
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                v = v.OrderBy(sortColumn + " " + sortColumnDir);
            }

            recordsTotal = v.Count();
            var data = v.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data.Select(x => new { x.Id, x.PlanName, PlanFrequency = x.PlanFrequency == "1" ? "Monthly" : "Yearly", x.Plancode, x.ProductBucketCount, x.UserBucketCount, x.Description, x.IsActive, x.MonthlyPrice, x.YearlyPrice }) }, JsonRequestBehavior.AllowGet);
        }


        // GET: /Plan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plans plans = db.Plans.Find(id);
            if (plans == null)
            {
                return HttpNotFound();
            }
            return View(plans);
        }

        [HttpPost]
        public ActionResult TogglePlan(int ID)
        {
            try
            {
                var _plan = db.Plans.Where(x => x.Id == ID).FirstOrDefault();
                _plan.IsActive = !_plan.IsActive;

                db.SaveChanges();
                return Json(new { Success = true, ex = "" });

            }
            catch (Exception ex)
            {
                return Json(new { Success = false, ex = ex.Message.ToString() });
            }
        }

        // GET: /Plan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Plan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PlanName,Plancode,CreatedDate,UpdatedDate,Sort,Description,Notes,PlanFrequency,MonthlyPrice,YearlyPrice,ProductBucketCount,UserBucketCount,IsActive")] Plans plans)
        {
            if (ModelState.IsValid)
            {
                plans.UpdatedDate = DateTime.Now;
                plans.CreatedDate = DateTime.Now;
                plans.IsActive = true;
                db.Plans.Add(plans);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(plans);
        }

        // GET: /Plan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plans plans = db.Plans.Find(id);
            if (plans == null)
            {
                return HttpNotFound();
            }
            return View(plans);
        }

        // POST: /Plan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PlanName,Plancode,CreatedDate,UpdatedDate,Sort,Description,Notes,PlanFrequency,MonthlyPrice,YearlyPrice,ProductBucketCount,UserBucketCount,IsActive")] Plans plans)
        {
            if (ModelState.IsValid)
            {
                plans.UpdatedDate = DateTime.Now;
                db.Entry(plans).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(plans);
        }

        // GET: /Plan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plans plans = db.Plans.Find(id);
            if (plans == null)
            {
                return HttpNotFound();
            }
            return View(plans);
        }

        // POST: /Plan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Plans plans = db.Plans.Find(id);
            db.Plans.Remove(plans);
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
