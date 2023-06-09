using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DepartmentStock.Models
{

    public class UserModels
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "RoleName")]
        public string RoleName { get; set; }


        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [DataType(DataType.PhoneNumber)]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }


        [Display(Name = "TargetRole")]
        public string TargetRole { get; set; }


        [Display(Name = "Degree")]
        public string Degree { get; set; }




    }

}