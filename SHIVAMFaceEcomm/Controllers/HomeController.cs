using SHIVAMFaceEcomm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHIVAMFaceEcomm.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        SHIVAMECommerceDBNewEntities db = new SHIVAMECommerceDBNewEntities();
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult AddNewCustomer()
        {
            return View();
        }

        public ActionResult Products()
        {

            return View();
        }
        public ActionResult MyProducts()
        {

            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult DeleteWishList(int id)
        {


            try
            {
                WishList wishList = db.WishLists.Find(id);
                db.WishLists.Remove(wishList);
                db.SaveChanges();
                return Json(new { Success = true, Ex = "", ID = wishList.Id }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {

                return Json(new { Success = false, Ex = Ex.Message.ToString(), ID = -1 }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult CustomerWishList(Guid CustomerId)
        {
            return View(CustomerId);
        }
        public ActionResult ProductDetail(int ProductId)
        {
            ProductDetail productDetail = new ProductDetail();
            productDetail.Product = new Product();
            var _ProductData = db.ProductAttributeWithQuantities.Where(p => p.Id == ProductId).FirstOrDefault();
            productDetail.Product = db.Products.Where(p => p.Id == _ProductData.ProductId).FirstOrDefault();
            productDetail.Attributes = new List<ProductDetailAttributes>();
            productDetail.Attributes = db.ProductValues_view.Where(p => p.productId == ProductId).Select(p => new ProductDetailAttributes() { AttributeName = p.AttributeName, AttributeValue = p.AttributeValue, Cost = p.Cost, ImageName = p.ImageName, ImagePath = p.ImagePath, Quantity = p.Quantity,ProductQuantityId=p.ProductQuantityID }).ToList();
            return View(productDetail);
        }

    }
}
