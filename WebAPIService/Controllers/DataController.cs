using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIService.Models;

namespace WebAPIService.Controllers
{
    public class DataController : ApiController
    {
        private EmployeeData _employeeData = new EmployeeData();
        private DepartmentData _departmentData = new DepartmentData();
        
        [HttpGet]
        [Route("get_employee")]
        public List<Employee> GetEmployeeData() => _employeeData.GetEmployeeData();

        [Route("get_employee/{id}")]
        public Employee GetEmployee(int id) => _employeeData.GetEmployeeById(id);

        [Route("get_departments")]
        public List<Department> GetDepartmentData() => _departmentData.GetDepartmentData();

        #region TestRoute
        /*
        //test/22/e/2/st/10
        [Route("test/{s}/e/{e}/st/{st}")]
        public IEnumerable<int> GetEmployee(int s, int e, int st = 0)
        {
            return Enumerable.Range(s, e + st);
        }
        */
        #endregion

        [Route("add_employee")]
        public HttpResponseMessage Post([FromBody] Employee value)
        {
            if (_employeeData.AddEmployee(value))    
                return Request.CreateResponse(HttpStatusCode.Created);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
