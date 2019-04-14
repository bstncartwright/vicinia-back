using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json.Linq;
using Microsoft.Azure.Documents;

namespace Vicinia
{
    public static class GetMessage
    {
        private static readonly DocumentClient _client = new DocumentClient(new Uri(Environment.GetEnvironmentVariable("endpoint")), Environment.GetEnvironmentVariable("key"));

        [FunctionName("GetMessage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get message triggered");

            string id = req.Query["id"];
            Document response;
            try
            {
               
                response = await _client.ReadDocumentAsync(UriFactory.CreateDocumentUri("MessageDB", "Messages", id));
            }
            catch (DocumentClientException e)
            {
                log.LogInformation($"{e}");
                throw;
            }

            var responseObject = JObject.Parse(JsonConvert.SerializeObject(response));

            return responseObject != null
                ? (ActionResult)new OkObjectResult($"{responseObject.ToString()}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
