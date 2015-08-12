namespace Rightpoint.Peeps.Client.Models
{
    public class Peep : Entity
    {
        public string Name { get; set; }

        public string Team { get; set; }

        public string Hometown { get; set; }

        public string Office { get; set; }

        public byte[] ImageBytes { get; set; }
    }
}
