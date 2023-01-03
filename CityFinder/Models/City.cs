using System.ComponentModel.DataAnnotations;

namespace CityFinder.Models
{
    public class City
    {
        [Display(Prompt ="Enter the city that you want to search")]
        [Required(ErrorMessage ="You must enter a city")]
        [MaxLength(32)]
        public String Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public String Country { get; set; }
        public String State { get; set; }
    }
}
