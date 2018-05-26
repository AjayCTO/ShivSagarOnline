using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SHIVAM_ECommerce.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using SHIVAM_ECommerce.Attributes;

namespace SHIVAM_ECommerce.Controllers
{
    [CustomAuthorize(Roles="superadmin,Admin,Supplier")]
    public class SupplierUserController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /SupplierUser/
        public ActionResult Index()
        {
            var suppliers = db.Suppliers.Include(s => s.Plan).Include(s => s.User).Where(x => x.ParentSupplierID == CurrentUserData.SupplierID);
            return View(suppliers.ToList());
        }

        // GET: /SupplierUser/Details/5
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

        // GET: /SupplierUser/Create
        public ActionResult Create()
        {
            ViewBag.PlanID = new SelectList(db.Plans, "Id", "PlanName");
            //  ViewBag.UserID = new SelectList(db.IdentityUsers, "Id", "UserName");
            return View();
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        // POST: /SupplierUser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CompanyName,FirstName,LastName,Title,Address1,Address2,City,State,PostalCode,Country,Phone,Email,URL,Logo,SupplierType,ParentSupplierID,RegisteredByID,UserID,PlanID,UserName,Password")] Supplier supplier)
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
                supplier.CreatedDate = DateTime.Now;
                supplier.UpdatedDate = DateTime.Now;
                supplier.Sort = 33;
                supplier.ParentSupplierID = CurrentUserData.SupplierID;
                supplier.RegisteredByID = CurrentUserData.UserID;
                db.Suppliers.Add(supplier);
                var user = new ApplicationUser() { UserName = supplier.UserName, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now };
                IdentityResult result = await _controller.UserManager.CreateAsync(user, supplier.Password);
                if (result.Succeeded)
                {
                    supplier.UserID = user.Id;
                    db.SaveChanges();
                    _controller.UserManager.AddToRole(user.Id, "SupplierUser");
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrors(result);
                }

            }

            ViewBag.PlanID = new SelectList(db.Plans, "Id", "PlanName", supplier.PlanID);
            return View(supplier);
        }

        // GET: /SupplierUser/Edit/5
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
            return View(supplier);
        }

        // POST: /SupplierUser/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CompanyName,FirstName,LastName,Title,Address1,Address2,City,State,PostalCode,Country,Phone,Email,URL,Logo,SupplierType,ParentSupplierID,RegisteredByID,UserID,PlanID,CreatedDate,UpdatedDate,Sort,Description,Notes")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlanID = new SelectList(db.Plans, "Id", "PlanName", supplier.PlanID);
            return View(supplier);
        }

        // GET: /SupplierUser/Delete/5
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

        // POST: /SupplierUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
