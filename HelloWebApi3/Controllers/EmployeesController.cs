using HelloWebApi3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HelloWebApi3.Controllers
{
    public class EmployeesController : ApiController
    {

        private static IList<Employee> list = new List<Employee>()
        {
            new Employee()
            {
                Id = 12345, FirstName= "John", LastName = "Human"
            },

            new Employee()
            {                
                Id = 12346, FirstName = "Jane", LastName = "Public"
            },

            new Employee()
            {
                Id = 12347, FirstName = "Joseph", LastName = "Law"
            }
        };

        #region: original http methods
        // Action methods go here

        // Get api/employees
        public IEnumerable<Employee> Get()
        {
            return list;
        }

        // Get api/employees/12345
        public Employee Get(int id)
        {
            return list.First(e => e.Id == id);
        }

        // Post api/employees
        public void Post(Employee employee)
        {
            int maxId = list.Max(e => e.Id);
            employee.Id = maxId + 1;

            list.Add(employee);
        }

        // Put api/employees/12345
        public void Put(int id, Employee employee)
        {
            int index = list.ToList().FindIndex(e => e.Id == id);
            list[index] = employee;
        }

        // Delete api/employee/12345
        public void Delete(int id)
        {
            Employee employee = Get(id);
            list.Remove(employee);
        }
        #endregion



        //[HttpGet]
        //public Employee RetrieveEmployeeById(int id)
        //{
        //    return list.First(e => e.Id == id);
        //}



        //// Get api/employees/12345
        //public Employee Get(int id)
        //{
        //    var employee = list.FirstOrDefault(e => e.Id == id);
        //    if (null == employee)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.NotFound);
        //    }
        //    return employee;
        //}
        

    }
}
