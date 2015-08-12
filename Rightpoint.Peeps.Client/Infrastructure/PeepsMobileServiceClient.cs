using Microsoft.WindowsAzure.MobileServices;

namespace Rightpoint.Peeps.Client.Infrastructure
{
    public class PeepsMobileServiceClient : MobileServiceClient
    {
        public PeepsMobileServiceClient() : base("https://rp-peeps-prod.azure-mobile.net/", "hJnyCcCBUXkBahMjbgcmmBrQddcPOV97")
        {
        }
    }
}
