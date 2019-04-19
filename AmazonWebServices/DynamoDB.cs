using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;

namespace AmazonWebServices
{
    public class DynamoDB
    {
        public static AmazonDynamoDBClient client = new AmazonDynamoDBClient(Utils.LoadProfile(), RegionEndpoint.USEast1);

        public static void ListTables()
        {
            Console.WriteLine(Utils.CurrentMethod);
            Task<ListTablesResponse> t = client.ListTablesAsync();
            t.Wait(10000);
            ListTablesResponse response = t.Result;

            foreach (string tableName in response.TableNames)
                Console.WriteLine(">> " + tableName);
        }

        //public static void GetItemRaw(string table, string keyName, string keyValue)
        //{
        //    Dictionary<string, AttributeValue> key = new Dictionary<string, AttributeValue>
        //    {
        //        { keyName, new AttributeValue{ S = keyValue} }
        //    };

        //    GetItemRequest request = new GetItemRequest
        //    {
        //        TableName = table,
        //        Key = key
        //    };

        //    Task<GetItemResponse> t = client.GetItemAsync(request);
        //    t.Wait(10000);
        //    GetItemResponse response = t.Result;

        //}

        public static void GetItem(string tableName, string value)
        {
            Table table = Table.LoadTable(client, tableName);
            Task<Document> t = table.GetItemAsync(value);
            t.Wait(10000);
            Document doc = t.Result;

            Console.WriteLine(doc?.ToJsonPretty());
        }
    }
}
