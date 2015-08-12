using System.Collections.Generic;
using Rightpoint.Peeps.Sync.Models;

namespace Rightpoint.Peeps.Sync
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Peep> peeps = new List<Peep>
            {
                new Peep
                {
                    Name = "John Doe",
                    ImagePath = @"c:\temp\peeps\person.bmp"
                },
                new Peep
                {
                    Name = "Jane Doe",
                    ImagePath = @"c:\temp\peeps\person.bmp"
                }
            };

            foreach (Peep peep in peeps)
            {
                // load image to byte array

                // replace peep.ImageBytes with byte array
            }

            // sync with mobile service
        }
    }
}
