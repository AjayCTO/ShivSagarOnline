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
using System.Collections;
using System.Web.Caching;

namespace SHIVAM_ECommerce.Controllers
{
    public class HomeController : BaseController
    {
        private void ManageCache()
        {
            if (HttpContext.Cache["UserClaims"] == null)
            {
                var userClaims = db.AspNetUserClaims.Where(x => x.User.Id == CurrentUserData.UserID).ToList();
                HttpContext.Cache["UserClaims"] = userClaims;

                var CanSeeCategoryURL = userClaims.FirstOrDefault(x => x.User.Id == CurrentUserData.UserID && x.ClaimValue == "URL:/Category" && x.IsActive == true);
                if (CanSeeCategoryURL != null)
                {
                    HttpContext.Cache["CanSeeCategoryURL"] = CanSeeCategoryURL.ClaimValue;
                }
                var CanSeeSupplierURL = userClaims.FirstOrDefault(x => x.User.Id == CurrentUserData.UserID && x.ClaimValue == "URL:/Supplier" && x.IsActive == true);
                if (CanSeeSupplierURL != null)
                {
                    HttpContext.Cache["CanSeeSupplierURL"] = CanSeeSupplierURL.ClaimValue;
                }
                var CanSeePlanURL = userClaims.FirstOrDefault(x => x.User.Id == CurrentUserData.UserID && x.ClaimValue == "URL:/Plan" && x.IsActive == true);
                if (CanSeePlanURL != null)
                {
                    HttpContext.Cache["CanSeePlanURL"] = CanSeePlanURL.ClaimValue;
                }
                var CanSeeManufacturerURL = userClaims.FirstOrDefault(x => x.User.Id == CurrentUserData.UserID && x.ClaimValue == "URL:/Manufacturers" && x.IsActive == true);
                if (CanSeeManufacturerURL != null)
                {
                    HttpContext.Cache["CanSeeManufacturerURL"] = CanSeeManufacturerURL.ClaimValue;
                }
                var CanSeeProductURL = userClaims.FirstOrDefault(x => x.User.Id == CurrentUserData.UserID && x.ClaimValue == "URL:/Product/GetAllProducts" && x.IsActive == true);
                if (CanSeeProductURL != null)
                {
                    HttpContext.Cache["CanSeeProductURL"] = CanSeeProductURL.ClaimValue;
                }
                var CanSeeProductStatusURL = userClaims.FirstOrDefault(x => x.User.Id == CurrentUserData.UserID && x.ClaimValue == "URL:/ProductStatus" && x.IsActive == true);
                if (CanSeeProductStatusURL != null)
                {
                    HttpContext.Cache["CanSeeProductStatusURL"] = CanSeeProductStatusURL.ClaimValue;
                }
                var CanSeeUOMURL = userClaims.FirstOrDefault(x => x.User.Id == CurrentUserData.UserID && x.ClaimValue == "URL:/UnitOfMeasures" && x.IsActive == true);
                if (CanSeeUOMURL != null)
                {
                    HttpContext.Cache["CanSeeUOMURL"] = CanSeeUOMURL.ClaimValue;
                }
                var CanSeeProductAttrURL = userClaims.FirstOrDefault(x => x.User.Id == CurrentUserData.UserID && x.ClaimValue == "URL:/ProductAttributes" && x.IsActive == true);
                if (CanSeeProductAttrURL != null)
                {
                    HttpContext.Cache["CanSeeProductAttrURL"] = CanSeeProductAttrURL.ClaimValue;
                }
                var CanSeeProductAttrSupplierURL = userClaims.FirstOrDefault(x => x.User.Id == CurrentUserData.UserID && x.ClaimValue == "URL:/ProductAttributes/AddAttributesForSupplier" && x.IsActive == true);
                if (CanSeeProductAttrSupplierURL != null)
                {
                    HttpContext.Cache["CanSeeProductAttrSupplierURL"] = CanSeeProductAttrSupplierURL.ClaimValue;
                }
                var CanSeeCustomerURL = userClaims.FirstOrDefault(x => x.User.Id == CurrentUserData.UserID && x.ClaimValue == "URL:/Customer" && x.IsActive == true);
                if (CanSeeCustomerURL != null)
                {
                    HttpContext.Cache["CanSeeCustomerURL"] = CanSeeCustomerURL.ClaimValue;
                }
                var CanSeeOrderURL = userClaims.FirstOrDefault(x => x.User.Id == CurrentUserData.UserID && x.ClaimValue == "URL:/Order" && x.IsActive == true);
                if (CanSeeOrderURL != null)
                {
                    HttpContext.Cache["CanSeeOrderURL"] = CanSeeOrderURL.ClaimValue;
                }
                var CanSeeEmailRecordURL = userClaims.FirstOrDefault(x => x.User.Id == CurrentUserData.UserID && x.ClaimValue == "URL:/EmailRecords" && x.IsActive == true);
                if (CanSeeEmailRecordURL != null)
                {
                    HttpContext.Cache["CanSeeEmailRecordURL"] = CanSeeEmailRecordURL.ClaimValue;
                }
                var CanSeeManageAccountURL = userClaims.FirstOrDefault(x => x.User.Id == CurrentUserData.UserID && x.ClaimValue == "URL:/Account/Manage" && x.IsActive == true);
                if (CanSeeManageAccountURL != null)
                {
                    HttpContext.Cache["CanSeeManageAccountURL"] = CanSeeManageAccountURL.ClaimValue;
                }
                var CanSeeSupplierUserURL = userClaims.FirstOrDefault(x => x.User.Id == CurrentUserData.UserID && x.ClaimValue == "URL:/SupplierUser" && x.IsActive == true);
                if (CanSeeSupplierUserURL != null)
                {
                    HttpContext.Cache["CanSeeSupplierUserURL"] = CanSeeSupplierUserURL.ClaimValue;
                }
            }     
        }



        private void ManageSession()
        {
            Session["CanSeeCategoryURL"] = HttpContext.Cache["CanSeeCategoryURL"] != null ? HttpContext.Cache["CanSeeCategoryURL"].ToString() : "";
            Session["CanSeeSupplierURL"] = HttpContext.Cache["CanSeeSupplierURL"] != null ? HttpContext.Cache["CanSeeSupplierURL"].ToString() : "";
            Session["CanSeePlanURL"] = HttpContext.Cache["CanSeePlanURL"] != null ? HttpContext.Cache["CanSeePlanURL"].ToString() : "";
            Session["CanSeeManufacturerURL"] = HttpContext.Cache["CanSeeManufacturerURL"] != null ? HttpContext.Cache["CanSeeManufacturerURL"].ToString() : "";
            Session["CanSeeProductURL"] = HttpContext.Cache["CanSeeProductURL"] != null ? HttpContext.Cache["CanSeeProductURL"].ToString() : "";
            Session["CanSeeProductStatusURL"] = HttpContext.Cache["CanSeeProductStatusURL"] != null ? HttpContext.Cache["CanSeeProductStatusURL"].ToString() : "";
            Session["CanSeeUOMURL"] = HttpContext.Cache["CanSeeUOMURL"] != null ? HttpContext.Cache["CanSeeUOMURL"].ToString() : "";
            Session["CanSeeProductAttrURL"] = HttpContext.Cache["CanSeeProductAttrURL"] != null ? HttpContext.Cache["CanSeeProductAttrURL"].ToString() : "";
            Session["CanSeeProductAttrSupplierURL"] = HttpContext.Cache["CanSeeProductAttrSupplierURL"] != null ? HttpContext.Cache["CanSeeProductAttrSupplierURL"].ToString() : "";
            Session["CanSeeCustomerURL"] = HttpContext.Cache["CanSeeCustomerURL"] != null ? HttpContext.Cache["CanSeeCustomerURL"].ToString() : "";
            Session["CanSeeOrderURL"] = HttpContext.Cache["CanSeeOrderURL"] != null ? HttpContext.Cache["CanSeeOrderURL"].ToString() : "";
            Session["CanSeeEmailRecordURL"] = HttpContext.Cache["CanSeeEmailRecordURL"] != null ? HttpContext.Cache["CanSeeEmailRecordURL"].ToString() : "";
            Session["CanSeeManageAccountURL"] = HttpContext.Cache["CanSeeManageAccountURL"] != null ? HttpContext.Cache["CanSeeManageAccountURL"].ToString() : "";
            Session["CanSeeSupplierUserURL"] = HttpContext.Cache["CanSeeSupplierUserURL"] != null ? HttpContext.Cache["CanSeeSupplierUserURL"].ToString() : "";
        }


        private void RemoveCache()
        {
            List<string> keys = new List<string>();

            Cache cache = new Cache();
            IDictionaryEnumerator enumerator = cache.GetEnumerator();

            while (enumerator.MoveNext())
                keys.Add(enumerator.Key.ToString());

            for (int i = 0; i < keys.Count; i++)
                cache.Remove(keys[i]);
        }

        private ApplicationDbContext db = new ApplicationDbContext();
        [CustomAuthorize]
        public ActionResult Index()
        {


            RemoveCache();

            ManageCache();

            ManageSession();

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
        public ActionResult Profile([Bind(Include = "Id,CompanyName,FirstName,LastName,Title,Address1,Address2,City,State,PostalCode,Country,Phone,Email,URL,Logo,SupplierType,UserID,PlanID,PlanStartDate,PlanEndDate,UserName,Password,ParentSupplierID,RegisteredByID")] Supplier supplier, HttpPostedFileBase file)
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