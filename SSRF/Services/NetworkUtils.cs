using System.Net;

namespace SSRF.Services
{
    public class NetworkUtils
    {
        public bool isInternalIp(string ip)
        {
            Uri uri;
            if(!Uri.TryCreate(ip, UriKind.Absolute, out uri))
            {
                return false;
            }
            var host= uri.Host;
            IPAddress ipAddress;
            if (IPAddress.TryParse(host, out ipAddress))
            {
                
                if ((ipAddress.ToString() == "127.0.0.1" && uri.Port == 8080) ||
                    (ipAddress.ToString() == "192.168.1.100" && uri.Port == 27017))
                {
                    return false; // Allow access to these internal services
                }

                // Block other internal IP ranges (127.x.x.x, 192.168.x.x, etc.)
                byte[] bytes = ipAddress.GetAddressBytes();
                if (bytes[0] == 127 || (bytes[0] == 192 && bytes[1] == 168))
                {
                    return true;
                }
            }
            return false;
        }
        public string SimulateInternalService(string url)
        {
            if (url.Contains("localhost:8080"))
            {
                return "Welcome to the internal web service (localhost:8080). This is a private admin panel.";
            }else if (url.Contains("192.168.1.100:27017")){
                return "MongoDB 4.2. Internal database access granted.";
            }
            return null;
        }
    }
}
