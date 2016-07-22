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
                //    Name = "Event_20160604",
                //    Hometown ="Event_20160604",
                //    Team ="Event_20160604",
                //    Office ="Event_20160604",
                //    ImagePath = @"c:\temp\people\June_Events-01-1.jpg"
                //},
                new Peep
                {
                    Name = "Andrew Barnett",
                    Hometown = "Grosse Pointe, MI",
                    Team = "App Dev",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\Andrew Barnett.png"
                },
                new Peep
                {
                    Name = "Jordan Magenta",
                    Hometown = "Ann Arbor, MI",
                    Team = "Marketing",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\Jordan Magenta.jpg"
                },
                new Peep
                {
                    Name = "Shay Depies",
                    Hometown = "Alpharetta, GA",
                    Team = "Client Partner",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\Shay Depies.JPG"
                },
                new Peep
                {
                    Name = "Siouxzi Donnelly",
                    Hometown = "Kokomo, IN ",
                    Team = "Content Strategy",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\Siouxzi Donnelly.JPG"
                },
                new Peep
                {
                    Name = "Andrew Gilbert",
                    Hometown = "Chicago, IL",
                    Team = "Delivery Support",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\Andrew Gilbert.JPG"
                },
                new Peep
                {
                    Name = "Brendan Carlson",
                    Hometown = "Grand Blanc, MI",
                    Team = "App Dev",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\Brendan Carlson.jpg"
                },
                new Peep
                {
                    Name = "Ryan Michalec",
                    Hometown = " Lake Orion, MI",
                    Team = "App Dev",
                    Office = "Michigan",
                    ImagePath = @"c:\temp\people\Ryan Michalec.jpg"
                },
                new Peep
                {
                    Name = "Akshat Sangal",
                    Hometown = "Muzaffarnagar, India",
                    Team = "Salesforce",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\Akshat Sangal.jpg"
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
