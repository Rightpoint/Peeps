using System.Collections.Generic;

namespace Rightpoint.Peeps.Api.Models
{
    public class Company
    {
        public ICollection<Peep> Peeps { get; set; }
    }
}