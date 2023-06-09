using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using RestSharp;
using System.Text.Json;
using DepartmentStock.Controllers.API;
using DepartmentStock.SystemsApproaches.FileSystem;


namespace DepartmentStock.SystemsApproaches.ReportingSystem
{
    public class MainReport
    {
        private DepartmentStockEntities db = new DepartmentStockEntities();

        //public IEnumerable< view_main> StringReport(string userName)
        //{
        //  return db.view_main.Where(a => a.UserName == userName).Select(b => new { b.SponsorshipID, b.DeviceID, b.DeviceName, b.Date, b.Note, b.LocationName, b.CategoryName });
        //}
    }
}



