using JWTImplimentation.Context;
using JWTImplimentation.Interfaces;
using JWTImplimentation.Models;

namespace JWTImplimentation.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly JWTContext _db;
        public EmployeeService(JWTContext db)
        {
          _db = db;
            
        }

        public Employee AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "Employee object cannot be null.");
            }

            _db.Add(employee);
            _db.SaveChanges();
            return employee;

        }

        public Employee DeleteEmployee(int id)
        {
            var employeeToDelete = _db.Find<Employee>(id);
            if (employeeToDelete == null)
            {
                throw new ArgumentException("Employee with the provided ID does not exist.", nameof(id));
            }

            _db.Remove(employeeToDelete);
            _db.SaveChanges();

            return employeeToDelete;
        }

        public List<Employee> GetAllEmployees()
        {
            var employees = _db.employees.ToList();
            if (employees.Count == 0)
            {
                throw new ArgumentException("No employees found.");
            }

            return employees;
        }

        public Employee GetEmployeeDetail(int id)
        {
            var employee = _db.Find<Employee>(id);
            if (employee == null)
            {
                throw new ArgumentException("Employee with the provided ID does not exist.", nameof(id));
            }

            return employee;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "Employee object cannot be null.");
            }

            var existingEmployee = _db.Find<Employee>(employee.Id);
            if (existingEmployee == null)
            {
                throw new ArgumentException("Employee with the provided ID does not exist.", nameof(employee.Id));
            }

            // Update existingEmployee properties with employee properties
            existingEmployee.Name = employee.Name;
            existingEmployee.Position = employee.Position;
            existingEmployee.Company = employee.Company;

            _db.SaveChanges();
            return existingEmployee;
        }
    }
}
