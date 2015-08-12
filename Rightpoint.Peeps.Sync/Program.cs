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
                    Name = "Nick Groos",
                    Hometown = "Grand Rapids, MI",
                    Team = "App Dev",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\NickGroos.jpg"
                },
                new Peep
                {
                    Name = "Dev Deol",
                    Hometown = "Arlington Heights, IL",
                    Team = "App Dev",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\DevDeol.jpg"
                },
                new Peep
                {
                    Name = "Steve Mierop",
                    Hometown = "La Grange, IL",
                    Team = "App Dev",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\SteveMierop.jpg"
                },
                new Peep
                {
                    Name = "Alex Zebrov",
                    Hometown = "Narva, Estonia",
                    Team = "Managed Services",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\AlexZebrov.jpg"
                },
                new Peep
                {
                    Name = "Bartel Welch",
                    Hometown = "Detroit, MI",
                    Team = "App Dev",
                    Office = "Detroit",
                    ImagePath = @"c:\temp\people\BartelWelch.jpg"
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
