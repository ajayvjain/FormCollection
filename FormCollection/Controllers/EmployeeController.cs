using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormCollection.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/

        public ActionResult Index()
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            return View(employeeBusinessLayer.GetEmployees());
        }

        [HttpGet]
        [ActionName("Create")]
        public ActionResult Create_Get()
        {

            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post(string name, string gender, DateTime dateofBirth, string city)
        {
            Employee employee = new Employee();
            employee.Name = name;
            employee.Gender = gender;
            employee.DateOfBirth = dateofBirth;
            employee.City = city;
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            employeeBusinessLayer.AddEmploee(employee);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            Employee employee = employeeBusinessLayer.GetEmployees().Single(emp => emp.Id == id);

            return View(employee);
        }

        //[HttpPost]
        //[ActionName("Edit")]
        //public ActionResult Edit_Post(int id)
        //{
        //    EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
        //    Employee employee = employeeBusinessLayer.GetEmployees().Single(x => x.Id == id);
        //    //Include List
        //    UpdateModel(employee, new string[] { "Id", "DateOfBirth", "Gender", "City" });

        //    if (ModelState.IsValid)
        //    {

        //        employeeBusinessLayer.UpdateEmployeeDetails(employee);

        //        return RedirectToAction("Index");
        //    }

        //    return View(employee);
        //}

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post([Bind(Include = "Id,DateOfBirth,Gender,City")]Employee employee)
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            //If excluded value "Name" is not set explicitly it'll be null.
            employee.Name = employeeBusinessLayer.GetEmployees().Single(x => x.Id == employee.Id).Name;

            if (ModelState.IsValid)
            {

                employeeBusinessLayer.UpdateEmployeeDetails(employee);

                return RedirectToAction("Index");
            }

            return View(employee);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            employeeBusinessLayer.DeleteEmployee(id);
            
            return RedirectToAction("Index");
        }
    }
}
