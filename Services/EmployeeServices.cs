using Models;

namespace Services
{
    public class EmployeeService
    {
        private JsonService JsonService;

        public EmployeeService(JsonService JsonService)
        {
            this.JsonService = JsonService;
        }
        public Employee GetById(string id)
        {
            try
            {
                return (from emp in this.GetAll() where emp.Id == id select emp).Single();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Employee> GetAssignedEmployee(string Id)
        {
            return (from employee in this.GetAll() where employee.JobTitle == Id select employee).ToList();
        }

        public List<Employee> GetAll()
        {
            return (from employee in JsonService.GetEmployees() where employee.IsActive select employee).ToList();
        }

        public bool Create(Employee employee)
        {
            try
            {
                employee.Id = this.GenerateId();

                var Employees = this.GetAll();
                Employees.Add(employee);

                if (!JsonService.UpdateEmployeeJson(Employees))
                    throw new Exception();

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
                var Employees = this.GetAll();
                int index = Employees.FindIndex(emp => emp.Id == employee.Id);
                Employees[index] = employee;

                if (!JsonService.UpdateEmployeeJson(Employees))
                    throw new Exception();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteByID(string Id)
        {
            try
            {
                Employee employee = this.GetById(Id);
                if (employee == null)
                    throw new Exception();
                var Employees = new List<Employee>();
                foreach (Employee emp in this.GetAll())
                {
                    if (emp.Id == Id) emp.IsActive = false;

                    Employees.Add(emp);
                }

                if (!JsonService.UpdateEmployeeJson(Employees))
                    throw new Exception();
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