using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeHomework.Entities;

namespace EmployeeHomework
{
    public interface IEmployeeService
    {
        public List<EmployeeEntity> GetAllEmployees();
        public void AddEmployee(Employee employeeInformation);
        public EmployeeEntity GetEmployeeById(int id);
        public List<EmployeeEntity> GetEmployeesByNameAndBirthdate(string firstName, DateTime birthdateFrom, DateTime birthdateTo);
        public List<EmployeeEntity> GetEmployeesByBossId(int bossId);
        public void GetAvgSalaryAndEmployeeCount(string role, out int employeeCount, out decimal averageSalary);
        public void UpdateEmployee(Employee employee);
        public void UpdateEmployeeSalary(int id, decimal salary);
        public void DeleteEmployee(int id);
    }

    public class EmployeeService : IEmployeeService
    {
        private EmployeeDatabaseContext _employeeDatabaseContext;

        public EmployeeService(EmployeeDatabaseContext employeeDatabaseContext)
        {
            _employeeDatabaseContext = employeeDatabaseContext;
        }

        public List<EmployeeEntity> GetAllEmployees()
        {
            return _employeeDatabaseContext.EmployeeTable.ToList();
        }

        public void AddEmployee(Employee employeeInformation)
        {
            var entity = EmployeeEntity.From(employeeInformation);
            if (IsValid(entity))
            {
                _employeeDatabaseContext.EmployeeTable.Add(entity);
                _employeeDatabaseContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Employee data is not correct");
            }
        }

        public EmployeeEntity GetEmployeeById(int id)
        {
            return _employeeDatabaseContext.EmployeeTable.Find(id);
        }

        public List<EmployeeEntity> GetEmployeesByNameAndBirthdate(string firstName, DateTime birthdateFrom, DateTime birthdateTo)
        {
            var employees = from employee in _employeeDatabaseContext.EmployeeTable
                            where employee.FirstName == firstName
                            where employee.Birthdate >= birthdateFrom
                            where employee.Birthdate <= birthdateTo
                            select employee;

            return employees.ToList();
        }

        public List<EmployeeEntity> GetEmployeesByBossId(int bossId)
        {
            var employees = from employee in _employeeDatabaseContext.EmployeeTable
                            where employee.BossId == bossId
                            select employee;

            return employees.ToList();
        }

        public void GetAvgSalaryAndEmployeeCount(string role, out int employeeCount, out decimal averageSalary)
        {
            var salaryList = from employee in _employeeDatabaseContext.EmployeeTable
                             where employee.Role == role
                             select employee.CurrentSalary;

            employeeCount = salaryList.Count();

            averageSalary = salaryList.Average();
        }

        public void UpdateEmployee(Employee employee)
        {
            var entity = EmployeeEntity.From(employee);
            if(IsValid(entity))
            {
                _employeeDatabaseContext.EmployeeTable.Find(employee.Id).FirstName = entity.FirstName;
                _employeeDatabaseContext.EmployeeTable.Find(employee.Id).LastName = entity.LastName;
                _employeeDatabaseContext.EmployeeTable.Find(employee.Id).Birthdate = entity.Birthdate;
                _employeeDatabaseContext.EmployeeTable.Find(employee.Id).EmploymentDate = entity.EmploymentDate;
                _employeeDatabaseContext.EmployeeTable.Find(employee.Id).BossId = entity.BossId;
                _employeeDatabaseContext.EmployeeTable.Find(employee.Id).HomeAddress = entity.HomeAddress;
                _employeeDatabaseContext.EmployeeTable.Find(employee.Id).CurrentSalary = entity.CurrentSalary;
                _employeeDatabaseContext.EmployeeTable.Find(employee.Id).Role = entity.Role;

                _employeeDatabaseContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Employee data is not correct");
            }
        }

        public void UpdateEmployeeSalary(int id, decimal salary)
        {
            EmployeeEntity employee = _employeeDatabaseContext.EmployeeTable.Find(id);
            employee.CurrentSalary = salary;
            if (IsValid(employee))
            {
                _employeeDatabaseContext.EmployeeTable.Update(employee);
                _employeeDatabaseContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Employee set salary is not valid");
            }
        }

        public void DeleteEmployee(int id)
        {
            EmployeeEntity employee = _employeeDatabaseContext.EmployeeTable.Find(id);
            if(employee != null)
            {
                _employeeDatabaseContext.EmployeeTable.Remove(employee);
                _employeeDatabaseContext.SaveChanges();
            }
        }

        private bool IsValid(EmployeeEntity employeeInformation)
        {
            if (employeeInformation.Role == "CEO")
            {
                if(!string.IsNullOrEmpty(employeeInformation.BossId.ToString()))
                {
                    return false;
                }

                var employees = from employee in _employeeDatabaseContext.EmployeeTable
                                where employee.Role == "CEO"
                                select employee;

                if(employees.Count() > 0)
                {
                    return false;
                }
            }

            if(employeeInformation.FirstName.Length > 50)
            {
                return false;
            }

            if (employeeInformation.LastName.Length > 50)
            {
                return false;
            }

            if(employeeInformation.FirstName == employeeInformation.LastName)
            {
                return false;
            }
            
            DateTime minAge = new DateTime(18, 1, 1);
            DateTime maxAge = new DateTime(73, 1, 1);

            if (DateTime.Now.AddYears(-employeeInformation.Birthdate.Year) > maxAge ||
                DateTime.Now.AddYears(-employeeInformation.Birthdate.Year) < minAge)
            {
                return false;
            }

            DateTime earliestEmployment = new DateTime(2000, 1, 1);
            if (employeeInformation.EmploymentDate < earliestEmployment ||
                employeeInformation.EmploymentDate > DateTime.Now)
            {
                return false;
            }

            if(employeeInformation.CurrentSalary < 0)
            {
                return false;
            }

            return true;
        }
    }
}
