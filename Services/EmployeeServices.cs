using DataBase;

namespace Services
{
    public class EmployeeService
    {
        public Employee? GetById(string id)
        {
            try
            {
                return (from emp in GlobalDB.Employees where emp.Id == id select emp).Single();
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public List<Employee> GetAll()
        {
            return GlobalDB.Employees;
        }

        public bool Create(Employee employee)
        {
            try
            {
                employee.Id = this.GenerateId();
                GlobalDB.Employees.Add(employee);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Employee employee)
        {
            try
            {
                int index = GlobalDB.Employees.FindIndex(emp => emp.Id == employee.Id);
                GlobalDB.Employees[index] = employee;
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool DeleteByID(string Id)
        {
            try
            {
                Employee? employee = this.GetById(Id);
                GlobalDB.Employees.Remove(employee);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Save(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.Id))
                return this.Create(employee);
            else
                return this.Update(employee);
        }

        public string GenerateId()
        {
            DateTime CurrentDate = DateTime.Now;
            return "TZ" + CurrentDate.Year + CurrentDate.Month + CurrentDate.Day + CurrentDate.Hour + CurrentDate.Minute + CurrentDate.Second;
        }

        public List<Employee> GetAssignedEmployees(List<string> roleIds)
        {
            return GlobalDB.Employees.FindAll(employee => roleIds.Contains(employee.Id));
        }
    }
}