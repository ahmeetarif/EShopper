namespace EShopper.Infrastructure.Utilities
{
    public static class StringExtension
    {
        public static bool CheckNullsAndWhitespace(this string[] ff,params string[] fileds)
        {
            foreach (var text in fileds)
            {
                if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
                {
                    return false;
                }
            }

            return true;
        }
    }
}