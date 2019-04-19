using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

using Amazon;
using Amazon.CostExplorer;
using Amazon.CostExplorer.Model;

namespace AmazonWebServices
{
    public class CostExplorer
    {
        public static AmazonCostExplorerClient client = new AmazonCostExplorerClient(Utils.LoadProfile(), RegionEndpoint.USEast1);

        public static void GetCostsAndUsage()
        {
            Console.WriteLine(Utils.CurrentMethod);
            GetCostAndUsageRequest request = new GetCostAndUsageRequest();
            request.TimePeriod = new DateInterval() { Start = "2019-03-01", End = "2019-04-01" };
            request.Granularity = Granularity.MONTHLY;
            request.Metrics = new List<string>();
            request.Metrics.Add(Metric.AMORTIZED_COST);
            request.Metrics.Add(Metric.BLENDED_COST);

            request.GroupBy = new List<GroupDefinition>();
            request.GroupBy.Add(new GroupDefinition { Type = GroupDefinitionType.TAG, Key = "cloud-environment" });
            request.GroupBy.Add(new GroupDefinition { Type = GroupDefinitionType.DIMENSION, Key = Dimension.SERVICE });

            bool keepLooking = true;
            int callCount = 0;

            while (keepLooking)
            {
                Task<GetCostAndUsageResponse> t = client.GetCostAndUsageAsync(request);
                t.Wait(30000);
                GetCostAndUsageResponse response = t.Result;

                string json = JsonTools.Serialize(response, true);
                Console.WriteLine(json);

                String fileName = $"/Users/guy/temp/cost-and-usage-{callCount.ToString().PadLeft(4, '0')}.json";
                File.WriteAllText(fileName, json);

                string nextPageToken = response.NextPageToken;

                if (String.IsNullOrWhiteSpace(nextPageToken))
                    keepLooking = false;
                else if (callCount > 30)
                    keepLooking = false;
                else
                    request.NextPageToken = nextPageToken;

                callCount++;

            }

        }
    }
}
