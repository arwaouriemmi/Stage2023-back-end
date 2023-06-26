using CityFix.Data;
using CityFix.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BCrypt.Net;
using System.Data.Entity;
using OpenQA.Selenium;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        [HttpGet("GetById/{id}")]
        
        public Citoyen GetById(int id)

        {
         
            ApplicationDbContext ApplicationDbContext = ApplicationDbContext.Instance();
        
            CitoyenRepository CitoyenRepository = new CitoyenRepository(ApplicationDbContext);
            Citoyen citoyen=CitoyenRepository.Find(id);

            return citoyen;

        }

        [Authorize]
        [HttpGet("GetAll")]
        public List<Citoyen> GetAll()

        {
            ApplicationDbContext ApplicationDbContext = ApplicationDbContext.Instance();
            CitoyenRepository CitoyenRepository = new CitoyenRepository(ApplicationDbContext);
            List<Citoyen> citoyens = CitoyenRepository.GetAllCitoyens();

            return citoyens;

        }


        [Authorize]
        [HttpPatch("update/{id}")]
        
        public Citoyen Update(int id,[FromBody] Citoyen citoyen)

        {
            ApplicationDbContext ApplicationDbContext = ApplicationDbContext.Instance();
            CitoyenRepository CitoyenRepository = new CitoyenRepository(ApplicationDbContext);
            Citoyen NewCitoyen=CitoyenRepository.Update(id, citoyen);

            return NewCitoyen;

        }
       


        [Authorize]
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
