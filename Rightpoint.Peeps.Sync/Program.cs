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

            //TODO: we are abusing peeps by injecting events... a second model or refactor may be prudent
            List<Peep> peeps = new List<Peep>
            {
                //new Peep
                //{
                //    Name = "Event_20160423",
                //    Hometown ="Event_20160423",
                //    Team ="Event_20160423",
                //    Office ="Event_20160423",
                //    ImagePath = @"c:\temp\people\FeedStarvingChildren-11.jpg"
                //},
                new Peep
                {
                    Name = "Nerijus Baniukevicius",
                    Hometown = "Klaipeda, Lithuania",
                    Team = "App Dev",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\NerijusBaniukevicius.jpg"
                },
                new Peep
                {
                    Name = "Laraib Jafri",
                    Hometown = "Chicago, IL",
                    Team = "Chicago",
                    Office = "Salesforce",
                    ImagePath = @"c:\temp\people\LaraibJafri.jpg"
                },
                new Peep
                {
                    Name = "Gabe Streza.jpg",
                    Hometown = "Addison, IL",
                    Team = "Chicago",
                    Office = "Managed Services",
                    ImagePath = @"c:\temp\people\GabeStreza.jpg"
                },
                /*
                new Peep
                {
                    Name = "",
                    Hometown = "",
                    Team = "",
                    Office = "",
                    ImagePath = @"c:\temp\people\"
                },
                */
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
