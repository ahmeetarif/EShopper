namespace EShopper.UI.Options
{
    public class EShopperApiOptions
    {
        public string BaseAddress { get; set; }
        public string Version { get; set; }
        public RequestUrls RequestUrls { get; set; }
    }

    public class RequestUrls
    {
        public string Login { get; set; }
        public string Register { get; set; }
    }
}