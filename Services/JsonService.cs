using System.Text.Json;
using Models;

namespace Services
{
    public class JsonService
    {
        public readonly string db = @"Database/db.json";

        public List<Employee> GetEmployees()
        {

            return JsonSerializer.Deserialize<JsonData>(File.ReadAllText(db))?.Employees!;
        }

        public bool UpdateEmployeeJson(List<Employee> Employees)
        {
            try
            {
                JsonData JsonData = JsonSerializer.Deserialize<JsonData>(File.ReadAllText(db))!;
                JsonData.Employees = Employees;
                string jsonString = JsonSerializer.Serialize(JsonData);
                File.WriteAllText(db, jsonString);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<Role> GetRoles()
        {
            return JsonSerializer.Deserialize<JsonData>(File.ReadAllText(db))?.Roles;
        }

        public bool UpdateRoleJson(List<Role> Roles)
        {
            try
            {
                JsonData JsonData = JsonSerializer.Deserialize<JsonData>(File.ReadAllText(db))!;
                JsonData.Roles = Roles;
                string jsonString = JsonSerializer.Serialize(JsonData);
                File.WriteAllText(db, jsonString);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}