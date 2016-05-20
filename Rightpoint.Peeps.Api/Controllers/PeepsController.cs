using System.Collections.Generic;
using System.Web.Http;

namespace Rightpoint.Peeps.Api.Controllers
{
    public class PeepsController : ApiController
    {
        /// <summary>
        /// TODO: get peeps by args
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public IEnumerable<string> Get(string[] args)
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// TODO: get config file, which will inform the app what query parameters it should use for the week
        /// </summary>
        /// <returns></returns>
        [Route("api/peeps/config")]
        public string Get()
        {
            return "value";
        }
    }
}
