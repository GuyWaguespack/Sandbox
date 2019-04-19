using System;
using System.Threading.Tasks;
using System.IO;

using Amazon;
using Amazon.Lambda;
using Amazon.Lambda.Model;

namespace AmazonWebServices
{
    public class Lambda
    {
        public static AmazonLambdaClient client = new AmazonLambdaClient(Utils.LoadProfile(), RegionEndpoint.USEast1);

        public static string CallMethod(string functionName, string json)
        {
            Console.WriteLine(Utils.CurrentMethod);

            InvokeRequest request = new InvokeRequest{
                FunctionName = functionName,
                InvocationType = "RequestResponse",
                LogType = "Tail",
                Payload = json
            };

            Task<InvokeResponse> t = client.InvokeAsync(request);
            t.Wait(30000);
            InvokeResponse response = t.Result;

            MemoryStream ps = response.Payload;
            StreamReader reader = new StreamReader(ps);
            string payload = reader.ReadToEnd();

            Console.WriteLine(">>> Calling Function : " + functionName);
            Console.WriteLine(payload);
            Console.WriteLine();
            Console.WriteLine(">>> Status Code : " + response.StatusCode);

            string logs = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(response.LogResult));

            Console.WriteLine(logs);
            Console.WriteLine(response.FunctionError);

            return payload;
        }
    }
}
