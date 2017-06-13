using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rightpoint.Peeps.Api.Models
{
    public class GraphNode<T> where T : IGraphObject
    {
        //public string odata.metadata { get; set; }
        public IList<IGraphObject> value { get; set; }
        //public string odata.nextLink { get; set; }
    }
}