using System.Net;
using System.Web.Http;
using Core.Azure.Queues;
using Microsoft.WindowsAzure.Storage.Queue;

namespace AzureWeb.Controllers
{
    [RoutePrefix("azure")]
    public class QueueStorageController : ApiController
    {
        private readonly CloudQueue _queue;

        public QueueStorageController(AzureQueues queues)
        {
            _queue = queues.FirstTestQueue;
        }

        [Route("push-message")]
        public IHttpActionResult SendMessageToQueue()
        {
            _queue.AddMessage(new CloudQueueMessage("This is a test message 2."));

            return StatusCode(HttpStatusCode.Accepted);
        }
    }
}
