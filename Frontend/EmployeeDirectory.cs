using Models;
using Services;

namespace Frontend
{

    public class EmployeeDirectory
    {
        Menus Menus = new Menus();
        ConsoleUtility ConsoleUtility = new ConsoleUtility();
        EmployeeService EmployeeService = new EmployeeService();
        RoleService RoleService = new RoleService();
        Utility Utility = new Utility();

        public void Initialize()
        {
            Console.WriteLine(Menus.ManagementMenu);
            int option;
            Utility.GetOption(out option, 3);
            switch ((MainMenu)option)
            {
                case MainMenu.Employee:
                    this.EmployeeInitalize();
                    break;

                case MainMenu.Role:
                    this.RoleInitialize();
                    break;

                case MainMenu.Exit:
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
                switch ((EmployeeMenu)option)
                {
                    case EmployeeMenu.Add:
                        this.AddEmployee();
                        break;

                    case EmployeeMenu.Edit:
                        this.EditEmployee();
                        break;

                    case EmployeeMenu.Delete:
                        this.DeleteEmployee();
                        break;

                    case EmployeeMenu.DisplayAll:
                        this.DisplayEmployees();
                        break;

                    case EmployeeMenu.DisplayOne:
                        this.DisplayOne();
                        break;

                    case EmployeeMenu.Back:
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
                switch ((RoleMenu)option)
                {
                    case RoleMenu.Add:
                        this.AddRole();
                        break;

                    case RoleMenu.Edit:
                        this.EditRole();
                        break;

                    case RoleMenu.Delete:
                        this.DeleteRole();
                        break;

                    case RoleMenu.Display:
                        this.ViewRoles();
                        break;

                    case RoleMenu.Back:
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
            if (RoleService.GetAll().Count > 0)
            {
                Console.WriteLine("------------------------------\nAdd Employee\n------------------------------");
                Employee employee = new Employee()
                {
                    Name = Utility.GetInputString("Fullname", true, RegularExpression.NamePattern),
                    DateOfBirth = Utility.GetInputDate("Date of birth", false),
                    Email = Utility.GetInputEmail(),
                    MobileNumber = Utility.GetMobileNumber(),
                    JoiningDate = Utility.GetInputDate("Joining date", true),
                    Location = Utility.GetInputString("Location", true, null),
                    JobTitle = this.AssignRoleToEmployee(),
                    Department = Utility.GetInputString("Department", true, null),
                    Manager = Utility.GetInputString("Manager ", false, null),
                    Project = Utility.GetInputString("Project ", false, null),
                };
                bool status = EmployeeService.Save(employee);
                if (status)
                    Console.WriteLine("Employee Created Successfully \n");
                else
                    Console.WriteLine("Error in Creation of employee");
            }
            else
            {
                Console.WriteLine("There are no roles available to create an Employee. Please create roles to create an employee ");
            }
        }

        public void EditEmployee()
        {
            try
            {
                this.DisplayEmployees();
                if (EmployeeService.GetAll().Count > 0)
                {
                    Console.WriteLine("Enter your option");
                    int optionId;
                    Utility.GetOption(out optionId, EmployeeService.GetAll().Count);
                    string? id = EmployeeService.GetAll().ElementAt(optionId - 1).Id;
                    Employee? employee = EmployeeService.GetById(id ?? "");
                    Console.WriteLine(Menus.EditEmployeeMenu);
                    int option;
                    Utility.GetOption(out option, 10);
                    this.UpdateEmployee(option, employee);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Employee not found with given Id");
                this.EditEmployee();
            }
        }

        public void UpdateEmployee(int option, Employee employee)
        {
            switch ((EditEmployeeMenu)option)
            {
                case EditEmployeeMenu.Name:
                    employee.Name = Utility.GetInputString("Fullname", true, RegularExpression.NamePattern);
                    break;

                case EditEmployeeMenu.Location:
                    employee.Location = Utility.GetInputString("Location", true, null);
                    break;

                case EditEmployeeMenu.Department:
                    employee.Department = Utility.GetInputString("Department", true, null);
                    break;

                case EditEmployeeMenu.JoiningDate:
                    employee.JoiningDate = Utility.GetInputDate("Joining date", true);
                    break;

                case EditEmployeeMenu.Jobtitle:
                    UpdateJobTitle(employee);
                    break;

                case EditEmployeeMenu.DateOfBirth:
                    employee.DateOfBirth = Utility.GetInputDate("Date of birth", false);
                    break;

                case EditEmployeeMenu.Email:
                    employee.Email = Utility.GetInputEmail();
                    break;

                case EditEmployeeMenu.Manager:
                    employee.Manager = Utility.GetInputString("Manager ", false, null);
                    break;

                case EditEmployeeMenu.Project:
                    employee.Project = Utility.GetInputString("Project ", false, null);
                    break;

                case EditEmployeeMenu.Back:
                    new EmployeeDirectory().EmployeeInitalize();
                    break;
            }
            EmployeeService.Update(employee);
            Console.WriteLine("\nUpdated Successfully :)");
            this.EmployeeInitalize();
        }

        public string AssignRoleToEmployee()
        {
            Console.WriteLine("Select Jobtitle / Role ");
            List<string> list = this.RoleService.GetRoles();
            for (int i = 0; i < list.Count; i++)
                Console.WriteLine("{0}.{1}", i + 1, list.ElementAt(i).Replace(list.ElementAt(i).Split(" ")[0], ""));
            Console.WriteLine("Enter options from above");
            int option;
            Utility.GetOption(out option, list.Count);
            return list.ElementAt(option - 1).Split(" ")[0];
        }

        public void DisplayOne()
        {
            if (EmployeeService.GetAll().Count > 0)
                try
                {
                    Console.WriteLine("Enter Employee Id");
                    string? id = Console.ReadLine()?.Trim();
                    Employee? employee = EmployeeService.GetById(id ?? "");
                    if (employee == null) throw new Exception();
                    this.DisplayEmployees(new List<Employee> { employee });
                }
                catch (System.Exception)
                {
                    Console.WriteLine("Employee not found with given Id");
                }
            else
            {
                ConsoleUtility.PrintNoData();
            }
        }

        public void UpdateJobTitle(Employee employee)
        {
            employee.JobTitle = this.AssignRoleToEmployee();
        }

        public void DeleteEmployee()
        {
            DisplayEmployees();
            if (EmployeeService.GetAll().Count > 0)
            {
                Console.WriteLine("Enter your option");
                int option;
                Utility.GetOption(out option, EmployeeService.GetAll().Count);
                string? id = EmployeeService.GetAll().ElementAt(option - 1).Id;
                Employee employee = EmployeeService.GetById(id);
                if (employee != null)
                {
                    bool status = EmployeeService.DeleteByID(employee.Id);
                    if (status)
                        Console.WriteLine("Employee deleted successfully");
                    else
                        Console.WriteLine("Please try again!");
                }
                else
                {
                    Console.WriteLine("Employee not found");
                }
            }
        }

        public void DisplayEmployees()
        {
            if (EmployeeService.GetAll().Count == 0)
                ConsoleUtility.PrintNoData();
            else
                ConsoleUtility.PrintTableHead();
            foreach (Employee employee in EmployeeService.GetAll())
            {
                ConsoleUtility.PrintEmployeeRow(employee, RoleService.GetById(employee.JobTitle).Name);
                ConsoleUtility.PrintLine();
            }
        }

        public void DisplayEmployees(List<Employee> employees)
        {
            if (employees.Count == 0)
                ConsoleUtility.PrintNoData();
            else
                ConsoleUtility.PrintTableHead();
            foreach (Employee employee in employees)
            {
                Role? role = RoleService.GetById(employee.JobTitle);
                ConsoleUtility.PrintEmployeeRow(employee, role.Name);
                ConsoleUtility.PrintLine();
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
            };
            bool status = RoleService.Save(role);
            if (status)
                Console.WriteLine("Role Created Successfully");
            else
                Console.WriteLine("Please try again!");
        }

        public void EditRole()
        {
            if (RoleService.GetAll().Count == 0)
            {
                ConsoleUtility.PrintNoData();
            }
            else
            {
                List<string> roles = RoleService.GetRoles();
                for (int i = 0; i < roles.Count; i++)
                    Console.WriteLine("{0}.{1}", i + 1, roles.ElementAt(i));
                Console.WriteLine("Enter your option above");
                int optionId;
                Utility.GetOption(out optionId, roles.Count);
                try
                {
                    string id = roles.ElementAt(optionId - 1).Split(" ")[0];
                    Role? role = RoleService.GetById(id ?? "");
                    Console.WriteLine(Menus.EditRoleMenu);
                    int option;
                    Utility.GetOption(out option, 5);
                    bool status = this.UpdateRole(option, role);
                    if (status)
                        Console.WriteLine("Updated Successfully");
                    else
                        Console.WriteLine("Please try again!");
                }
                catch (System.Exception)
                {
                    Console.WriteLine("Role not found with given id");
                    this.EditRole();
                }
            }
        }

        public bool UpdateRole(int option, Role role)
        {
            try
            {
                switch ((EditRoleMenu)option)
                {
                    case EditRoleMenu.Name:
                        role.Name = Utility.GetInputString("Role name", true, null);
                        break;

                    case EditRoleMenu.Department:
                        role.Department = Utility.GetInputString("Department", true, null);
                        break;

                    case EditRoleMenu.Location:
                        role.Location = Utility.GetInputString("Location", true, null);
                        break;

                    case EditRoleMenu.Description:
                        role.Description = Utility.GetInputString("Description", true, null);
                        break;

                    case EditRoleMenu.Back:
                        this.RoleInitialize();
                        break;
                }
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public void DeleteRole()
        {
            if (RoleService.GetAll().Count == 0)
            {
                ConsoleUtility.PrintNoData();
            }
            else
            {
                List<string> roles = RoleService.GetRoles();
                for (int i = 0; i < roles.Count; i++)
                    Console.WriteLine("{0}.{1}", i + 1, roles.ElementAt(i));
                Console.WriteLine("Enter your option");
                int option;
                Utility.GetOption(out option, RoleService.GetAll().Count);
                string? id = RoleService.GetAll().ElementAt(option - 1).Id;
                Role? role = RoleService.GetById(id ?? "");
                if (role != null)
                {
                    if (this.GetAssignedEmployees(role.Id).Count > 0)
                    {
                        Console.WriteLine("{0} role contains employees. Please assign employees to another role and then try to delete the role", role.Name);
                    }
                    else
                    {
                        bool status = RoleService.DeleteById(id ?? "");
                        if (status)
                            Console.WriteLine("Role Deleted Successfully");
                        else
                            Console.WriteLine("Please try again!");
                    }
                }
                else
                {
                    Console.WriteLine("Role not found with given Id");
                }
            }
        }

        public List<Employee> GetAssignedEmployees(string Id)
        {
            return (from employee in EmployeeService.GetAll() where employee.JobTitle == Id select employee).ToList();
        }
        public void ViewRoles()
        {
            if (RoleService.GetAll().Count == 0)
            {
                ConsoleUtility.PrintNoData();
            }
            else
            {
                Console.WriteLine("+----------------------+");
                Console.WriteLine("|       Roles          |");
                Console.WriteLine("+----------------------+");
            }
            foreach (Role role in RoleService.GetAll())
            {
                Console.WriteLine("==============================================================================================================================================================");
                ConsoleUtility.PrintRoleHeader();
                ConsoleUtility.PrintRoleRow(role);
                this.DisplayEmployees(this.GetAssignedEmployees(role.Id));
            }
        }
    }
}