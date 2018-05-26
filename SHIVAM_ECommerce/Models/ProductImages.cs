using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SHIVAM_ECommerce.Models
{
    public class ProductImages:BaseClass    
    {


        public string ImageName { get; set; }
        public string ImagePath { get; set; }

        public string Title { get; set; }
        //public int ProductId { get; set; }
        public int ProductQuantityId { get; set; }
        

        //[ForeignKey("ProductId")]
        //public virtual Product Product { get; set; }

        [ForeignKey("ProductQuantityId")]
        public virtual ProductAttributeWithQuantity ProductWithQuantity { get; set; }
    }
}