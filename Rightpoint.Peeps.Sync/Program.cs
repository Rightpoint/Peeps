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
                    Name = "Ali Quadri",
                    Hometown = "Chicago, IL",
                    Team = "UX",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\AliQuadri.jpg"
                },
                new Peep
                {
                    Name = "Gina Lee",
                    Hometown = "Schaumburg, IL",
                    Team = "Creative",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\GinaLee.jpg"
                },
                new Peep
                {
                    Name = "Dan Costanzo",
                    Hometown = "Berkeley Heights, NJ",
                    Team = "BD",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\DanCostanzo.jpg"
                },
                new Peep
                {
                    Name = "Krystal Blesi",
                    Hometown = "Minnesota",
                    Team = "Salesforce",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\KrystalBlesi.jpg"
                },
                new Peep
                {
                    Name = "Austin Smith",
                    Hometown = "Deerfield, IL",
                    Team = "Creative",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\AustinSmith.jpg"
                },
                new Peep
                {
                    Name = "Tim Alvis",
                    Hometown = "Chicago, IL",
                    Team = "Ops",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\TimAlvis.jpg"
                },
                new Peep
                {
                    Name = "Tom Keuten",
                    Hometown = "Detroit, MI",
                    Team = "BD",
                    Office = "Novi",
                    ImagePath = @"c:\temp\people\TomKeuten.jpg"
                },
                new Peep
                {
                    Name = "Rob Mayer",
                    Hometown = "Chicago, IL",
                    Team = "Ops",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\RobMayer.jpg"
                },
                new Peep
                {
                    Name = "Leslie Lockett",
                    Hometown = "Canton, MI",
                    Team = "Delivery Support",
                    Office = "Novi",
                    ImagePath = @"c:\temp\people\LeslieLockett.jpg"
                },
                new Peep
                {
                    Name = "Syed Belgam",
                    Hometown = "Hydrabad, India",
                    Team = "Managed Services",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\SyedBelgam.jpg"
                },
                 new Peep
                {
                    Name = "Barbara Kubas",
                    Hometown = "Krakow, Poland",
                    Team = "App Dev",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\BarbaraKubas.jpg"
                },
                 new Peep
                {
                    Name = "Jay Mueller",
                    Hometown = "McHenry, IL",
                    Team = "Ops",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\JayMueller.jpg"
                },
                 new Peep
                {
                    Name = "Gautam Jaiswal",
                    Hometown = "Chicago, IL",
                    Team = "App Dev",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\GautamJaiswal.jpg"
                },
                 new Peep
                {
                    Name = "Steve Dykstra",
                    Hometown = "Northville, MI",
                    Team = "PMO",
                    Office = "Novi",
                    ImagePath = @"c:\temp\people\SteveDykstra.jpg"
                },
                 new Peep
                {
                    Name = "Carlee Wolfe",
                    Hometown = "Arlington Heights, IL",
                    Team = "Change Management",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\CarleeWolfe.jpg"
                },
                 new Peep
                {
                    Name = "Jimmy Hopton",
                    Hometown = "Basildon, UK",
                    Team = "Creative",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\JimmyHopton.jpg"
                },
                 new Peep
                {
                    Name = "Laurel Petty",
                    Hometown = "Sevierville, TN",
                    Team = "Creative",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\LaurelPetty.jpg"
                }
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
