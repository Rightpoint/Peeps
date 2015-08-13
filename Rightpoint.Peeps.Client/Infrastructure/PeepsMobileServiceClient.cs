using Microsoft.WindowsAzure.MobileServices;

namespace Rightpoint.Peeps.Client.Infrastructure
{
    public class PeepsMobileServiceClient : MobileServiceClient
    {
        public PeepsMobileServiceClient(string applicationUrl)
            : base(applicationUrl)
        {
        }

        public PeepsMobileServiceClient(string applicationUrl, string applicationKey)
            : base(applicationUrl, applicationKey)
        {
        }
    }
}
