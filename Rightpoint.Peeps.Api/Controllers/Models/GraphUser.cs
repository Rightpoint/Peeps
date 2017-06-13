using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rightpoint.Peeps.Api.Models
{
    public class GraphUser : IGraphObject
    {
        //public string odata.metadata { get; set; }
        //public string odata.type { get; set; }
        public string objectType { get; set; }
        public string objectId { get; set; }
        public object deletionTimestamp { get; set; }
        public bool accountEnabled { get; set; }
        public IList<object> signInNames { get; set; }
        public IList<object> assignedLicenses { get; set; }
        public IList<object> assignedPlans { get; set; }
        public object city { get; set; }
        public string companyName { get; set; }
        public object country { get; set; }
        public object creationType { get; set; }
        public string department { get; set; }
        public bool dirSyncEnabled { get; set; }
        public string displayName { get; set; }
        public object facsimileTelephoneNumber { get; set; }
        public string givenName { get; set; }
        public string immutableId { get; set; }
        public object isCompromised { get; set; }
        public string jobTitle { get; set; }
        public DateTime lastDirSyncTime { get; set; }
        public string mail { get; set; }
        public string mailNickname { get; set; }
        public object mobile { get; set; }
        public object onPremisesDistinguishedName { get; set; }
        public string onPremisesSecurityIdentifier { get; set; }
        public IList<object> otherMails { get; set; }
        public string passwordPolicies { get; set; }
        public object passwordProfile { get; set; }
        public string physicalDeliveryOfficeName { get; set; }
        public object postalCode { get; set; }
        public object preferredLanguage { get; set; }
        public IList<object> provisionedPlans { get; set; }
        public IList<object> provisioningErrors { get; set; }
        public IList<string> proxyAddresses { get; set; }
        public DateTime refreshTokensValidFromDateTime { get; set; }
        public object showInAddressList { get; set; }
        public string sipProxyAddress { get; set; }
        public object state { get; set; }
        public object streetAddress { get; set; }
        public string surname { get; set; }
        public string telephoneNumber { get; set; }
        //public string thumbnailPhoto@odata.mediaContentType { get; set; }
        public string usageLocation { get; set; }
        public string userPrincipalName { get; set; }
        public string userType { get; set; }
    }
}