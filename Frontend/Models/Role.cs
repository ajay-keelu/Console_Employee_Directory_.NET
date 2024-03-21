public class Role
{
    public string Name { get; set; } = "";

    public string Id { get; set; } = "";

    public string Department { get; set; } = "";

    public string Description { get; set; } = "";

    public string Location { get; set; } = "";

    public List<string> AssignedEmployeesId { get; set; } = new List<string>();

}