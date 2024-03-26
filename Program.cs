using Frontend;
using Services;
namespace MyApp
{
    public class Program
    {
        public static JsonService JsonService = new JsonService();
        static void Main()
        {
            new EmployeeDirectory().Initialize();
            // Console.WriteLine(JsonService.ReadRoles().Count);
            // foreach (Role role in JsonService.ReadRoles())
            //     Console.WriteLine(role.Name);
        }
    }

}