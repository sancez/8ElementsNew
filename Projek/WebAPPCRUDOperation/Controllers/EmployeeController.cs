using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPPCRUDOperation.Models;
using WebAPPCRUDOperation.ViewModel;

namespace WebAPPCRUDOperation.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeDBEntities objEmployeeDbEntities;
        public EmployeeController()
        {
            objEmployeeDbEntities = new EmployeeDBEntities();
        }
        // GET: Employee
        public ActionResult Index()
        {
            ViewBag.Cities = (from obj in objEmployeeDbEntities.Cities
                              select new SelectListItem()
                              { 
                                Text = obj.Name,
                                Value = obj.CityId.ToString()
                              }).ToList();
            return View();
        }

        public ActionResult GetAllEmployee()
        {
            var employeeRecord = (from objEmp1 in objEmployeeDbEntities.Employees join
                                  objCities in objEmployeeDbEntities.Cities on
                                  objEmp1.CityId equals objCities.CityId
                                  select new 
                                  {
                                      EmployeId = objEmp1.EmployeeId,
                                      FirstName = objEmp1.FirstName,
                                      LastName = objEmp1.LastName,
                                      Department = objEmp1.Department,
                                      Jobtype = objEmp1.JobType,
                                      Salary = objEmp1.Salary,
                                      CityId1 = objEmp1.CityId,
                                      Name = objCities.Name
                                  }
                              ).ToList();
            return Json(new { Success =  true,data = employeeRecord}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddUpdateEmployee(EmployeeViewModel objEmployeeViewModel)
        {
            string Message = "Data Successfully Updated";
            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState
                                 where item.Value.Errors.Any()
                                 select item.Value.Errors[0].ErrorMessage).ToList();

                return Json(new { success = false,Message ="Some Problem in validation",errorList});
            }
            Employee objEmployee = objEmployeeDbEntities.Employees.SingleOrDefault(model => model.EmployeeId == objEmployeeViewModel.EmployeeId) ?? new Employee() ;
            objEmployee.EmployeeId = objEmployeeViewModel.EmployeeId;
            objEmployee.FirstName = objEmployeeViewModel.FirstName;
            objEmployee.LastName = objEmployeeViewModel.LastName;
            objEmployee.Department = objEmployeeViewModel.Department;
            objEmployee.JobType = objEmployeeViewModel.JobType;
            objEmployee.Salary = objEmployeeViewModel.salary;
            objEmployee.CityId = objEmployeeViewModel.CityId;

            if (objEmployeeViewModel.EmployeeId == 0)
            {
                Message = "Data SuccessFully Added..";
                objEmployeeDbEntities.Employees.Add(objEmployee);
            }

            objEmployeeDbEntities.SaveChanges();  
            return Json(new { Success = true, Message = Message },JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditEmployee(int employeeId)
        {
            return Json(objEmployeeDbEntities.Employees.SingleOrDefault(Model => Model.EmployeeId == employeeId),
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteEmployee(int employeeId)
        {
            Employee objEmployee = 
                objEmployeeDbEntities.Employees.Single(Model => Model.EmployeeId == employeeId);
            objEmployeeDbEntities.Employees.Remove(objEmployee);
            objEmployeeDbEntities.SaveChanges();
            return Json(new { Success = true,Message ="Data Successfully Deleted" },JsonRequestBehavior.AllowGet);
        }
    }
}