using CityFix.Models;
using CityFix.Models.CityFix.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace CityFix.Data
{
    public class ObservationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly CitoyenRepository CitoyenRepository;
        public ObservationRepository(ApplicationDbContext context)
        {
            this._context = context;
            this.CitoyenRepository = new CitoyenRepository(context);
        }
        public Observation Add(Observation observation)

        {
         
            _context.Observations.Add(observation);
            _context.SaveChanges();
            return observation;
        }
        public List<Observation> ObservationsByCitoyenId(int CitoyenId)

        {
            List<Observation> observations = new List<Observation>();
            try
            {
            
                observations = _context.Observations
                    .Where(o => o.CitoyenId == CitoyenId)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving observations: " + ex.Message);
            }
            return observations;
        }
        public Observation Update(int id, Observation updatedObservation)
        {
            var existingObservation = _context.Observations.Find(id);
            if (existingObservation == null)
            {
                throw new Exception("Observation not found");
            }
            existingObservation.Date = updatedObservation.Date;
            existingObservation.Localisation = updatedObservation.Localisation;
            existingObservation.Text = updatedObservation.Text;
            existingObservation.Images = updatedObservation.Images ?? existingObservation.Images;
            if (updatedObservation.CitoyenId != 0)
            {
                existingObservation.CitoyenId = updatedObservation.CitoyenId;
            }


            _context.SaveChanges();

            return existingObservation;
        }

        public Observation Delete(int id)
        {
            var observation = _context.Observations.Find(id);
            if (observation == null)
            {
                throw new Exception("observation not found");
            }


            _context.Observations.Remove(observation);
            _context.SaveChanges();

            return observation;
        }




    }
}
