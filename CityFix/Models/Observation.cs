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
            public string Localisation { get; set; }

            public string Text{ get; set; }

            public ICollection<Img> Images { get; set; }
            public int CitoyenId { get; set; }

        }
    }

}

