using System.Globalization;
using System.Text.RegularExpressions;

namespace Frontend
{
    public class Utility
    {
        public void GetOption(out int option, int options)
        {
            try
            {
                option = int.Parse(Console.ReadLine().Trim());
                if (option <= 0 || option > options) throw new Exception();
            }
            catch (System.Exception)
            {
                Console.WriteLine("\nEnter options from above :)");
                this.GetOption(out option, options);
            }
        }

        public bool IsValidProperty(string s, string pattern)
        {
            return Regex.IsMatch(s, pattern);
        }

        public string GetInputString(string propertyName, bool isRequired, string? pattern)
        {
            string? s;
            try
            {
                Console.WriteLine("Enter {0}{1}", propertyName, isRequired ? "*" : "");
                s = Console.ReadLine()?.Trim();
                if (isRequired && string.IsNullOrEmpty(s)) throw new Exception();
                if (pattern != null && !IsValidProperty(s ??= "", pattern)) throw new Exception();

                return s;
            }
            catch (System.Exception)
            {
                Console.WriteLine("Please enter {0} correctly ... ", propertyName);
                return this.GetInputString(propertyName, isRequired, pattern);
            }
        }

        public DateTime GetInputDate(string propertyName, bool isRequired)
        {
            string? date;
            try
            {
                Console.WriteLine("Enter {0}{1}", propertyName, isRequired ? "*" : "");
                date = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(date) && !this.IsValidProperty(date, RegularExpression.DatePattern)) throw new Exception();

                if (isRequired && !this.IsValidProperty(date ??= "", RegularExpression.DatePattern)) throw new Exception();
                return DateTime.Parse(date is null or "" ? DateTime.MinValue.ToString("dd/MM/yyyy") : date);
            }
            catch (System.Exception)
            {
                Console.WriteLine("Please enter {0} correctly ... ", propertyName);
                return this.GetInputDate(propertyName, isRequired);
            }
        }

        public string GetInputEmail()
        {
            string? email;
            try
            {
                Console.WriteLine("Enter email*");
                email = Console.ReadLine()?.Trim();
                if (!this.IsValidProperty(email ??= "", RegularExpression.EmailPattern)) throw new Exception();
                return email;
            }
            catch (System.Exception)
            {
                Console.WriteLine("Please enter email correctly ... ");
                return this.GetInputEmail();
            }
        }

        public string GetMobileNumber()
        {
            try
            {
                Console.WriteLine("Enter mobile number ");
                string? mobile = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(mobile ??= "") && !this.IsValidProperty(mobile, RegularExpression.MobilePattern)) throw new Exception();
                return mobile;
            }
            catch (System.Exception)
            {
                Console.WriteLine("Enter mobile number correctly");
                return this.GetMobileNumber();
            }
        }

    }
}