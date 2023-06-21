using CityFix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;


namespace CityFix.Data
{
    public class CitoyenRepository
    {
        private readonly ApplicationDbContext _context;
        public CitoyenRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public Citoyen Register(Citoyen citoyen)
        {
            _context.Citoyens.Add(citoyen);
            _context.SaveChanges();
            return citoyen;
        }
        public Citoyen Find(int  id)
        {
            var Citoyen = _context.Citoyens.Find(id);
            if (Citoyen == null)
            {
                throw new Exception("Citoyen not found");
            }
            return Citoyen;
        }
        public List<Citoyen> GetAllCitoyens()
        {
            var citoyens = _context.Citoyens.ToList();

            if (citoyens.Count == 0)
            {
                throw new Exception("No citoyens found");
            }

            return citoyens;
        }

        public Citoyen Update(int id, Citoyen updatedCitoyen)
        {
            var existingCitoyen = _context.Citoyens.Find(id);
            if (existingCitoyen == null)
            {
                throw new Exception("Citoyen not found");
            }

            existingCitoyen.NomComplet = updatedCitoyen.NomComplet ?? existingCitoyen.NomComplet;
            existingCitoyen.Password = updatedCitoyen.Password ?? existingCitoyen.Password;
            existingCitoyen.Email = updatedCitoyen.Email ?? existingCitoyen.Email;
            existingCitoyen.Cin = updatedCitoyen.Cin ?? existingCitoyen.Cin;
            existingCitoyen.Tel = updatedCitoyen.Tel ?? existingCitoyen.Tel;
            existingCitoyen.Image = updatedCitoyen.Image ?? existingCitoyen.Image;
            existingCitoyen.Observations = updatedCitoyen.Observations ?? existingCitoyen.Observations;

            _context.SaveChanges();

            return existingCitoyen;
        }

        public Citoyen Delete(int id)
        {
            var citoyen = _context.Citoyens.Find(id);
            if (citoyen == null)
            {
                throw new Exception("Citoyen not found");
            }


            _context.Citoyens.Remove(citoyen);
            _context.SaveChanges();

            return citoyen;
        }



    }
}
