﻿using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;
using webApplicationDemo.DAL;
using webApplicationDemo.Models;

namespace webApplicationDemo.Controllers
{
    public class CompanyController : Controller
    {

        private readonly EmployeeDAL _employeeDAL;
     
        public CompanyController(EmployeeDAL employeeDAL)
        {
            _employeeDAL = employeeDAL;
        }

        public IActionResult Company()
        {
            return View();
        }

        public IActionResult AddEmpInCmp()
        {
            List<EmployeeModel> employees = _employeeDAL.GetAllEmployees();
            return View(employees);
        }


    }
}
