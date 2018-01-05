using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using Newtonsoft.Json;
using Rightpoint.Peeps.Api.Models;
using Rightpoint.Peeps.Api.Services;

namespace Rightpoint.Peeps.Api.Controllers
{
    public class PeepsController : ApiController
    {
        private GraphApiService service = new GraphApiService();

        public PeepsController()
        {

        }

        /// <summary>
        /// TODO: get peeps by args
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public HttpResponseMessage Get([FromUri]string[] args)
        {
            var appData = HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data");
            string file = Path.Combine(appData, Constants.PeepsFile);

            using (var sr = new StreamReader(file))
            {
                ICollection<Peep> peepsByArgs;
                var result = JsonConvert.DeserializeObject<Company>(sr.ReadToEnd());

                if (args.Length > 0)
                {
                    //TODO filter by args
                }

                // Randomize order and return no more than 50 peeps
                peepsByArgs = result.Peeps.OrderBy(x => Guid.NewGuid()).Take(30).ToList();

                DoSpookyScary(peepsByArgs);

                return Request.CreateResponse(HttpStatusCode.OK, new Company()
                {
                    Peeps = peepsByArgs
                });
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

        [Route("api/peeps/graph")]
        public HttpResponseMessage GetFromGraph([FromUri]string[] args)
        {
            throw new NotImplementedException();

            // TODO JM: Configure OAuth (App registration + flow)
            var result = service.FetchUsers(null, 30).Result;

            return Request.CreateResponse(HttpStatusCode.OK, new Company()
            {
                Peeps = result.ToList()
            });
        }

        private void DoSpookyScary(ICollection<Peep> peepsByArgs)
        {
            // Special Spooky Halloween Pictures
            var spookyPic = new List<string>
                {
                    "cat.jpg",
                    "ghost.jpg",
                    "jack.jpg",
                    "skeleton.jpg",
                    "cat.jpg",
                    "ghost.jpg",
                    "jack.jpg",
                    "skeleton.jpg",
                    "cat.jpg",
                    "ghost.jpg",
                    "jack.jpg",
                    "skeleton.jpg"
                };

            var random = new Random();
            var dayNumber = DateTime.Now.DayOfYear;
            var spookyStart = 302;
            var spookyEnd = 305;
            if (dayNumber >= spookyStart && dayNumber < spookyEnd)
            {
                foreach (var peep in peepsByArgs)
                {
                    int index = random.Next(spookyPic.Count);
                    var photo = spookyPic[index];

                    peep.Photo = $"spooky/{spookyPic[index]}";
                }
            }
        }
    }
}
