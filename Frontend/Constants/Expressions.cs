namespace Frontend
{
    public class Expression
    {
        public static readonly string EmailPattern = @"[a-zA-Z0-9.-_]+@[a-zA-Z.-]+\.[a-zA-z]{2,4}$";
        public static readonly string mobilePattern = @"^[1-9]{1}[0-9]{9}$";
        public static readonly string datePattern = @"[0-9]{1,2}\/[0-9]{1,2}\/([0-9]{4}|[0-9]{2})$";
    }
}