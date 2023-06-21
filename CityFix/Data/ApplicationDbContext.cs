using CityFix.Models;
using CityFix.Models.CityFix.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace CityFix.Data
{
    public class ApplicationDbContext : DbContext
    {
        private static ApplicationDbContext instance;

        public static ApplicationDbContext Instance()
        {

            if (instance == null)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseMySQL("Server=localhost;User =root;Password=root;Database=CityFix");
                instance = new ApplicationDbContext(optionsBuilder.Options);

            }
            return instance;
        }





        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        
        public DbSet<Citoyen> Citoyens { get; set; }
        public DbSet<Observation> Observations { get; set; }
        public DbSet<Img> Images { get; set; }


    }
}


