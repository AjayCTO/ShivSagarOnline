using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SHIVAM_ECommerce.ViewModels
{
    public class CustomClaims
    {
        public int Id { get; set; }
        public bool IsChecked { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

    }
}
