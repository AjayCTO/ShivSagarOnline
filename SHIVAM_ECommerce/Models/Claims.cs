using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHIVAM_ECommerce.Models
{
    public class Claims:BaseClass
    {
        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
        
        public bool IsActive { get; set; }


    }
}