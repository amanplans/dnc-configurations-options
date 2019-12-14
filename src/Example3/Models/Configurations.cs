using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "The BaseUrl is required for the SiteConfiguration section")]
        public string BaseUrl { get; set; }

        [Required(ErrorMessage = "The Key is required for the SiteConfiguration section")]
        public string Key { get; set; }
    }
}
