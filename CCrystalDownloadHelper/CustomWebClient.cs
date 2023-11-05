using System;
using System.Net;

namespace CDownloadHelper {
    public class CustomWebClient : WebClient {
        [System.Security.SecuritySafeCritical]
        public CustomWebClient() : base() {
        }
        public CookieContainer cookieContainer = new CookieContainer();


        protected override WebRequest GetWebRequest(Uri myAddress) {
            WebRequest request = base.GetWebRequest(myAddress);
            if (request is HttpWebRequest) {
                (request as HttpWebRequest).CookieContainer = cookieContainer;
                (request as HttpWebRequest).AllowAutoRedirect = true;
            }
            return request;
        }
    }
}
