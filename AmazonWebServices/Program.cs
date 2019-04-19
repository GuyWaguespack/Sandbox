using System;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon;

namespace AmazonWebServices
{
    class Program
    {
        public static void Main(string[] args)
        {
            // S3 Methods
            //S3.ListBuckets();

            // DynamoDb Methods
            //DynamoDB.ListTables();
            //DynamoDB.GetItem("syntinel-signals", "000000000");

            // Lambda Methods
            //Lambda.CallMethod("Sandbox-ToUpper", "\"Hello World\"");

            // Cost Explorer Methods
            CostExplorer.GetCostsAndUsage();
        }
    }
}
