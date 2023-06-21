using Amazon.Runtime.Internal;
using CityFix.Data;
using CityFix.Models;
using CityFix.Models.CityFix.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CityFix.Controllers
{
    [Route("observation")]
    public class ObservationController : Controller
    {
        private readonly ILogger<ObservationController> _logger;
        private readonly IConfiguration _configuration;

        public ObservationController(ILogger<ObservationController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
    
        [HttpPost("add")]
        public async Task<Observation> AddAsync ([FromForm] ICollection<IFormFile> images, [FromForm] string observationJson)

        {
            ApplicationDbContext ApplicationDbContext = ApplicationDbContext.Instance();
            ObservationRepository ObservationRepository = new ObservationRepository(ApplicationDbContext);
            ImageRepository ImageRepository= new ImageRepository(ApplicationDbContext);
            try
            {
                Observation observation = JsonConvert.DeserializeObject<Observation>(observationJson);
                Observation AddedObservation=ObservationRepository.Add(observation);
                if (images != null && images.Count > 0)
                {
                    foreach (var img in images)
                    {
                        var fileName = Generics.GenerateUniqueFileName(img.FileName);


                        var filePath = Path.Combine("../CityFix/Images", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await img.CopyToAsync(stream);
                        }
                        var imgObject = new Img
                        {
                            Src = fileName,
                            ObservationId = AddedObservation.Id
                        };
                        ImageRepository.Add(imgObject);
                        


                    }
                   

                }
             
                return observation;


            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");


                return null;
            }

        }

        [HttpGet("ObservationsByCitoyenId/{CitoyenId}")]
        public List<Observation> ObservationsByCitoyenId(int CitoyenId)

        {
            ApplicationDbContext ApplicationDbContext = ApplicationDbContext.Instance();
            ObservationRepository ObservationRepository = new ObservationRepository(ApplicationDbContext);

            List<Observation> observations= ObservationRepository.ObservationsByCitoyenId(CitoyenId);

            return observations;


        }
        [HttpPatch("update/{id}")]
        public Observation Update(int id, [FromBody] Observation observation)

        {
            ApplicationDbContext ApplicationDbContext = ApplicationDbContext.Instance();
            ObservationRepository ObservationRepository = new ObservationRepository(ApplicationDbContext);
            Observation NewObservation = ObservationRepository.Update(id, observation);

            return NewObservation;

        }
        [HttpDelete("delete/{id}")]
        public Observation Delete(int id)

        {
            ApplicationDbContext ApplicationDbContext = ApplicationDbContext.Instance();
            ObservationRepository ObservationRepository = new ObservationRepository(ApplicationDbContext);
            Observation NewObservation = ObservationRepository.Delete(id);

            return NewObservation;

        }















    }
}
