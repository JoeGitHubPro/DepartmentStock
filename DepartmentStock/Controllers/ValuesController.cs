using DepartmentStock.SystemsApproaches.BackupSystem;
using DepartmentStock.SystemsApproaches.FileSystem;
using DepartmentStock.SystemsApproaches.MailSystem;
using DepartmentStock.SystemsApproaches.ReportingSystem;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Results;

namespace DepartmentStock.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {



        // GET api/values
        [AllowAnonymous]
        public IHttpActionResult Get()
        {

            AuthenticationHeaderValue authHeader = Request.Headers.Authorization;

            
            if (authHeader != null && authHeader.Scheme == "Bearer")
            {
                var token = authHeader.Parameter;
                // use the token value here

                return Ok();
            }
            else
            {
                return Unauthorized();
            }

        }

        [Route("api/verification")]
        [HttpGet]
        public IHttpActionResult verification()
        {

            return Ok();
        }

        // GET api/values/5
        [AllowAnonymous]
        [Route("api/Log")]
        [HttpGet]
        public RedirectResult Log()
        {
            string root = System.Web.HttpContext.Current.Server.MapPath("~/Files/LoggingArea/Log.txt");


            //String ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //SqlConnectionStringBuilder Builder = new SqlConnectionStringBuilder(ConStr);

            //String DatabaseName = Builder.InitialCatalog;
            //String ServerDB = Builder.DataSource;
            //String PasswordDB = Builder.Password;
            //String UserNameDB = Builder.UserID;

            //DatabaseBackup.GenerateBackup(ServerDB, UserNameDB, PasswordDB, DatabaseName);
           


            return Redirect(root);

        }
        [Route("api/Test")]
        [HttpGet]
        public IHttpActionResult Test()
        {

            //string userName = HttpContext.Current.User.Identity .GetUserName();
            //var userId = HttpContext.Current.User.Identity.GetUserId();
            //using (var context = new DepartmentStockEntities())
            //{
            //    context.ReportFunc("");
            //    var user = context.AspNetUsers.FirstOrDefault(u => u.Id == userId);      
            //    SendMailVia.SendViaGmail("gamasaporto@gmail.com", "youssefprogrammingservices@gmail.com", "qpklcmchtorbbqgh", "MailBody", "Subject HTML Test");

            //    return Ok();
            //}

            //MainReport mainReport = new MainReport();

            //return Ok(mainReport.StringReport(userName).Select(b => new { b.SponsorshipID, b.DeviceID, b.DeviceName, b.Date, b.Note, b.LocationName, b.CategoryName }));

            return Ok();
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
