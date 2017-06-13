using System.Configuration;
using System.Web;

namespace Rightpoint.Peeps.Api
{
    public static class Constants
    {
        public const string PeepsFile = "peeps.json";
        public const string ImagePath = "/Content/Images/";

        public static class GraphApi
        {
            private static string Endpoint = "https://graph.windows.net";
            private static string Organization = $"{ConfigurationManager.AppSettings["graph:OrganizationName"]}";
            private static string ApiVersion = $"api-version={ConfigurationManager.AppSettings["graph:Version"]}";

            /// <summary>
            /// 
            /// </summary>
            /// <remarks>
            /// https://msdn.microsoft.com/en-us/Library/Azure/Ad/Graph/api/users-operations#GetUsers
            /// </remarks>
            public static string Users = $"{Endpoint}/{Organization}/users?{ApiVersion}";
            public static string UserThumbnail = "/thumbnailPhoto";
        }
    }
}