using DataBase;

namespace Services
{

    public class EmployeeService
    {
        public Employee? GetById(string id)
        {
            try
            {
                IEnumerable<Employee> employees = from emp in GlobalDB.Employees where emp.Id == id select emp;
                return employees.Single();
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

        public int GetCount()
        {
            return GlobalDB.Employees.Count;
        }
        public void Create(Employee employee)
        {
            GlobalDB.Employees.Add(employee);
        }

        public void Update(Employee employee)
        {
            int index = GlobalDB.Employees.FindIndex(emp => emp.Id == employee.Id);
            GlobalDB.Employees[index] = employee;
        }

        public void DeleteByID(string Id)
        {
            GlobalDB.Employees = (from emp in GlobalDB.Employees where emp.Id != Id select emp).ToList();
        }

        public void Save(Employee employee)
        {
            Employee? emp = this.GetById(employee.Id);
            if (emp == null) this.Create(employee);
            else this.Update(employee);
        }

        public int Random4Digit()
        {
            return new Random().Next(1000, 9999);
        }

        public string GenerateId()
        {
            string id = "TZ" + this.Random4Digit();
            Employee? employee = this.GetById(id);
            if (employee == null) return id;

            return this.GenerateId();
        }

    }

    public class RoleService
    {
        static EmployeeService EmployeeService = new EmployeeService();

        public Role? GetById(string id)
        {
            try
            {
                IEnumerable<Role> roles = from role in GlobalDB.Roles where role.Id == id select role;
                return roles.Single();
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public List<Role> GetRoles()
        {
            return GlobalDB.Roles;
        }
        public void DeleteById(string Id)
        {
            GlobalDB.Roles = (from role in GlobalDB.Roles where role.Id != Id select role).ToList();

        }
        public int GetCount()
        {
            return GlobalDB.Roles.Count;
        }
        public List<Employee> GetAssignedEmployees(List<string> ids)
        {
            List<Employee> emps = new List<Employee>();
            foreach (string id in ids)
            {
                Employee? employee = EmployeeService.GetById(id);
                if (employee != null) emps.Add(employee);
            }
            return emps;
        }

        public List<string> ShowRoles()
        {
            var list = from role in GlobalDB.Roles select role.Id + " " + role.Name;
            return list.ToList();
        }

        public void Save(Role role)
        {
            Role? r = GetById(role.Id);
            if (r == null) Create(role);
            else Update(role);
        }

        public void Create(Role role)
        {
            GlobalDB.Roles.Add(role);
        }

        public void Update(Role role)
        {
            int index = GlobalDB.Roles.FindIndex(r => r.Id == role.Id);
            GlobalDB.Roles[index] = role;
        }

        public int Random4Digit()
        {
            return new Random().Next(1000, 9999);
        }

        public void AssignEmployeeToRole(Employee employee)
        {
            Role? role = this.GetById(employee.JobTitle);
            if (role != null)
            {
                role.AssignedEmployeesId.Add(employee.Id);
                this.Update(role);
            }
        }

        public void RemoveEmployeeFromRole(Employee employee)
        {
            Role? role = this.GetById(employee.JobTitle);
            if (role != null)
            {
                List<string> emps = role.AssignedEmployeesId;
                if (emps.Contains(employee.Id)) emps.Remove(employee.Id);
                role.AssignedEmployeesId = emps;
                this.Update(role);
            }
        }
        public string GenerateId()
        {
            string id = "TZ" + Random4Digit();
            Role? role = GetById(id);
            if (role == null) return id;

            return GenerateId();
        }
    }

}