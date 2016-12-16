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
                    Name = "Jason Alexander",
                    Hometown = "",
                    Team = "Managed Services",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\Jason Alexander.jpg"
                },
                new Peep
                {
                    Name = "Gautam Budidha",
                    Hometown = "",
                    Team = "Managed Services",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\Gautam Budidha.jpg"
                },
                new Peep
                {
                    Name = "Jack Davidson",
                    Hometown = "",
                    Team = "Account Management",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\Jack Davidson.jpeg"
                },
                new Peep
                {
                    Name = "Rebe De La Paza",
                    Hometown = "",
                    Team = "Salesforce",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\Rebe De La Paza.jpg"
                },
                new Peep
                {
                    Name = "Sruti Dhulipala",
                    Hometown = "",
                    Team = "Design & Strategy",
                    Office = "Los Angeles",
                    ImagePath = @"c:\temp\people\Sruti Dhulipala.jpg"
                },

                new Peep
                {
                    Name = "Sarah Doppelberger",
                    Hometown = "",
                    Team = "Alliances",
                    Office = "detroit",
                    ImagePath = @"c:\temp\people\Sarah Doppelberger.jpg"
                },
                new Peep
                {
                    Name = "Rizvi Hameed",
                    Hometown = "",
                    Team = "CMS",
                    Office = "Boston",
                    ImagePath = @"c:\temp\people\Rizvi Hameed.jpg"
                },
                new Peep
                {
                    Name = "Joe Hans",
                    Hometown = "",
                    Team = "App Dev",
                    Office = "Atlanta",
                    ImagePath = @"c:\temp\people\Joe Hans.jpg"
                },
                new Peep
                {
                    Name = "Nancy Hoag",
                    Hometown = "",
                    Team = "PMO",
                    Office = "Dallas",
                    ImagePath = @"c:\temp\people\Nancy Hoag.jpg"
                },
                new Peep
                {
                    Name = "Lew Jones",
                    Hometown = "",
                    Team = "Alliances",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\Lewis Jones.png"
                },

                new Peep
                {
                    Name = "Kevin Joyce ",
                    Hometown = "",
                    Team = "PMO",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\Kevin Joyce.jpg"
                },
                new Peep
                {
                    Name = "Jess Lewis",
                    Hometown = "",
                    Team = "QA",
                    Office = "Atlanta",
                    ImagePath = @"c:\temp\people\Jessica Lewis.jpg"
                },
                new Peep
                {
                    Name = "Jeff O'Neill",
                    Hometown = "",
                    Team = "Strategy",
                    Office = "Boston",
                    ImagePath = @"c:\temp\people\Jeff O'Neill.jpg"
                },
                new Peep
                {
                    Name = "Tom Quish",
                    Hometown = "",
                    Team = "Creative",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\Tom Quish.jpeg"
                },
                new Peep
                {
                    Name = "Melissa Ranalli",
                    Hometown = "",
                    Team = "PMO",
                    Office = "Boston",
                    ImagePath = @"c:\temp\people\Melissa Ranalli.jpg"
                },

                new Peep
                {
                    Name = "Camille Sharrow",
                    Hometown = "",
                    Team = "Account Manager",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\Camille Sharrow.jpg"
                },
                new Peep
                {
                    Name = "Delia Starr",
                    Hometown = "",
                    Team = "People Potential",
                    Office = "chicago",
                    ImagePath = @"c:\temp\people\Delia Starr.jpg"
                },
                new Peep
                {
                    Name = "Leigh Vitek",
                    Hometown = "",
                    Team = "Account Manager",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\Leigh Vitek.jpg"
                },
                new Peep
                {
                    Name = "Mariusz Walczuk",
                    Hometown = "",
                    Team = "Delivery Support",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\Mariusz Walczuk.png"
                },
                new Peep
                {
                    Name = "Ming Zhong",
                    Hometown = "",
                    Team = "App Dev",
                    Office = "Chicago",
                    ImagePath = @"c:\temp\people\Minghua Zhong.jpg"
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
