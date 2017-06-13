using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Rightpoint.Peeps.Api.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Rightpoint.Peeps.Api.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// https://msdn.microsoft.com/en-us/library/azure/ad/graph/howto/azure-ad-graph-api-supported-queries-filters-and-paging-options
    /// </remarks>
    public class GraphApiService
    {
        /// <summary>
        /// Fetch a list of users from the Graph API
        /// </summary>
        /// <param name="take">Indicates how many users to fetch</param>
        /// <returns></returns>
        public async Task<IEnumerable<Peep>> FetchUsers(AuthenticationResult auth, int take)
        {
            var result = new List<Peep>();
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 2, 0);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(auth.AccessTokenType, auth.AccessToken);

                var queryUrl = Constants.GraphApi.Users;

                queryUrl += $"&$top=999";   //Easier way to randomize than to take "all" and orderby GUID?

                var response = await client.GetAsync(queryUrl);
                Debug.WriteLine($"[GetEmployeeDirectory] Result = {response.ReasonPhrase}");

                if (response.IsSuccessStatusCode && response.Content != null)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var node = JsonConvert.DeserializeObject<GraphNode<GraphUser>>(responseString);

                    foreach(var u in node.value.ToList())
                    {
                        var user = u as GraphUser;

                        result.Add(new Peep()
                        {
                            Name = user.displayName,
                            Office = user.physicalDeliveryOfficeName,
                            ServiceLine = user.department,
                            Photo = "https://graph.windows.net/myorganization/users/{user_id}/thumbnailPhoto?api-version"
                        });
                    }
                }
                else
                    throw new Exception(response.ReasonPhrase);
            }

            return result.OrderBy(_ => Guid.NewGuid()).Take(take);
        }
    }
}