using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistrationApp.Model;
using System.Data.Entity.SqlServer;

namespace UserRegistrationDb.DbOperations
{
    public class EmployeeRepository
    {
        public int AddEmployee(EmployeeModel model)
        {
            using (var context = new EmployeeDBEntities())
            {
                Employee emp = new Employee()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Code = model.Code,


                };
                if (model.Address != null)
                {
                    emp.Address = new Address()
                    {
                        Details = model.Address.Details,
                        country = model.Address.country,
                        State = model.Address.State

                    };
                }
                context.Employee.Add(emp);
                context.SaveChanges();
                return emp.Id;
            }
        }

        public List<EmployeeModel> GetEmployees()
        {
            using (var context = new EmployeeDBEntities())
            {
                var result = context.Employee.
                    Select(X => new EmployeeModel()
                    {
                        Id = X.Id,
                        AddressId = X.AddressId,
                        Code = X.Code,
                        Email = X.Email,
                        FirstName = X.FirstName,
                        LastName = X.LastName,
                        Address = new AddressModel()
                        {
                            Id = X.Id,
                            Details = X.Address.Details,
                            country = X.Address.country,
                            State = X.Address.State
                        }
                    }
                    ).ToList();
                return result;
            }
        }

        public EmployeeModel GetEmployee(int id)
        {
            using (var context = new EmployeeDBEntities())
            {
                var result = context.Employee
                    .Where(x => x.Id == id)
                    .Select(X => new EmployeeModel()
                    {
                        Id = X.Id,
                        AddressId = X.AddressId,
                        Code = X.Code,
                        Email = X.Email,
                        FirstName = X.FirstName,
                        LastName = X.LastName,
                        Address = new AddressModel()
                        {
                            Id = X.Id,
                            Details = X.Address.Details,
                            country = X.Address.country,
                            State = X.Address.State
                        }
                    }
                    ).FirstOrDefault();
                return result;
            }


        }

        public bool updateEmployee(int id , EmployeeModel model)
        {
            using (var context = new EmployeeDBEntities())
            {
                var employee = context.Employee.FirstOrDefault(x => x.Id == id);

                if (employee != null)
                {
                    employee.FirstName = model.FirstName;
                    employee.LastName = model.LastName;
                    employee.Email = model.Email;
                    employee.Code = model.Code;

                }

                context.SaveChanges();
                return true;
            }
        }
    }
}