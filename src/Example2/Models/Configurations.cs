namespace Example2.Models
{
    public class Configurations
    {
        public Example2 Example2 { get; set; }
    }

    public class Example2
    {
        public SiteConfiguration SiteConfiguration { get; set; }
    }

    public class SiteConfiguration
    {
        public string BaseUrl { get; set; }

        public string Key { get; set; }
    }
}
