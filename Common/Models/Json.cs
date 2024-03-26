namespace Models
{
    public class JsonData
    {
        public List<Employee> Employees { get; set; }
        public List<Role> Roles { get; set; }
        public JsonData()
        {
            Employees = new List<Employee>();
            Roles = new List<Role>();
        }
    }
}