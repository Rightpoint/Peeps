using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using Newtonsoft.Json;
using Rightpoint.Peeps.Api.Models;

namespace Rightpoint.Peeps.Api.Controllers
{
    public class PeepsController : ApiController
    {
        /// <summary>
        /// TODO: get peeps by args
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public HttpResponseMessage Get([FromUri]string args)
        {
            var appData = HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data");
            string file = Path.Combine(appData, Constants.PeepsFile);

            using (var sr = new StreamReader(file))
            {
                var result = JsonConvert.DeserializeObject<Company>(sr.ReadToEnd());

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// TODO: get config file, which will inform the app what query parameters it should use for the week
        /// </summary>
        /// <returns></returns>
        [Route("api/peeps/config")]
        public string Get()
        {            
            return WebConfigurationManager.AppSettings["PeepsQuery"];
        }
    }
}
