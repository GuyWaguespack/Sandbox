using System;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon;

namespace AmazonWebServices
{
    public class S3
    {
        public static AmazonS3Client client = new AmazonS3Client(RegionEndpoint.USEast1);

        public static void ListBuckets()
        {
            Console.WriteLine(Utils.CurrentMethod);
            Task<ListBucketsResponse> t = client.ListBucketsAsync();
            t.Wait(10000);
            ListBucketsResponse response = t.Result;

            foreach (S3Bucket bucket in response.Buckets)
            {
                Console.WriteLine(">>> " + bucket.BucketName);
            }
        }
    }
}
