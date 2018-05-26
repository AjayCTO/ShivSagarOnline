using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHIVAM_ECommerce.ViewModels
{
    public class UserViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }
        public List<CustomClaims> Claims { get; set; }
    }

    public class UserRoleViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }
        public List<CustomRoles> Roles { get; set; }
    }
}