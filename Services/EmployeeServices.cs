using Models;

namespace Services
{
    public class EmployeeService
    {
        public JsonService JsonService = new JsonService();
        public Employee GetById(string id)
        {
            try
            {
                return (from emp in JsonService.ReadEmployees() where emp.Id == id select emp).Single();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Employee> GetAll()
        {
            return JsonService.ReadEmployees();
        }

        public bool Create(Employee employee)
        {
            try
            {
                employee.Id = this.GenerateId();
                var Employees = JsonService.ReadEmployees();
                Employees.Add(employee);
                if (!JsonService.UpdateEmployeeJson(Employees)) throw new Exception();

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
                var Employees = JsonService.ReadEmployees();
                int index = Employees.FindIndex(emp => emp.Id == employee.Id);
                Employees[index] = employee;
                if (!JsonService.UpdateEmployeeJson(Employees)) throw new Exception();

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
                Employee employee = this.GetById(Id);
                var Employees = JsonService.ReadEmployees();
                Employees = (from emp in JsonService.ReadEmployees() where emp.Id != Id select emp).ToList();
                if (!JsonService.UpdateEmployeeJson(Employees)) throw new Exception();
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

    }
}