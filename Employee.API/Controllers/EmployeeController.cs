using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Employee.Entities;
using Employee.Service;
using Employee.API.Filters;

namespace Employee.API.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeInfoService employeeService;
        public EmployeeController(IEmployeeInfoService _employeeService)
        {
            employeeService = _employeeService;
        }
        // GET api/values
        public HttpResponseMessage Get()
        {
            
            var data = employeeService.GetEmployeeList();
            if (data == null || data.Count() == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Records not found.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        // GET api/values/5
        public HttpResponseMessage Get(int id)
        {
            var data = employeeService.GetEmployee(id);
            if (data == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Record not found.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        // POST api/values
        [ValidateModel]
        public HttpResponseMessage Post([FromBody]EmployeeInfo value)
        {
            try
            {
                employeeService.CreateEmployee(value);
                return Request.CreateResponse(HttpStatusCode.OK, "New record saved successfully." );
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "An error occured while saving data.");
            }
        }

        // PUT api/values/5
        [ValidateModel]
        public HttpResponseMessage  Put([FromBody]EmployeeInfo value)
        {
            try
            {
                employeeService.UpdateEmployee(value);
                return Request.CreateResponse(HttpStatusCode.OK, "Record updated successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "An error occured while saving data.");
            }
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                employeeService.DeleteEmployee(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Record deleted successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "An error occured while saving data.");
            }
        }
    }
}
