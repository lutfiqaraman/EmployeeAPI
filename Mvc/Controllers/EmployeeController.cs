using Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            IEnumerable<mvcEmployeeModel> ListofEmployee;
            HttpResponseMessage Response = GlobalVariables.WebApiClient.GetAsync("Employee").Result;

            ListofEmployee = Response.Content.ReadAsAsync<IEnumerable<mvcEmployeeModel>>().Result;

            return View(ListofEmployee);
        }

        public ActionResult AddOrUpdate(int id = 0)
        {
            if (id == 0)
                return View(new mvcEmployeeModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employee/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcEmployeeModel>().Result);
            }
            
        }

        [HttpPost]
        public ActionResult AddOrUpdate(mvcEmployeeModel employeeModel)
        {
            var EmployeeID = employeeModel.EmployeeID;

            if(EmployeeID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Employee", employeeModel).Result;
                TempData["SuccessMessage"] = "Saved Successfully ...";
            } else {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Employee/" + EmployeeID, employeeModel).Result;
                TempData["SuccessMessage"] = "Updated Successfully ...";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Employee/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully ...";

            return RedirectToAction("Index");
        }
    }
}