using System.Net;
using System.Web.Http;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace AzureWeb.Controllers
{
    [RoutePrefix("azure")]
    public class QueueStorageController : ApiController
    {
        private readonly CloudQueue _queue;
        public QueueStorageController()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzureConnectionString"));
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            _queue = queueClient.GetQueueReference("first-test-queue");

            _queue.CreateIfNotExists();
        }

        [Route("push-message")]
        public IHttpActionResult SendMessageToQueue()
        {
            _queue.AddMessage(new CloudQueueMessage("This is a test message."));

            return StatusCode(HttpStatusCode.Accepted);
        }
    }
}
