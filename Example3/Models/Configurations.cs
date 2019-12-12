namespace Example3.Models
{
    public class Configurations
    {
        public Example3 Example3 { get; set; }
    }

    public class Example3
    {
        public SiteConfiguration SiteConfiguration { get; set; }
    }

    public class SiteConfiguration
    {
        public string BaseUrl { get; set; }

        public string Key { get; set; }
    }
}
