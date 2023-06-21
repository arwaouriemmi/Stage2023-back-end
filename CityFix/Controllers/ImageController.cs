using CityFix.Data;
using CityFix.Models;
using CityFix.Models.CityFix.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityFix.Controllers
{
    [Route("image")]
    public class ImageController : Controller
    {
        private readonly ILogger<ImageController> _logger;
        private readonly IConfiguration _configuration;

        public ImageController(ILogger<ImageController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        [HttpPost("add")]
        public Img Add([FromBody] Img img)

        {
            ApplicationDbContext ApplicationDbContext = ApplicationDbContext.Instance();
            ImageRepository ImageRepository = new ImageRepository(ApplicationDbContext);

            ImageRepository.Add(img);

            return img;

        }

        [HttpGet("ImagesByObservationId/{ObservationId}")]
        public List<Img> ImagesByObservationId(int ObservationId)

        {
            ApplicationDbContext ApplicationDbContext = ApplicationDbContext.Instance();
            ImageRepository ImageRepository = new ImageRepository(ApplicationDbContext);

            List<Img> images= ImageRepository.ImagesByObservationId(ObservationId);

            return images;


        }
        [HttpPatch("update/{id}")]
        public Img Update(int id, [FromBody] Img img)

        {
            ApplicationDbContext ApplicationDbContext = ApplicationDbContext.Instance();
            ImageRepository ImageRepository = new ImageRepository(ApplicationDbContext);
            Img NewImage = ImageRepository.Update(id, img);

            return NewImage;

        }
        [HttpDelete("delete/{id}")]
        public Img Delete(int id)

        {
            ApplicationDbContext ApplicationDbContext = ApplicationDbContext.Instance();
            ImageRepository ImageRepository = new ImageRepository(ApplicationDbContext);
            Img NewImage = ImageRepository.Delete(id);

            return NewImage;

        }


    }
}
