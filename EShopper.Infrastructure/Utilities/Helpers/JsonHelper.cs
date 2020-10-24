using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EShopper.Infrastructure.Utilities.Helpers
{
    public static class JsonHelper
    {
        public static bool ValidateJSON(this string s)
        {
            bool response;
            try
            {
                JToken.Parse(s);
                response = true;
            }
            catch (JsonReaderException)
            {
                response = false;
            }
            return response;
        }
    }
}