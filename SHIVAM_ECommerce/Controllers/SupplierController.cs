﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SHIVAM_ECommerce.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using ExportImplementation;
using System.Diagnostics;
using System.IO;
using SHIVAM_ECommerce.Repository;
using System.Configuration;
using SHIVAM_ECommerce.Extensions;
using SHIVAM_ECommerce.Attributes;

namespace SHIVAM_ECommerce.Controllers
{

    // [Authorize(Roles = "superadmin,Admin")]
    public class SupplierController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();



        private IRepository<Supplier> _repository = null;
        public SupplierController()
        {
            this._repository = new Repository<Supplier>();
        }


        [CustomAuthorize()]
        // GET: /Supplier/
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
            //Find Order Column
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();


            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            // dc.Configuration.LazyLoadingEnabled = false; // if your table is relational, contain foreign key
            var v = (from a in _repository.GetAll() select a);
            if (!string.IsNullOrEmpty(searchitem))
            {

                v = v.Where(b => b.CompanyName.Contains(searchitem));
            }
            //SORT
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                // v = v.OrderBy(sortColumn + " " + sortColumnDir);
            }

            recordsTotal = v.Count();
            var data = v.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data.Select(x => new { x.Id, x.CompanyName, x.FirstName, x.LastName, x.Logo, x.Phone, x.City }) }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult AllSupplier()
        {
            var suppliers = db.Suppliers.Include(s => s.Plan).Include(s => s.User).Select(p => new { Name = p.FirstName + p.LastName, Id = p.Id }); ;
            return Json(suppliers.ToList(), JsonRequestBehavior.AllowGet);

        }

        // GET: /Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: /Supplier/Create
        [AllowAnonymous]
        public ActionResult Create()
        {

            var allplans = db.Plans.ToList();
            ViewBag.allplans = allplans;
            return View(new Supplier());
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        // POST: /Supplier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CompanyName,FirstName,LastName,Title,Address1,Address2,City,State,PostalCode,Country,Phone,Email,URL,Logo,SupplierType,UserID,PlanID,UserName,Password")] Supplier supplier, HttpPostedFileBase file)
        {
            var _controller = new AccountController();

            if (!string.IsNullOrEmpty(supplier.UserName))
            {

                var _user = _controller.UserManager.FindByNameAsync(supplier.UserName);
                if (_user.Result != null)
                {
                    ModelState.AddModelError("Already Exist", "User already exist please provide different user name");
                }
            }
            if (ModelState.IsValid)
            {
                var _plan = db.Plans.Where(x => x.Id == supplier.PlanID).FirstOrDefault();

                supplier.CreatedDate = DateTime.Now;
                supplier.UpdatedDate = DateTime.Now;
                supplier.Sort = 33;
                supplier.ParentSupplierID = 0;
                supplier.ProductCount = _plan.ProductBucketCount;
                supplier.PlanStartDate = DateTime.Now;
                supplier.PlanEndDate = DateTime.Now.AddDays(_plan.PlanFrequency == "1" ? 30 : 365);
                supplier.UserCount = _plan.UserBucketCount;

                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/SupplierImage"), pic);
                    // file is uploaded
                    file.SaveAs(path);

                    supplier.Logo = pic;

                    // save the image path path to the database or you can send image 
                    // directly to database
                    // in-case if you want to store byte[] ie. for DB
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                }



                db.Suppliers.Add(supplier);
                var user = new ApplicationUser() { UserName = supplier.UserName, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now };
                IdentityResult result = await _controller.UserManager.CreateAsync(user, supplier.Password);
                if (result.Succeeded)
                {
                    supplier.UserID = user.Id;
                    db.SaveChanges();
                    _controller.UserManager.AddToRole(user.Id, "Supplier");
                    string email = supplier.Email;
                    string username = supplier.UserName;
                    string adminemail = ConfigurationManager.AppSettings["adminemail"].ToString();
                    string admintemple = ConfigurationManager.AppSettings["admintemple"].ToString();
                    string subject = ConfigurationManager.AppSettings["subject"].ToString();
                    string AdminUserName = ConfigurationManager.AppSettings["adminusername"].ToString();
                    var sendemail = new EmailService.Service.EmailService();
                    sendemail.SendEmail(email, "ForAdmin.html", "verfication", username);
                    sendemail.SendEmail(adminemail, admintemple, subject, AdminUserName);
                    this.AddNotification("Created successfully.", NotificationType.SUCCESS);
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrors(result);
                }

            }


            ViewBag.PlanID = new SelectList(db.Plans, "Id", "PlanName", supplier.PlanID);
            var allplans = db.Plans.ToList();
            ViewBag.allplans = allplans;
            return View(supplier);
            return View(supplier);
        }


        [HttpPost]
        public ActionResult UniqueUserName(string UserName)
        {
            try
            {
                var _controller = new AccountController();
                var _user = _controller.UserManager.FindByName(UserName);
                if (_user != null)
                {
                    return Json(new { Success = true, ex = "", IsAlreadyExist = true });
                }
                return Json(new { Success = true, ex = "", IsAlreadyExist = false });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, ex = ex.Message.ToString(), IsAlreadyExist = false });
            }
        }
        // GET: /Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlanID = new SelectList(db.Plans, "Id", "PlanName", supplier.PlanID);
            // ViewBag.UserID = new SelectList(db.IdentityUsers, "Id", "UserName", supplier.UserID);
            return View(supplier);
        }


        public FileContentResult DownloadAsExcel()
        {
            var listWithPerson = db.Suppliers.Include(s => s.Plan).Include(s => s.User).ToList();
            var export = new ExportExcel2007<Supplier>();
            var data = export.ExportResult(listWithPerson);
            var _path = Server.MapPath("~/DownloadedFiles/a.xlsx");
            System.IO.File.WriteAllBytes(_path, data);
            Process.Start(_path);

            return null;
        }

        // POST: /Supplier/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CompanyName,FirstName,LastName,Title,Address1,Address2,City,State,PostalCode,Country,Phone,Email,URL,Logo,SupplierType,UserID,PlanID,UserName,Password,PlanEndDate,PlanStartDate,ProductCount,CreatedDate,UserCount")] Supplier supplier, HttpPostedFileBase file)
        {


            if (ModelState.IsValid)
            {

                supplier.UpdatedDate = DateTime.Now;


                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/SupplierImage"), pic);
                    // file is uploaded
                    file.SaveAs(path);

                    supplier.Logo = pic;

                    // save the image path path to the database or you can send image 
                    // directly to database
                    // in-case if you want to store byte[] ie. for DB
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                }

                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
                this.AddNotification("Updated successfully.", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            ViewBag.PlanID = new SelectList(db.Plans, "Id", "PlanName", supplier.PlanID);
            return View(supplier);
        }

        // GET: /Supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: /Supplier/Delete/5

        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            db.Suppliers.Remove(supplier);
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