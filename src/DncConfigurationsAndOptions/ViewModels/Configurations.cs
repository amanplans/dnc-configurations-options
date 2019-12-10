namespace DncConfigurationsAndOptions.ViewModels
{
    public class Configurations
    {
        public Example1 Example1 { get; set; }  
    }

    public class Example1
    {
        public SiteConfiguration SiteConfiguration { get; set; }    
    }

    public class SiteConfiguration
    {
        public string BaseUrl { get; set; }

        public string Key { get; set; }
    }
}
