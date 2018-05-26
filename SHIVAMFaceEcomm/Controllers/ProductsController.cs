using SHIVAMFaceEcomm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SHIVAMFaceEcomm.Controllers
{
    public class ProductsController : ApiController
    {
        SHIVAMECommerceDBNewEntities db = new SHIVAMECommerceDBNewEntities();
        // GET api/products
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/products/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/products
        public void Post([FromBody]string value)
        {
        }

        // PUT api/products/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/products/5
        public void Delete(int id)
        {
        }

        public HttpResponseMessage GetCategories()
        {
           // var result = db.Categories.Where(p => p.IsActive == true).Select(p => new CategoryViewModel() {CategoryName=p.CategoryName,CategoryImage=p.CategoryImage,Id=p.Id,Categories1=p.Categories1.Select(x=>new CategoryViewModel() {CategoryName=x.CategoryName,CategoryImage=x.CategoryImage,Id=x.Id }).ToList() }).ToList();

            var result = db.Categories.Where(y => y.ParentCategory==null)
                                      .SelectMany(x => x.Categories1).ToList();

            var nresult = result.Select(c => new CategoryViewModel()
                {

                    CategoryName = c.Category1.CategoryName,
                    CategoryImage = c.Category1.CategoryImage,
                    Id = c.Category1.Id,
                    Categories1 = GetChildren(c.Category1.Categories1.ToList(), c.Id)
                });
            return Request.CreateResponse(HttpStatusCode.OK, nresult.ToList());
        }

        public HttpResponseMessage GetWishList(Guid CustomerId)
        {
            // var result = db.Categories.Where(p => p.IsActive == true).Select(p => new CategoryViewModel() {CategoryName=p.CategoryName,CategoryImage=p.CategoryImage,Id=p.Id,Categories1=p.Categories1.Select(x=>new CategoryViewModel() {CategoryName=x.CategoryName,CategoryImage=x.CategoryImage,Id=x.Id }).ToList() }).ToList();
            var userCust = CustomerId.ToString();
            var result = db.WishLists.Include("Product").Include("Customer").Where(p => p.Customer.UserID == userCust);

            var nresult = result.Select(c => new WishListItems()
            {
                Customer=c.Customer,
                CustomerId=c.CustomerId,
                Id=c.Id,
                Product=c.Product,
                ProductId=c.ProductId
              
            });
            return Request.CreateResponse(HttpStatusCode.OK, nresult.ToList());
        }
        public HttpResponseMessage AddToWishList(int CustomerId,int ProductId)
        {
            // var result = db.Categories.Where(p => p.IsActive == true).Select(p => new CategoryViewModel() {CategoryName=p.CategoryName,CategoryImage=p.CategoryImage,Id=p.Id,Categories1=p.Categories1.Select(x=>new CategoryViewModel() {CategoryName=x.CategoryName,CategoryImage=x.CategoryImage,Id=x.Id }).ToList() }).ToList();
            WishList newWishList = new WishList();
            newWishList.ProductId = ProductId;
            newWishList.CustomerId = CustomerId;
            db.WishLists.Add(newWishList);
            db.SaveChanges();
              
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }
       

        private static List<CategoryViewModel> GetChildren(List<Category> categories,int p)
        {
            return categories
                    .Select(c => new CategoryViewModel
                    {
                        CategoryName = c.CategoryName,
                        CategoryImage = c.CategoryImage,
                        Id =c.Id,
                        Categories1 = GetChildren(c.Categories1.ToList(), c.Id)
                    })
                    .ToList();
        }
        
        public HttpResponseMessage GetAttributes()
        {

            // Filling the list with data here...
            var result = db.ProductAttributes.Select(p => new { AttributeName = p.AttributeName, Id=p.Id });

            // Then I return the list
            return Request.CreateResponse(HttpStatusCode.OK, result.ToList());
        }

        public HttpResponseMessage GetAttributesValue()
        {

            // Filling the list with data here...
            var result = db.ProductAttribute_view.Select(p => new {AttributeId=p.AttributeName,AttributeValue=p.AttributeValue });

            // Then I return the list
            return Request.CreateResponse(HttpStatusCode.OK, result.ToList());
        }
       
    }
}
