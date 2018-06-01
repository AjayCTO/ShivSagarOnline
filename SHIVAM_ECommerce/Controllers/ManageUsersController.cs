using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SHIVAM_ECommerce.Models;
using SHIVAM_ECommerce.Repository;
using SHIVAM_ECommerce.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SHIVAM_ECommerce.Extensions;
namespace SHIVAM_ECommerce.Controllers
{
    public class ManageUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IRepository<Claims> _ClaimsRepository = null;
        private IRepository<ApplicationUser> _UsersRepository = null;
        private IRepository<IdentityUserClaim> _UserClaimsRepository = null;
        private UserManager<ApplicationUser> UserManager = null;
        public ManageUsersController()
        {
            this._ClaimsRepository = new Repository<Claims>();
            this._UsersRepository = new Repository<ApplicationUser>();
            this._UserClaimsRepository = new Repository<IdentityUserClaim>();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        }



        [Authorize(Roles = "superadmin,Supplier")]
        //
        // GET: /Manage/
        public ActionResult Index()
        {
            var _users = db.Users.ToList();
            var _user = db.Users.FirstOrDefault();
            return View(_users);
        }
        public ActionResult EditRoles(string Id)
        {
            var _AllRoles = db.Roles.ToList();


            var _user = _UsersRepository.GetById(Id);
            var _newuser = new UserRoleViewModel() { UserId = Id, UserName = _user.UserName, Roles = new List<CustomRoles>() };
            var _countRoles = _user.Roles.Count();
            foreach (var item in _AllRoles)
            {
                var _cRole = _countRoles > 0 ? _user.Roles.Where(x => x.Role.Name.ToLower() == item.Name.ToLower()).FirstOrDefault() : null;
                _newuser.Roles.Add(new CustomRoles() { Role = item.Name, Id = item.Id, IsChecked = _cRole == null ? false : true });
            }
            return View(_newuser);
        }


        [HttpPost]
        public ActionResult EditRoles(UserRoleViewModel model)
        {
            try
            {
                var _user = UserManager.FindById(model.UserId);

                var _userRoles = db.Roles.ToList();
                foreach (var item in model.Roles)
                {

                    var _ExistingRole = _user.Roles.Where(x => x.Role.Name.ToLower() == item.Role.ToLower()).FirstOrDefault();
                    if (_ExistingRole == null && item.IsChecked == true)
                    {
                        UserManager.AddToRole(_user.Id, item.Role);
                    }

                    if (_ExistingRole != null && item.IsChecked == false)
                    {
                        UserManager.RemoveFromRole(_user.Id, item.Role);
                     
                    }


                }





                db.SaveChanges();
                this.AddNotification("Roles updated successfully.", NotificationType.SUCCESS);
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public ActionResult Edit(string Id)
        {
            var _AllClaims = _ClaimsRepository.GetAll();
            var _user = _UsersRepository.GetById(Id);
            var _newuser = new UserViewModel() { UserId = Id, UserName = _user.UserName, Claims = new List<CustomClaims>() };
            var _countClaims = _user.Claims.Count();
            foreach (var item in _AllClaims)
            {
                var _cClaim = _countClaims > 0 ? _user.Claims.Where(x => x.ClaimValue.ToLower() == item.ClaimValue.ToLower()).FirstOrDefault() : null;
                _newuser.Claims.Add(new CustomClaims() { ClaimValue = item.ClaimValue, ClaimType = item.ClaimType, Id = item.Id, IsChecked = _cClaim == null ? false : true });
            }
            return View(_newuser);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            try
            {
                var _userClaims = _UserClaimsRepository.GetAll().Where(x => x.User.Id == model.UserId).ToList();
                foreach (var item in model.Claims)
                {
                    var _user = UserManager.FindById(model.UserId);

                    var _ExistingClaim = _userClaims.Where(x => x.ClaimValue.ToLower() == item.ClaimValue.ToLower()).FirstOrDefault();
                    if (_ExistingClaim == null && item.IsChecked == true)
                    {
                        var _newClaim = new System.Security.Claims.Claim(item.ClaimType, item.ClaimValue);
                        UserManager.AddClaim(model.UserId, _newClaim);
                    }

                    if (_ExistingClaim != null && item.IsChecked == false)
                    {
                        var _newClaim = new System.Security.Claims.Claim(_ExistingClaim.ClaimType, _ExistingClaim.ClaimValue);
                        UserManager.RemoveClaim(model.UserId, _newClaim);

                    }

                }
                _UserClaimsRepository.Save();
                this.AddNotification("Claims updated successfully.", NotificationType.SUCCESS);
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}