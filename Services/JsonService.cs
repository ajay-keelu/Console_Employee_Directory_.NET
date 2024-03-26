using System.Text.Json;
using Models;

namespace Services
{
    public class JsonService
    {
        public readonly string Storage = @"Database/Storage.json";

        public List<Employee> ReadEmployees()
        {

            return JsonSerializer.Deserialize<JsonData>(File.ReadAllText(Storage))?.Employees!;
        }

        public bool UpdateEmployeeJson(List<Employee> Employees)
        {
            try
            {
                JsonData JsonData = JsonSerializer.Deserialize<JsonData>(File.ReadAllText(Storage))!;
                JsonData.Employees = Employees;
                string jsonString = JsonSerializer.Serialize(JsonData);
                File.WriteAllText(Storage, jsonString);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<Role> ReadRoles()
        {
            return JsonSerializer.Deserialize<JsonData>(File.ReadAllText(Storage))?.Roles;
        }

        public bool UpdateRoleJson(List<Role> Roles)
        {
            try
            {
                JsonData JsonData = JsonSerializer.Deserialize<JsonData>(File.ReadAllText(Storage))!;
                JsonData.Roles = Roles;
                string jsonString = JsonSerializer.Serialize(JsonData);
                File.WriteAllText(Storage, jsonString);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}