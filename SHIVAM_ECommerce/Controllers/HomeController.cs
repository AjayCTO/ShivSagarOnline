using System;
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
using SHIVAM_ECommerce.Attributes;

namespace SHIVAM_ECommerce.Controllers
{
    public class HomeController : Controller
    {


        private ApplicationDbContext db = new ApplicationDbContext();
        [CustomAuthorize]
        public ActionResult Index()
        {
            
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Error()
        {
            ViewBag.Message = "You are not authorized to access this page, please contact administrator.";

            return View();
        }

        public ActionResult PlanExpire()
        {
            ViewBag.Message = "You are selected plan has been expired, please contact administrator to update your plan.";

            return View();
        }


        [Authorize(Roles = "Admin")]
        [ClaimsAuthorize("Admin", "Contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Profile(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile([Bind(Include = "Id,CompanyName,FirstName,LastName,Title,Address1,Address2,City,State,PostalCode,Country,Phone,Email,URL,Logo,SupplierType,UserID,PlanID,PlanStartDate,PlanEndDate,UserName,Password,ParentSupplierID")] Supplier supplier, HttpPostedFileBase file)
        {
            //supplier.UserName = "testtestsetetsetst";
            //supplier.Password = "testtestteststeest";
            supplier.UpdatedDate = DateTime.Now;
            supplier.CreatedDate = DateTime.Now;
           
            
           if (ModelState.IsValid)
            {

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
                ViewBag.PlanID = new SelectList(db.Plans, "Id", "PlanName", supplier.PlanID);
                return View(supplier);
           }
            ViewBag.PlanID = new SelectList(db.Plans, "Id", "PlanName", supplier.PlanID);
            return View(supplier);
        }


    }
}