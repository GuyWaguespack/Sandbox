using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.IO;
using System.Threading;

using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;


namespace Sandbox.OAuth
{
    public static class Program
    {
        static void Main(string[] args)
        {
            // V2 Token (Scopes)
            OAuthToken oauth = new OAuthToken()
            {
                TenantId = "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
                ClientId = "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
                ClientSecret = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                Scope = "MyScope"
            };

            string token = oauth.GetToken();
            Console.WriteLine(token);
            Console.WriteLine($"Expires : {oauth.ExpireDate}");
            Console.WriteLine("==========================");

            // V1 Token (Resource)
            oauth = new OAuthToken()
            {
                TenantId = "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
                ClientId = "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
                ClientSecret = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                Resource = "https://management.azure.com/"
            };

            token = oauth.GetToken();
            Console.WriteLine(token);
            Console.WriteLine($"Expires : {oauth.ExpireDate}");


        }
    }
}
