using ShivamEComFace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShivamEComFace.Controllers
{
    public class ProductsController : ApiController
    {
        SHIVAMECommerceDBEntities db = new SHIVAMECommerceDBEntities();
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
