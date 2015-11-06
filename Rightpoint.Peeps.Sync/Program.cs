using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Rightpoint.Peeps.Server.DataObjects;
using Rightpoint.Peeps.Server.Models;

namespace Rightpoint.Peeps.Sync
{
    class Program
    {
        private static readonly MobileServiceContext DbContext = new MobileServiceContext();

        static void Main(string[] args)
        {
            var currentPeeps = DbContext.Peeps.AsEnumerable().ToList();

            List<Peep> peeps = new List<Peep>
            {
                new Peep
                {
                    Name = "Jesse Wilbur",
                    Hometown = "Chicago, IL",
                    Team = "Agency",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\JesseWilbur.jpg"
                },
                new Peep
                {
                    Name = "Josh Chung",
                    Hometown = "Seoul, Korea",
                    Team = "BI",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\JoshChung.jpeg"
                },
                new Peep
                {
                    Name = "Eli Albert",
                    Hometown = "Teaneck, NJ",
                    Team = "App Dev",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\EliAlbert.jpeg"
                },
                new Peep
                {
                    Name = "Nicole Lambiase",
                    Hometown = "West Palm Beach, FL",
                    Team = "Managed Services",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\NicoleLambiase.jpg"
                },
                new Peep
                {
                    Name = "Addam Wassel",
                    Hometown = "Northville, MI",
                    Team = "App Dev",
                    Office = "Michigan",
                    ImagePath = @"c:\temp\people\AddamWassel.jpg"
                },
                new Peep
                {
                    Name = "Robin Schaffer",
                    Hometown = "Denver, CO",
                    Team = "Denver",
                    Office = "Denver",
                    ImagePath = @"c:\temp\people\Robin.jpg"
                },
                new Peep
                {
                    Name = "Allie Gauthier",
                    Hometown = "Coopersville, MI",
                    Team = "Delivery Support",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\AllieGauthier.jpg"
                },
                new Peep
                {
                    Name = "Jason McDermott",
                    Hometown = "Erie, PA",
                    Team = "App Dev",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\JasonMcDermott.jpg"
                },
                new Peep
                {
                    Name = "Kimberly Arakelian",
                    Hometown = "Lansing, MI",
                    Team = "Content Strategy",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\KimberlyArakelian.jpg"
                },
            };

            foreach (Peep peep in peeps)
            {
                peep.ImageBytes = ImagePathToByteArray(peep.ImagePath);

                var currentPeep =
                    currentPeeps.SingleOrDefault(
                        p => String.Equals(peep.Name, p.Name, StringComparison.InvariantCultureIgnoreCase));

                if (currentPeep == null)
                {
                    peep.Id = Guid.NewGuid().ToString();
                    peep.CreatedAt = DateTime.UtcNow;
                    peep.UpdatedAt = DateTime.UtcNow;
                    peep.Deleted = false;

                    DbContext.Peeps.Add(peep);

                    Console.WriteLine($"Adding {peep.Name}");
                }
                else
                {
                    currentPeep.Name = peep.Name;
                    currentPeep.Hometown = peep.Hometown;
                    currentPeep.Team = peep.Team;
                    currentPeep.Office = peep.Office;
                    currentPeep.ImageBytes = peep.ImageBytes;

                    currentPeep.UpdatedAt = DateTime.UtcNow;

                    Console.WriteLine($"Updating {peep.Name}");
                }
            }

            try
            {
                DbContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw;
            }
        }

        private static byte[] ImagePathToByteArray(string imagePath)
        {
            Image image = Image.FromFile(imagePath);
            byte[] imageBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                imageBytes = ms.ToArray();
            }

            return imageBytes;
        }
    }
}
