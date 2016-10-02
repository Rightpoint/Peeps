
namespace Rightpoint.Peeps.Api.Models
{
    public class Peep
    {
        public string Name { get; set; }

        public string ServiceLine { get; set; }

        public string Origin { get; set; }

        public string Office { get; set; }

        public string Photo { get; set; }

        public string PhotoPath => Constants.ImagePath + Photo;
    }
}