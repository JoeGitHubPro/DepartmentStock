using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DepartmentStock;
using DepartmentStock.Models;

namespace DepartmentStock.Controllers.API
{
    [RoutePrefix("api")]
    [Authorize(Roles = RoleNameModels.Admin)]  
    public class DeviceCategoriesController : ApiController
    {
        private DepartmentStockEntities db = new DepartmentStockEntities();

     
        [HttpGet]
        [Route("AllDeviceCategories")]
        public IHttpActionResult GetDeviceCategories()
        {
            return Ok( db.DeviceCategories.Select(a=> new {a.CategoryID, a.CategoryName, a.CategoryDescription}));
        }

        
        [HttpGet]
        [Route("SingleDeviceCategory")]
        [ResponseType(typeof(DeviceCategory))]
        public IHttpActionResult GetDeviceCategory(int CategoryID)
        {
            var deviceCategory = db.DeviceCategories.Where(a=>a.CategoryID==CategoryID).Select(a => new { a.CategoryID, a.CategoryName, a.CategoryDescription }).FirstOrDefault();
            if (deviceCategory == null)
            {
                return NotFound();
            }

            return Ok(deviceCategory);
        }

       
        [HttpPut]
        [Route("EditDeviceCategory")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDeviceCategory(DeviceCategory model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(model).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceCategoryExists(model.CategoryID))
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

       
        [HttpPost]
        [Route("AddDeviceCategory")]
        [ResponseType(typeof(DeviceCategory))]
        public IHttpActionResult PostDeviceCategory(DeviceCategory model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DeviceCategories.Add(model);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.Created);
        }

       
        [HttpDelete]
        [Route("DeleteDeviceCategory")]
        [ResponseType(typeof(DeviceCategory))]
        public IHttpActionResult DeleteDeviceCategory(DeviceCategory model)
        {
            DeviceCategory deviceCategory = db.DeviceCategories.Find(model.CategoryID);
            if (deviceCategory == null)
            {
                return NotFound();
            }

            db.DeviceCategories.Remove(deviceCategory);
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

        private bool DeviceCategoryExists(int id)
        {
            return db.DeviceCategories.Count(e => e.CategoryID == id) > 0;
        }
    }
}