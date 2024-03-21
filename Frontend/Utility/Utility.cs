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

        public string GetInputString(string propertyName, bool? isRequired, string? pattern)
        {
            string? s;
            try
            {
                Console.WriteLine("Enter {0}{1}", propertyName, isRequired != null && isRequired == true ? "*" : ' ');
                s = Console.ReadLine()?.Trim();
                if (isRequired != null && isRequired == true && string.IsNullOrEmpty(s)) throw new Exception();
                if (pattern != null && !IsValidProperty(s ??= "", pattern)) throw new Exception();

                return s ??= "";
            }
            catch (System.Exception)
            {
                Console.WriteLine("Please enter {0} correctly ... ", propertyName);
                return this.GetInputString(propertyName, isRequired, pattern);
            }
        }

        public string GetInputDate(string propertyName, bool? isRequired)
        {
            string? s;
            try
            {
                Console.WriteLine("Enter {0}{1}", propertyName, isRequired != null && isRequired == true ? "*" : " ");
                s = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(s) && !this.IsValidProperty(s, Expression.datePattern)) throw new Exception();

                if (isRequired != null && isRequired == true && !this.IsValidProperty(s ??= "", Expression.datePattern)) throw new Exception();

                return s ??= "";
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
                Console.WriteLine("Enter Email*");
                email = Console.ReadLine()?.Trim();
                if (!this.IsValidProperty(email ??= "", Expression.EmailPattern)) throw new Exception();

                return email;
            }
            catch (System.Exception)
            {
                Console.WriteLine("Please enter Email correctly ... ");
                return this.GetInputEmail();
            }
        }

        public string GetMobileNumber()
        {
            try
            {
                Console.WriteLine("Enter mobile number ");
                string? mobile = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(mobile) && !this.IsValidProperty(mobile, Expression.mobilePattern)) throw new Exception();
                return mobile ??= "";
            }
            catch (System.Exception)
            {
                Console.WriteLine("Enter mobile number correctly");
                return this.GetMobileNumber();
            }
        }

    }
}