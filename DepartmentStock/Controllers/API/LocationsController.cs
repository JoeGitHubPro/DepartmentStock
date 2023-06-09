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
    public class LocationsController : ApiController
    {
        private DepartmentStockEntities db = new DepartmentStockEntities();

  
        [HttpGet]
        [Route("AllLocations")]
        public IHttpActionResult GetLocations()
        {
            return Ok( db.Locations.Select(a=> new {a.LocationID ,a.LocationName}));
        }

        [HttpGet]
        [Route("SingleLocation")]
        [ResponseType(typeof(Location))]
        public IHttpActionResult GetLocation(int LocationID)
        {
            Location location = db.Locations.Find(LocationID);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location.LocationName);
        }

       
        [HttpPut]
        [Route("EditLocation")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLocation(Location model)
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
                if (!LocationExists(model.LocationID))
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
        [Route("AddLocation")]
        [ResponseType(typeof(Location))]
        public IHttpActionResult PostLocation(Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Locations.Add(location);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.Created);
        }

    
        [HttpDelete]
        [Route("DeleteLocation")]
        [ResponseType(typeof(Location))]
        public IHttpActionResult DeleteLocation(Location model)
        {
            Location location = db.Locations.Find(model.LocationID);
            if (location == null)
            {
                return NotFound();
            }

            db.Locations.Remove(location);
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

        private bool LocationExists(int id)
        {
            return db.Locations.Count(e => e.LocationID == id) > 0;
        }
    }
}