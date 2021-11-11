using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebAPPCRUDOperation.ViewModel
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        [Display(Name = "First Name :")]
        [Required(ErrorMessage ="First name is Required..")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Display(Name = "Department Name :")]
        [Required(ErrorMessage = "Departmen name is Required..")]
        public string Department { get; set; }
        [Display(Name = "Job type Name :")]
        [Required(ErrorMessage = "Job type is Required..")]
        public string JobType { get; set; }
        [Display(Name = "Salary Name :")]
        [Required(ErrorMessage = "Salary is Required..")]
        public decimal salary { get; set; }
        public int CityId { get; set; }
    }
}