using CityFix.Models.CityFix.Models;
using System.ComponentModel.DataAnnotations;

namespace CityFix.Models
{
    public class Img
    {
        [Key]
        public int Id { get; set; }
        public string Src { get; set; }

        public int ObservationId { get; set; }
      
    }
}
