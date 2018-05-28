﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SHIVAM_ECommerce.Models;
using SHIVAM_ECommerce.Repository;
using SHIVAM_ECommerce.Extensions;
namespace SHIVAM_ECommerce.Controllers
{
    public class ProductAttributesController : Controller
    {
        private IRepository<ProductAttributes> _repository = null;
        private IRepository<ProductAttributesRelation> _ProductAttributeRelationrepository = null;

        public ProductAttributesController()
        {
            this._repository = new Repository<ProductAttributes>();
            this._ProductAttributeRelationrepository = new Repository<ProductAttributesRelation>();
        }
        // GET: /Category/
        public ActionResult Index()
        {
            return View(_repository.GetAll());
        }


        public JsonResult GetProductAttributes(int SupplierID)
        {
            var productAttributes = _ProductAttributeRelationrepository.GetAll().Where(x => x.SupplierID == SupplierID).Select(p => new { Name = p.ProductAttributes.AttributeName, Id = p.ProductAttributesId, Descp = p.ProductAttributes.AttributeDescription });
            return Json(productAttributes.ToList(), JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetAllAttributes()
        {
            var productAttributes = _repository.GetAll().Select(p => new { Name = p.AttributeName, Id = p.Id, Descp = p.AttributeDescription });
            return Json(productAttributes.ToList(), JsonRequestBehavior.AllowGet);

        }

      
        public ActionResult AddAttributesForSupplier()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SaveSupplierAttributes(List<ProductAttributesRelation> Model)
        {
            try
            {

                DeleteAllOldAttribute(Model.First().SupplierID);
                foreach (var item in Model.ToList())
                {
                    _ProductAttributeRelationrepository.Insert(item);

                }
                _ProductAttributeRelationrepository.Save();
                return Json(new { Success = true, ex = "", data = "" });
            }
            catch (Exception ex)
            {

                return Json(new { Success = false, ex = ex.Message.ToString(), data = "" });

            }
        }

        private void DeleteAllOldAttribute(int p)
        {
            try
            {
                _ProductAttributeRelationrepository.DeleteAttributeForSupplier(p);
            }
            catch (Exception ex)
            {

                throw;
            }
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

                v = v.Where(b => b.AttributeName.Contains(searchitem));
            }
            //SORT
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                // v = v.OrderBy(sortColumn + " " + sortColumnDir);
            }

            recordsTotal = v.Count();
            var data = v.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data.Select(x => new { x.Id, x.AttributeName }) }, JsonRequestBehavior.AllowGet);
        }




        // GET: /Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductAttributes PA = _repository.GetById(id);
            if (PA == null)
            {
                return HttpNotFound();
            }
            return View(PA);
        }

        // GET: /Category/Create
        public ActionResult Create()
        {
            return View(new ProductAttributes());
        }

        // POST: /Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AttributeName,AttributeDescription")] ProductAttributes PA)
        {

            if (ModelState.IsValid)
            {
                _repository.Insert(PA);
                _repository.Save();
                this.AddNotification("Created successfully.", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }

            return View(PA);
        }

        // GET: /Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductAttributes PA = _repository.GetById(id);
            if (PA == null)
            {
                return HttpNotFound();
            }
            return View(PA);
        }

        // POST: /Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AttributeName")] ProductAttributes PA)
        {

            if (ModelState.IsValid)
            {
                _repository.Update(PA);
                _repository.Save();
                this.AddNotification("Updated successfully.", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            return View(PA);
        }

        // GET: /Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductAttributes PA = _repository.GetById(id);
            if (PA == null)
            {
                return HttpNotFound();
            }
            return View(PA);
        }

        // POST: /Category/Delete/5

        public ActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            _repository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}