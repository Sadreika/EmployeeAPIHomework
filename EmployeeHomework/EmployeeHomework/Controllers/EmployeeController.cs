using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace EmployeeHomework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllEmployees")]
        public ActionResult GetAllEmployees()
        {
            return Ok(_employeeService.GetAllEmployees());
        }

        [HttpPost]
        [Route("AddEmployees")]
        public void AddEmployees([FromBody] Employee employeeInformation)
        {
            try
            {
                _employeeService.AddEmployee(employeeInformation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetEmployeeById/{employeeId}")]
        public ActionResult GetEmployeeById(int employeeId)
        {
            return Ok(_employeeService.GetEmployeeById(employeeId));
        }

        [HttpDelete]
        [Route("DeleteEmployee/{employeeId}")]
        public void DeleteEmployee(int employeeId)
        {
            _employeeService.DeleteEmployee(employeeId);
        }

        [HttpPut]
        [Route("UpdateEmployee")]
        public void UpdateEmployee([FromBody] Employee employee)
        {
            try
            {
                _employeeService.UpdateEmployee(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateEmployeeSalary/{id}/{salary}")]
        public void UpdateEmployeeSalary(int id, decimal salary)
        {
            try
            {
                _employeeService.UpdateEmployeeSalary(id, salary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAvgSalaryAndEmployeeCount/{role}")]
        public ActionResult GetAvgSalaryAndEmployeeCount(string role)
        {
            _employeeService.GetAvgSalaryAndEmployeeCount(role, out int employeeCount, out decimal averageSalary);

            return Ok(new { employeeCount, averageSalary });
        }

        [HttpGet]
        [Route("GetEmployeesByBossId/{bossId}")]
        public ActionResult GetEmployeesByBossId(int bossId)
        {
            return Ok(_employeeService.GetEmployeesByBossId(bossId));
        }

        [HttpGet]
        [Route("GetEmployeesByNameAndBirthdate/{firstName}/{birthdateFrom}/{birthdateTo}")]
        public ActionResult GetEmployeesByNameAndBirthdate(string firstName, DateTime birthdateFrom, DateTime birthdateTo)
        {
            return Ok(_employeeService.GetEmployeesByNameAndBirthdate(firstName, birthdateFrom, birthdateTo));
        }
    }
}
