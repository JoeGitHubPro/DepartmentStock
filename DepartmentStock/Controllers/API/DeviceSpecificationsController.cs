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
    public class DeviceSpecificationsController : ApiController
    {
        private DepartmentStockEntities db = new DepartmentStockEntities();

        [HttpGet]
        [Route("AllDeviceSpecifications")]
        public IHttpActionResult GetDeviceSpecifications()
        {
            return Ok( db.DeviceSpecifications.Select(a=>new {a.SpecificationID ,a.ModelName , a.INFO}));
        }

        [HttpPost]
        [Route("SingleDeviceSpecification")]
        [ResponseType(typeof(DeviceSpecification))]
        public IHttpActionResult GetDeviceSpecification(DeviceSpecification model)
        {
            var deviceSpecification = db.DeviceSpecifications.Where(a=>a.SpecificationID == model.SpecificationID).Select(a => new { a.SpecificationID, a.ModelName, a.INFO }).FirstOrDefault();
            if (deviceSpecification == null)
            {
                return NotFound();
            }

            return Ok(deviceSpecification);
        }

        [HttpPut]
        [Route("EditDeviceSpecification")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDeviceSpecification( DeviceSpecification model)
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
                if (!DeviceSpecificationExists(model.SpecificationID))
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
        [Route("AddDeviceSpecification")]
        [ResponseType(typeof(DeviceSpecification))]
        public IHttpActionResult PostDeviceSpecification(DeviceSpecification model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DeviceSpecifications.Add(model);
            db.SaveChanges();
            return StatusCode(HttpStatusCode.Created);

        }

        [HttpDelete]
        [Route("DeleteDeviceSpecification")]
        [ResponseType(typeof(DeviceSpecification))]
        public IHttpActionResult DeleteDeviceSpecification(DeviceSpecification model)
        {
            DeviceSpecification deviceSpecification = db.DeviceSpecifications.Find(model.SpecificationID);
            if (deviceSpecification == null)
            {
                return NotFound();
            }

            db.DeviceSpecifications.Remove(deviceSpecification);
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

        private bool DeviceSpecificationExists(int id)
        {
            return db.DeviceSpecifications.Count(e => e.SpecificationID == id) > 0;
        }
    }
}