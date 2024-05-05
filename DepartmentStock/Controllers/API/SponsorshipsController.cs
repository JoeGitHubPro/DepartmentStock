using DepartmentStock.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace DepartmentStock.Controllers.API
{
    [RoutePrefix("api")]
    [Authorize]
    public class SponsorshipsController : ApiController
    {
        #region MainViewEF      
        //return Ok(db.Sponsorships.Select(a =>
        //new
        //{
        //    SponsorshipID = a.SponsorshipID,
        //    UserName = a.AspNetUser.UserName,
        //    Name = a.AspNetUser.Name,
        //    DeviceCode = a.Device1.DeviceID,
        //    DeviceName = a.Device1.DeviceName,

        //    DeviceCategory = db.DeviceCategories.Where(b=>b.CategoryID == a.Device1.DeviceCategory.Value).Select(c=>c.CategoryName).FirstOrDefault(),
        //    DeviceSpecification = db.DeviceSpecifications.Where(b => b.SpecificationID == a.Device1.DeviceSpecification.Value).Select(c => new { c.ModelName , c.INFO}).FirstOrDefault(),
        //    Date = a.Date,
        //    Notes = a.Note,
        //    Location = a.Location1.LocationName,

        //}));
        #endregion

        private DepartmentStockEntities db = new DepartmentStockEntities();

        [HttpGet]
        [Route("AllSponsorships")]

        public IHttpActionResult GetSponsorships()
        {
            return Ok(db.view_main);
        }


        [HttpGet]
        [Route("SingleSponsorship")]
        [ResponseType(typeof(Sponsorship))]
        public IHttpActionResult GetSponsorship(int SponsorshipID)
        {
            var sponsorship = db.view_main.Where(a => a.SponsorshipID == SponsorshipID).FirstOrDefault();
            if (sponsorship == null)
            {
                return NotFound();
            }

            return Ok(sponsorship);
        }

        [HttpPut]
        [Route("EditSponsorship")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = RoleNameModels.Admin)]
        public IHttpActionResult PutSponsorship(Sponsorship model)
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
                if (!SponsorshipExists(model.SponsorshipID))
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
        [Route("AddSponsorship")]
        [ResponseType(typeof(Sponsorship))]
        [Authorize(Roles = RoleNameModels.Admin)]
        public IHttpActionResult PostSponsorship(Sponsorship model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string userId = db.AspNetUsers.Where(a => a.UserName == model.User).Select(a => a.Id).SingleOrDefault();
            model.User = userId;
            db.Sponsorships.Add(model);
            db.SaveChanges();
            try
            {

                // SponsorshipNotification.AddSponsorshipNotification(model.SponsorshipID);

            }
            catch (Exception e)
            {
                return Ok("Obj added successfully, but without notification erorr message - " + e.Message);

            }

            return StatusCode(HttpStatusCode.Created);
        }

        [HttpDelete]
        [Route("DeleteSponsorship")]
        [ResponseType(typeof(Sponsorship))]
        [Authorize(Roles = RoleNameModels.Admin)]
        public IHttpActionResult DeleteSponsorship(Sponsorship model)
        {
            Sponsorship sponsorship = db.Sponsorships.Find(model.SponsorshipID);
            if (sponsorship == null)
            {
                return NotFound();
            }

            db.Sponsorships.Remove(sponsorship);
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

        private bool SponsorshipExists(int id)
        {
            return db.Sponsorships.Count(e => e.SponsorshipID == id) > 0;
        }
    }
}