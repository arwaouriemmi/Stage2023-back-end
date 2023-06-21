using CityFix.Models;
using CityFix.Models.CityFix.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;


namespace CityFix.Data
{
    public class ImageRepository
    {
        private readonly ApplicationDbContext _context;
        public ImageRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public Img Add(Img img)

        {
      
            _context.Images.Add(img);
            _context.SaveChanges();
            return img;
        }
        public List<Img> ImagesByObservationId(int ObservationId)

        {
            List<Img> images = new List<Img>();
            try
            {
                
                images = _context.Images
                    .Where(i => i.ObservationId == ObservationId)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving images: " + ex.Message);
            }
            return images;
        }
        public Img Update(int id, Img updatedImage)
        {
            var img = _context.Images.Find(id);

            if (img == null)
            {
                throw new Exception("Image not found");
            }

            img.Src = updatedImage.Src;


           

            if (updatedImage.ObservationId != 0)
            {
                img.ObservationId = updatedImage.ObservationId;
            }





            _context.SaveChanges();
            return img;
        }

        public Img Delete(int id)
        {
            var img = _context.Images.Find(id);
            if (img == null)
            {
                throw new Exception("image not found");
            }


            _context.Images.Remove(img);
            _context.SaveChanges();

            return img;
        }



    }
}
