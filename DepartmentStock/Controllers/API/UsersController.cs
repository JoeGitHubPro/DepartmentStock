using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DepartmentStock;
using DepartmentStock.SystemsApproaches.AuthenticationSystem;
using DepartmentStock.Models;
using static DepartmentStock.Models.UserModels;

namespace DepartmentStock.Controllers.API
{
    [RoutePrefix("api/Users")]
    [Authorize(Roles = RoleNameModels.Admin)]
    public class UsersController : ApiController
    {
        private DepartmentStockEntities db = new DepartmentStockEntities();

        [HttpGet]
        [Route("NoUsers")]
        public IHttpActionResult GetNoUsers()
        {
            // get the user manager from the owin context
            ApplicationUserManager userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

            List<AspNetUser> User = new List<AspNetUser>();
            foreach (var item in db.AspNetUsers)
            {
                string Role = userManager.GetRoles(item.Id).SingleOrDefault();
                if (Role == RoleNameModels.User)
                {
                    User.Add(item);
                }
            }
            int No = User.Count();
            User.Clear();
            userManager.Dispose();

            return Ok(No);
        }

        [HttpGet]
        [Route("NoManger")]
        public IHttpActionResult GetNoManger()
        {
            // get the user manager from the owin context
            ApplicationUserManager userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

            List<AspNetUser> User = new List<AspNetUser>();
            foreach (var item in db.AspNetUsers)
            {
                string Role = userManager.GetRoles(item.Id).SingleOrDefault();
                if (Role == RoleNameModels.Manger)
                {
                    User.Add(item);
                }
            }
            int No = User.Count();
            User.Clear();
            userManager.Dispose();

            return Ok(No);
        }

        [HttpGet]
        [Route("NoAdmin")]
        public IHttpActionResult GetNoAdmin()
        {
            // get the user manager from the owin context
            ApplicationUserManager userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

            List<AspNetUser> User = new List<AspNetUser>();
            foreach (var item in db.AspNetUsers)
            {
                string Role = userManager.GetRoles(item.Id).SingleOrDefault();
                if (Role == RoleNameModels.Admin)
                {
                    User.Add(item);
                }
            }
            int No = User.Count();
            User.Clear();
            userManager.Dispose();

            return Ok(No);
        }

        [HttpGet]
        [Route("NoOfAllUsers")]
        public IHttpActionResult GetAllUsers()
        {
            // get the user manager from the owin context
            ApplicationUserManager userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

            List<AspNetUser> User = new List<AspNetUser>();
            foreach (var item in db.AspNetUsers)
            {
                string Role = userManager.GetRoles(item.Id).SingleOrDefault();
                User.Add(item);

            }
            int No = User.Count();
            User.Clear();

            return Ok(No);
        }

        [HttpPost]
        [Route("FullAccountUsers")]
        public IHttpActionResult GetAccUsers(UserModels model)
        {
            // get the user manager from the owin context
            ApplicationUserManager userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

            List<AspNetUser> User = new List<AspNetUser>();
            foreach (var item in db.AspNetUsers)
            {
                string Role = userManager.GetRoles(item.Id).SingleOrDefault();
                if (Role == model.RoleName)
                {
                    User.Add(item);
                }


            }


            return Ok(User.Select(a => new { a.Id, a.UserName, a.Email, a.PhoneNumber }));
        }

        // GET: api/Users
        [Route("AllAccountUsers")]
        public IHttpActionResult GetAspNetUsers()
        {



            return Ok(db.AspNetUsers.Select(e => new
            {
                Id = e.Id,
                UserName = e.UserName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                Degree = e.Degree,
                Name = e.Name,

                RoleNameModels = e.AspNetRoles.Select(a => a.Name).FirstOrDefault()
            }));
        }

        // GET: api/Users/5
        [HttpGet]
        [Route("SingleAccountUser")]
        [ResponseType(typeof(AspNetUser))]
        public IHttpActionResult GetAspNetUser(string Id)
        {


            var aspNetUser = db.AspNetUsers.Where(e => e.Id == Id).Select(e => new
            {
                Id = e.Id,
                UserName = e.UserName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                Degree = e.Degree,
                Name = e.Name,
                RoleNameModels = e.AspNetRoles.Select(a => a.Name).FirstOrDefault()
            }).FirstOrDefault();

            if (aspNetUser == null)
            {
                return NotFound();
            }

            return Ok(aspNetUser);
        }

        [Route("EditAccountUsers")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAspNetUserModel(UserModels model)
        {
            AspNetUser aspNetUser = db.AspNetUsers.Find(model.Id);

            aspNetUser.UserName = model.UserName;
            aspNetUser.Email = model.Email;
            aspNetUser.PhoneNumber = model.PhoneNumber;
            aspNetUser.Degree = model.Degree;

            aspNetUser.Name = model.Name;


            if (model.Id != aspNetUser.Id)
            {
                return BadRequest();
            }

            db.Entry(aspNetUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetUserExists(model.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Users/5
        [Route("DeleteAccountUsers")]
        [ResponseType(typeof(AspNetUser))]
        public IHttpActionResult DeleteAspNetUser(UserModels model)
        {
            AspNetUser aspNetUser = db.AspNetUsers.Find(model.Id);
            if (aspNetUser == null)
            {
                return NotFound();
            }

            db.AspNetUsers.Remove(aspNetUser);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.Accepted);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AspNetUserExists(string id)
        {
            return db.AspNetUsers.Count(e => e.Id == id) > 0;
        }
    }
}