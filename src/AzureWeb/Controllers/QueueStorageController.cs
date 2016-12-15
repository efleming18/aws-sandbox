using System.Net;
using System.Web.Http;
using Core.Azure;
using Core.Azure.Interfaces;
using Microsoft.WindowsAzure.Storage.Queue;

namespace AzureWeb.Controllers
{
    [RoutePrefix("azure")]
    public class QueueStorageController : ApiController
    {
        private readonly IQueueResolver _queueResolver;

        public QueueStorageController(IQueueResolver queueResolver)
        {
            _queueResolver = queueResolver;
        }

        [Route("push-message")]
        public IHttpActionResult SendMessageToQueue()
        {
            var firstTestQueue = _queueResolver.GetQueue(AzureQueues.FirstTestQueueName);
            firstTestQueue.AddMessage(new CloudQueueMessage("This is a test message 2."));

            return StatusCode(HttpStatusCode.Accepted);
        }
    }
}
