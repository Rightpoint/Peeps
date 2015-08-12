using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.WindowsAzure.Mobile.Service;

namespace Rightpoint.Peeps.Server.DataObjects
{
    public class Peep : EntityData
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(30)]
        public string Team { get; set; }

        [MaxLength(100)]
        public string Hometown { get; set; }

        [Required, MaxLength(20)]
        public string Office { get; set; }

        public byte[] ImageBytes { get; set; }

        [NotMapped]
        public string ImagePath { get; set; }
    }
}
