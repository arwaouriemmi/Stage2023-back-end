using Amazon.Runtime.Internal;
using CityFix.Data;
using CityFix.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CityFix.Controllers
{
    [Route("citoyen")]
    public class CitoyenController : Controller

    {
        private readonly ILogger<CitoyenController> _logger;
        private readonly IConfiguration _configuration;

        public CitoyenController(ILogger<CitoyenController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        [HttpGet("GetById/{id}")]
        public Citoyen GetById(int id)

        {
         
            ApplicationDbContext ApplicationDbContext = ApplicationDbContext.Instance();
        
            CitoyenRepository CitoyenRepository = new CitoyenRepository(ApplicationDbContext);
            Citoyen citoyen=CitoyenRepository.Find(id);

            return citoyen;

        }
        [HttpGet("GetAll")]
        public List<Citoyen> GetAll()

        {
            ApplicationDbContext ApplicationDbContext = ApplicationDbContext.Instance();
            CitoyenRepository CitoyenRepository = new CitoyenRepository(ApplicationDbContext);
            List<Citoyen> citoyens = CitoyenRepository.GetAllCitoyens();

            return citoyens;

        }
       
        

        [HttpPatch("update/{id}")]
        public Citoyen Update(int id,[FromBody] Citoyen citoyen)

        {
            ApplicationDbContext ApplicationDbContext = ApplicationDbContext.Instance();
            CitoyenRepository CitoyenRepository = new CitoyenRepository(ApplicationDbContext);
            Citoyen NewCitoyen=CitoyenRepository.Update(id, citoyen);

            return NewCitoyen;

        }
        [HttpPost("register")]
        public async Task<Citoyen> RegisterAsync([FromForm] IFormFile img, [FromForm] string citoyenJson)

        {
            ApplicationDbContext ApplicationDbContext = ApplicationDbContext.Instance();
            CitoyenRepository CitoyenRepository = new CitoyenRepository(ApplicationDbContext);
            try {

                Citoyen citoyen = JsonConvert.DeserializeObject<Citoyen>(citoyenJson);

                if (img != null)
                {

                    var fileName = Generics.GenerateUniqueFileName(img.FileName);


                    var filePath = Path.Combine("../CityFix/Images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await img.CopyToAsync(stream);
                    }


                    citoyen.Image = fileName;
                }

                CitoyenRepository.Register(citoyen);

                return citoyen;
            }
            catch (Exception ex)
            {
             
                Console.WriteLine($"An error occurred: {ex.Message}");


                return null;

            }
          
            


        }
        [HttpDelete("delete/{id}")]
        public Citoyen Delete (int id)

        {
            ApplicationDbContext ApplicationDbContext = ApplicationDbContext.Instance();
            CitoyenRepository CitoyenRepository = new CitoyenRepository(ApplicationDbContext);
            Citoyen NewCitoyen = CitoyenRepository.Delete(id);

            return NewCitoyen;

        }
      

       

       

       
     

      

       
       

        

       
    }
}
