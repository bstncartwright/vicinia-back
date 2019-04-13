using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Vicinia.sharedObjects;

namespace Vicinia
{
    public static class CreateMessage
    {
        private static readonly DocumentClient _client= new DocumentClient(new Uri(Environment.GetEnvironmentVariable("endpoint")), Environment.GetEnvironmentVariable("key"));

        [FunctionName("CreateMessage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var name = data?.name;
            var text = data?.text;
            var time = data?.time;
            var location = data?.location;
            var longitude = location.GetValue("long");
            var latitude = location.GetValue("lat");
            var newLocation = new Location(Convert.ToDouble(longitude), Convert.ToDouble(latitude));
            
            var message = new Message($"{name}", $"{text}", Convert.ToDateTime(time), newLocation);

            await _client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri("MessageDB", "Messages"), message);

            return name != null
                ? (ActionResult)new OkObjectResult(JsonConvert.SerializeObject(message))
                : new BadRequestObjectResult("Please pass the name, text, time, and location in the request body");
        }
    }
}
