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
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;

        public AuthController(ILogger<AuthController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<Citoyen> RegisterAsync([FromForm] IFormFile img, [FromForm] string citoyenJson)

        {
            ApplicationDbContext ApplicationDbContext = ApplicationDbContext.Instance();
            CitoyenRepository CitoyenRepository = new CitoyenRepository(ApplicationDbContext);
            try
            {

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


                citoyen.Password = BCrypt.Net.BCrypt.HashPassword(citoyen.Password);
                Citoyen AddedCitoyen = CitoyenRepository.Register(citoyen);

                return AddedCitoyen;


            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred: {ex.Message}");


                return null;

            }




        }
        [HttpPost("login")]
        public async Task<string> Login([FromBody] LoginCredentials LoginCredentials)
        {
            string login = LoginCredentials.Login;
            string password = LoginCredentials.Password;
            ApplicationDbContext ApplicationDbContext = ApplicationDbContext.Instance();
            CitoyenRepository CitoyenRepository = new CitoyenRepository(ApplicationDbContext);
            var citoyen = ApplicationDbContext.Citoyens
                          .Where(c => c.NomComplet == login || c.Email == login)
                          .FirstOrDefault();



            if (citoyen == null)
            {

                throw new NotFoundException("Le login ou le mot de passe est incorrect.");
            }

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(LoginCredentials.Password, citoyen.Password);


            if (isPasswordCorrect)
            {
                var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
           {
                new Claim(ClaimTypes.NameIdentifier, citoyen.Id.ToString()),
                new Claim(ClaimTypes.Email, citoyen.Email),
                new Claim(ClaimTypes.Name, citoyen.NomComplet)

            };
                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
        _configuration["Jwt:Audience"],
       claims,
       expires: DateTime.Now.AddMinutes(15),
       signingCredentials: credentials);



                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                throw new NotFoundException("Le login ou le mot de passe est incorrect.");
            }
        }

    }
}
