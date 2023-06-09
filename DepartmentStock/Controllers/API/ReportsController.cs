using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DepartmentStock.Controllers.API
{
    [RoutePrefix("api")]
    [Authorize]
    public class ReportsController : ApiController
    {
        private DepartmentStockEntities db = new DepartmentStockEntities();

        [HttpGet]
        [Route("Report")]
        public IHttpActionResult GetSponsorships()
        {
            return Ok(db.ReportFunc(HttpContext.Current.User.Identity.GetUserId()));
        }

    }
}
