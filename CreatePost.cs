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

namespace Vicinia
{
    public static class CreatePost
    {
        private static readonly DocumentClient _client= new DocumentClient(new Uri(Environment.GetEnvironmentVariable("endpoint")), Environment.GetEnvironmentVariable("key"));

        [FunctionName("CreatePost")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var name = data?.name;
            var text = data?.text;
            var time = DateTime.Now;//data?.time;
            var location = data?.location;
            var locationJson = JsonConvert.DeserializeObject(location);
            
            
            

            //var message = Message(name, text, time, )

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
