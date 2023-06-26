using CityFix.Models.CityFix.Models;
using System.Collections.Generic;

namespace CityFix.Models
{
    public class Citoyen
    {
    

        public int Id { get; set; }
        public string NomComplet { get; set; }
        public string Password { get; set; }

       

        public string Email { get; set; }
        public string Cin { get; set; }
        public string Tel { get; set; }
        public string? Image { get; set; }
        public ICollection<Observation>? Observations { get; set; }
    }
}



