public class Role
{
    public string Name { get; set; } = "";

    public string Id { get; set; } = "";

    public string Department { get; set; } = "";

    public string Description { get; set; } = "";

    public string Location { get; set; } = "";

    public List<string> AssignedEmployees { get; set; }

    public bool IsActive { get; set; }

    public Role()
    {
        this.Location = string.Empty;
        this.AssignedEmployees = new List<string>();
        this.Department = string.Empty;
        this.Description = string.Empty;
        this.IsActive = true;
    }
}