using System.ComponentModel.DataAnnotations;

namespace CityFix.Models
{
  

namespace CityFix.Models
    {
        public class Observation
        {
            [Key]
            public int Id { get; set; }
          
            public DateTime Date { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }

            public string Text{ get; set; }

            public ICollection<Img> Images { get; set; }
            public int CitoyenId { get; set; }

        }
    }

}

