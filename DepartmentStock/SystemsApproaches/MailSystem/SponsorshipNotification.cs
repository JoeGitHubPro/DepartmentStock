using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DepartmentStock.SystemsApproaches.MailSystem
{
    public static class SponsorshipNotification
    {
        public static void AddSponsorshipNotification(int Id)
        {
            using (DepartmentStockEntities db = new DepartmentStockEntities())
            {
                Sponsorship model = db.Sponsorships.Find(Id);
                string MailBody = $"Dear {model.AspNetUser.Name},\nA New Device Added To You Custody\n{model.SponsorshipID}\t{model.Location.Value}\t{model.Device1.DeviceID}\t{model.Device1.DeviceName}\t{model.Device1.DeviceSpecification1.ModelName}\t{model.Device1.DeviceSpecification1.INFO}\t{model.Location1.LocationName}\t{model.Date}\t{model.Note}";
                SendMailVia.SendViaGmail(model.AspNetUser.Email, "", "", MailBody, "CDMS Notification");

            }

        }
    }
}