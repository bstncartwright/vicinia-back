using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using static Vicinia.sharedObjects;
using System.Collections.Generic;

namespace Vicinia
{
    public static class GetMessages
    {
        private static readonly DocumentClient _client = new DocumentClient(new Uri(Environment.GetEnvironmentVariable("endpoint")), Environment.GetEnvironmentVariable("key"));


        [FunctionName("GetMessages")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string location = req.Query["location"];

            var time = DateTime.Now;

            var locationObject = JObject.Parse(location);

            var latitude = locationObject.GetValue("lat");

            var longitude = locationObject.GetValue("long");

            var query = _client.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri("MessageDB", "Messages"),
                CreateQuery(new Location(Convert.ToDouble(longitude), Convert.ToDouble(latitude)), time)).AsDocumentQuery();

            var results = new List<object>();

            while (query.HasMoreResults)
            {
                results.Add(await query.ExecuteNextAsync());
            }

            return results != null
                ? (ActionResult)new OkObjectResult($"Hello, {results}")
                : new BadRequestObjectResult("Please pass a location on the query string or in the request body");
        }

        public static string CreateQuery(Location location, DateTime time)
        {
            var radius = .0003;
            var query = $"SELECT value c.id FROM c where (c.location.lat between {location.Latitiude - radius} and {location.Latitiude + radius}) " +
            $"and (c.location.long between {location.Longitude - radius} and {location.Longitude + radius}) " + 
            $"and (c.time between {time.ToString("yyyy-MM-dd-HH-mm-ss")} and {time.AddDays(-1).ToString("yyyy-MM-dd-HH-mm-ss")}) " + 
            " order by c.time desc";
            return query;
        }
    }
}
