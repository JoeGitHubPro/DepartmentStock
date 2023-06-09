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
    public class DevicesController : ApiController
    {
        private DepartmentStockEntities db = new DepartmentStockEntities();

        [HttpGet]
        [Route("AllAvailableDevices")]
        [AllowAnonymous]
        public IHttpActionResult GetAllAvailableDevices()
        {
            var result = (from a in db.Devices

                          join b in db.DeviceSpecifications on a.DeviceSpecification equals b.SpecificationID
                          where !db.Sponsorships.Any(q => q.Device1.DeviceID == a.DeviceID)
                          select new { a.DeviceID, a.DeviceName, a.DeviceCategory1.CategoryName, b.SpecificationID, b.ModelName, b.INFO });
          
            return Ok(result);
        }


        [HttpGet]
        [Route("AllDevices")]
        [AllowAnonymous]
        public IHttpActionResult GetDevices()
        {


            var result = (from a in db.Devices
                          join b in db.DeviceSpecifications on a.DeviceSpecification equals b.SpecificationID
                          select new { a.DeviceID, a.DeviceName, a.DeviceCategory1.CategoryName, b.SpecificationID, b.ModelName, b.INFO });

            return Ok(result);
        }

        [HttpGet]
        [Route("SingleDevice")]
        [AllowAnonymous]
        [ResponseType(typeof(Device))]
        public IHttpActionResult GetDevice(string DeviceID)
        {
            // var device = db.Devices.Where(a=>a.DeviceID == model.DeviceID).Select(a => new { a.DeviceID, a.DeviceName }).FirstOrDefault();

            var result = (from a in db.Devices
                          join b in db.DeviceSpecifications on a.DeviceSpecification equals b.SpecificationID
                          select new { a.DeviceID, a.DeviceName, a.DeviceCategory1.CategoryName, b.SpecificationID, b.ModelName, b.INFO }).Where(a => a.DeviceID == DeviceID).SingleOrDefault();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        [HttpPut]
        [Route("EditDevice")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDevice( Device model, string modelName, string info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var device = db.Devices.Where(a => a.DeviceID == model.DeviceID).Select(q => q.DeviceSpecification).FirstOrDefault();
            model.DeviceSpecification = device;

            db.Entry(model).State = EntityState.Modified;

            DeviceSpecification specification = db.DeviceSpecifications.Find(device);
            specification.ModelName = modelName;
            specification.INFO = info;
            db.Entry(specification).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e )
            {
                throw e;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("AddDevice")]
        [ResponseType(typeof(Device))]
        public IHttpActionResult PostDevice(Device device, string modelName, string info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (device.DeviceSpecification.Value == 0)
            {
                DeviceSpecification specification = new DeviceSpecification();
                specification.ModelName = modelName;
                specification.INFO = info;
                db.DeviceSpecifications.Add(specification);
                db.SaveChanges();
                device.DeviceSpecification = specification.SpecificationID;
            }
            else
            {
                device.DeviceSpecification = device.DeviceSpecification.Value;
            }







            db.Devices.Add(device);


            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DeviceExists(device.DeviceID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.Created);
        }

        [HttpDelete]
        [Route("DeleteDevice")]
        [ResponseType(typeof(Device))]
        public IHttpActionResult DeleteDevice(Device model)
        {
            Device device = db.Devices.Find(model.DeviceID);
            if (device == null)
            {
                return NotFound();
            }

            db.Devices.Remove(device);
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

        private bool DeviceExists(string id)
        {
            return db.Devices.Count(e => e.DeviceID == id) > 0;
        }
    }
}