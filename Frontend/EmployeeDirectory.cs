using DataBase;
using Services;
namespace Frontend
{
    public class EmployeeDirectory
    {
        static Menus Menus = new Menus();
        static PrintData PrintData = new PrintData();
        static EmployeeService EmployeeService = new EmployeeService();
        static RoleService RoleService = new RoleService();
        static Utility Utility = new Utility();
        public void Initialize()
        {
            Console.WriteLine(Menus.ManagementMenu);
            int option;
            Utility.GetOption(out option, 3);
            switch (option)
            {
                case 1:
                    this.EmployeeInitalize();
                    break;
                case 2:
                    this.RoleInitialize();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }
        }

        public void EmployeeInitalize()
        {
            Console.WriteLine(Menus.EmployeeMenu);
            try
            {
                int option;
                Utility.GetOption(out option, 6);
                switch (option)
                {
                    case 1:
                        this.AddEmployee();
                        break;
                    case 2:
                        this.EditEmployee();
                        break;
                    case 3:
                        this.DeleteEmployee();
                        break;
                    case 4:
                        this.DisplayEmployees();
                        break;
                    case 5:
                        this.DisplayOne();
                        break;
                    case 6:
                        this.Initialize();
                        break;
                }
                this.EmployeeInitalize();
            }
            catch (System.Exception)
            {
                Console.WriteLine("Enter option from above");
                this.EmployeeInitalize();
            }
        }

        public void RoleInitialize()
        {
            Console.WriteLine(Menus.RoleMenu);
            try
            {
                int option;
                Utility.GetOption(out option, 5);
                switch (option)
                {
                    case 1:
                        this.AddRole();
                        break;
                    case 2:
                        this.EditRole();
                        break;
                    case 3:
                        this.DeleteRole();
                        break;
                    case 4:
                        this.ViewRoles();
                        break;
                    case 5:
                        this.Initialize();
                        break;
                }
            }
            catch (System.Exception)
            {
                Console.WriteLine("Enter options from above");
                this.RoleInitialize();
            }
            this.RoleInitialize();
        }


        public void AddEmployee()
        {
            if (RoleService.GetCount() != 0)
            {
                Console.WriteLine("------------------------------\nAdd Employee\n------------------------------");
                Employee employee = new Employee()
                {
                    Id = EmployeeService.GenerateId(),
                    Name = Utility.GetInputString("Fullname", true, @"^[a-zA-Z]+\ [a-zA-Z]+$"),
                    DateOfBirth = Utility.GetInputDate("Date of birth", false),
                    Email = Utility.GetInputEmail(),
                    MobileNumber = Utility.GetMobileNumber(),
                    JoiningDate = Utility.GetInputDate("Joining date", true),
                    Location = Utility.GetInputString("Location", true, null),
                    JobTitle = this.AssignRoleToEmployee(),
                    Department = Utility.GetInputString("Department", true, null),
                    Manager = Utility.GetInputString("Manager ", false, null),
                    Project = Utility.GetInputString("Project ", false, null),
                    Status = "Active",
                };
                Console.WriteLine("Employee Id : {0}", employee.Id);
                EmployeeService.Save(employee);
                RoleService.AssignEmployeeToRole(employee);
                Console.WriteLine("Employee Added Successfully.. :) \n");
            }
            else
            {
                Console.WriteLine("Add Some Roles assign to employees ... ");
            }
        }

        public void EditEmployee()
        {
            this.DisplayEmployees();
            if (EmployeeService.GetCount() != 0)
            {
                Console.WriteLine("Enter Employee id");
                string? id = Console.ReadLine();
                Employee? employee = EmployeeService.GetById(id ??= "");
                if (employee != null)
                {
                    Console.WriteLine(Menus.EditEmployeeMenu);
                    int option;
                    Utility.GetOption(out option, 10);
                    this.UpdateEmployee(option, employee);
                }
                else
                {
                    Console.WriteLine("Employee not found with given Id");
                    this.EditEmployee();
                }
            }
        }

        public void UpdateEmployee(int option, Employee employee)
        {
            switch (option)
            {
                case 1:

                    employee.Name = Utility.GetInputString("Fullname", true, @"^[a-zA-Z]+\ [a-zA-Z]+$");
                    break;
                case 2:
                    employee.Location = Utility.GetInputString("Location", true, null);
                    break;
                case 3:
                    employee.Department = Utility.GetInputString("Department", true, null);
                    break;
                case 4:
                    employee.JoiningDate = Utility.GetInputDate("Joining date", true);
                    break;
                case 5:
                    RoleService.RemoveEmployeeFromRole(employee);
                    employee.JobTitle = this.AssignRoleToEmployee();
                    RoleService.AssignEmployeeToRole(employee);
                    break;
                case 6:
                    employee.DateOfBirth = Utility.GetInputDate("Date of birth", false);
                    break;
                case 7:
                    employee.Email = Utility.GetInputEmail();
                    break;
                case 8:
                    employee.Manager = Utility.GetInputString("Manager ", false, null);
                    break;
                case 9:
                    employee.Project = Utility.GetInputString("Project ", false, null);
                    break;
                default:
                    new EmployeeDirectory().EmployeeInitalize();
                    break;
            }
            EmployeeService.Update(employee);
            Console.WriteLine("\nUpdated Successfully :)");
            new EmployeeDirectory().EmployeeInitalize();
        }

        public string AssignRoleToEmployee()
        {
            List<string> list = RoleService.ShowRoles();
            for (int i = 0; i < list.Count; i++)
                Console.WriteLine("{0}.{1}", i + 1, list.ElementAt(i).Split(" ")[1]);
            Console.WriteLine("Enter options from above");
            int option;
            Utility.GetOption(out option, list.Count);
            return list.ElementAt(option - 1).Split(" ")[0];
        }

        public void DisplayOne()
        {
            try
            {
                Console.WriteLine("Enter Employee Id");
                string? id = Console.ReadLine()?.Trim();
                Employee? employee = EmployeeService.GetById(id ??= "");
                if (employee == null) throw new Exception();
                this.DisplayEmployees(new List<Employee> { employee });
            }
            catch (System.Exception)
            {
                Console.WriteLine("Employee not found with given Id");
            }
        }

        public void DeleteEmployee()
        {
            if (GlobalDB.Employees.Count == 0)
                DisplayEmployees();
            else
            {
                string? id = Console.ReadLine()?.Trim();
                Employee? employee = EmployeeService.GetById(id ??= "");
                if (employee != null)
                {
                    EmployeeService.DeleteByID(id);
                    RoleService.RemoveEmployeeFromRole(employee);
                }
                else
                {
                    Console.WriteLine("Employee not found");
                }
            }
        }

        public void DisplayEmployees()
        {
            if (EmployeeService.GetCount() == 0)
                PrintData.PrintNoData();
            else
                PrintData.PrintTableHead();
            foreach (Employee employee in GlobalDB.Employees)
            {
                PrintData.PrintEmployeeRow(employee);
                PrintData.PrintLine();
            }
        }

        public void DisplayEmployees(List<Employee> employees)
        {
            if (employees.Count == 0)
                PrintData.PrintNoData();
            else
                PrintData.PrintTableHead();
            foreach (Employee employee in employees)
            {
                PrintData.PrintEmployeeRow(employee);
                PrintData.PrintLine();
            }
        }

        public void AddRole()
        {
            Role role = new Role()
            {
                Name = Utility.GetInputString("Role name", true, null),
                Department = Utility.GetInputString("Department ", true, null),
                Location = Utility.GetInputString("Location", true, null),
                Description = Utility.GetInputString("Description ", true, null),
                Id = RoleService.GenerateId()
            };
            RoleService.Save(role);
            Console.WriteLine("Role saved successfully");
        }

        public void EditRole()
        {
            if (RoleService.GetCount() == 0)
            {
                PrintData.PrintNoData();
            }
            else
            {
                List<string> roles = RoleService.ShowRoles();
                for (int i = 0; i < roles.Count; i++)
                    Console.WriteLine("{0}.{1}", i + 1, roles.ElementAt(i));
                Console.WriteLine("Enter id from above");
                string? id = Console.ReadLine()?.Trim();
                Role? role = RoleService.GetById(id ??= "");
                if (role != null)
                {
                    Console.WriteLine(Menus.EditRoleMenu);
                    Console.WriteLine("Enter options from above");
                    int option;
                    Utility.GetOption(out option, 5);
                    this.UpdateRole(option, role);
                }
                else
                {
                    Console.WriteLine("Role not found with given id");
                    this.EditRole();
                }
            }
        }

        public void UpdateRole(int option, Role role)
        {
            switch (option)
            {
                case 1:
                    role.Name = Utility.GetInputString("Role name", true, null);
                    break;
                case 2:
                    role.Department = Utility.GetInputString("Department", true, null);
                    break;
                case 3:
                    role.Location = Utility.GetInputString("Location", true, null);
                    break;
                case 4:
                    role.Description = Utility.GetInputString("Description", true, null);
                    break;
                default:
                    this.RoleInitialize();
                    break;
            }
        }

        public void DeleteRole()
        {
            if (RoleService.GetCount() == 0)
            {
                PrintData.PrintNoData();
            }
            else
            {
                List<string> roles = RoleService.ShowRoles();
                for (int i = 0; i < roles.Count; i++)
                    Console.WriteLine("{0}.{1}", i + 1, roles.ElementAt(i));
                Console.WriteLine("Enter id from above");
                string? id = Console.ReadLine()?.Trim();
                Role? role = RoleService.GetById(id ??= "");
                if (role != null)
                {
                    if (role.AssignedEmployeesId.Count > 0)
                    {
                        Console.WriteLine("In this role has employees\nPlease assign these employees to another role and then delete the role");
                    }
                    else
                    {
                        RoleService.DeleteById(id);
                        Console.WriteLine("Role deleted Successfully");
                    }
                }
                else
                {
                    Console.WriteLine("Role not found with given Id");
                }
            }
        }

        public void ViewRoles()
        {
            if (RoleService.GetCount() == 0)
            {
                PrintData.PrintNoData();
            }
            else
            {
                Console.WriteLine("+----------------------+");
                Console.WriteLine("|       Roles          |");
                Console.WriteLine("+----------------------+");
            }
            foreach (Role role in GlobalDB.Roles)
            {
                PrintData.PrintRoleHeader();
                PrintData.PrintRoleRow(role);
            }
        }

    }
}