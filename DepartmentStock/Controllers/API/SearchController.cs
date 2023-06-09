using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DepartmentStock.Controllers.API
{
    [RoutePrefix("api")]
    [Authorize]
    public class SearchController : ApiController
    {
        private DepartmentStockEntities db = new DepartmentStockEntities();

        [HttpPost]
        [Route("SearchMainByName")]
        public IHttpActionResult SearchByName(view_main model)
        {
            var query = db.view_main.Where(x => x.Name.Contains(model.Name));

            if (query.Any())
                return Ok(query);
           
            return BadRequest("Not Found");
        }

        [HttpPost]
        [Route("SearchMainByUserName")]
        public IHttpActionResult SearchByUserName(view_main model)
        {
            var query = db.view_main.Where(x => x.UserName.Contains(model.UserName));

            if (query.Any())
                return Ok(query);

            return BadRequest("Not Found");
        }

        [HttpPost]
        [Route("SearchMainByLocationName")]
        public IHttpActionResult SearchByLocationName(view_main model)
        {
            var query = db.view_main.Where(x => x.LocationName.Contains(model.LocationName));

            if (query.Any())
                return Ok(query);

            return BadRequest("Not Found");
        }

        [HttpPost]
        [Route("SearchMainByCategoryName")]
        public IHttpActionResult SearchByCategoryName(view_main model)
        {
            var query = db.view_main.Where(x => x.CategoryName.Contains(model.CategoryName));

            if (query.Any())
                return Ok(query);

            return BadRequest("Not Found");
        }

        [HttpPost]
        [Route("GetMainByDeviceID")]
        public IHttpActionResult GetByDeviceID(view_main model)
        {
            var query = db.view_main.Where(x => x.DeviceID == model.DeviceID);

            if (query.Any())
                return Ok(query);

            return BadRequest("Not Found");
        }

        [HttpPost]
        [Route("SearchUserByName")]
        public IHttpActionResult SearchUserByName(AspNetUser model)
        {
            var query = db.AspNetUsers.Where(x => x.Name.Contains(model.Name) );

            if (query.Any())
                return Ok(query);

            return BadRequest("Not Found");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

       


    }
}
