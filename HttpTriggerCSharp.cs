using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using Company.Function.DigestFetcher;



namespace Company.Function
{
    public static class HttpTriggerCSharp
    {
        [FunctionName("HttpTriggerCSharp")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.{}", req.HttpContext);

            string digestId = req.Query["digestId"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            digestId = digestId ?? data?.digestId;
            DigestFetcher.DigestFetcher digestFetcher = new DigestFetcher.DigestFetcher();
            return digestId != null
                ? (ActionResult)new OkObjectResult(digestFetcher.GetDigestLinks(digestId))
                : new BadRequestObjectResult("Please pass a digest id on the query string or in the request body");
        }     
    }
}
