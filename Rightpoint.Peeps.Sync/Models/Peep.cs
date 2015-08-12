using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rightpoint.Peeps.Sync.Models
{
    public class Peep
    {
        public string Name { get; set; }

        public string ImagePath { get; set; }

        public byte[] ImageBytes { get; set; }
    }
}
