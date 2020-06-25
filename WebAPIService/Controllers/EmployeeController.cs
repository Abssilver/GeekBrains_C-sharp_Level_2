using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIService.Models;

namespace WebAPIService.Controllers
{
    public class EmployeeController : ApiController
    {
        private EmployeeData data = new EmployeeData();
        
        [HttpGet]
        [Route("getData")]
        public List<Employee> GetData() => data.GetEmployeeData();

        [Route("getData/{id}")]
        public Employee GetEmployee(int id) => data.GetEmployeeById(id);

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

        [Route("addEmployee")]
        public HttpResponseMessage Post([FromBody] Employee value)
        {
            if (data.AddEmployee(value))
                return Request.CreateResponse(HttpStatusCode.Created);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
