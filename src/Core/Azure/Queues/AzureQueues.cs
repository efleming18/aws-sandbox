using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Core.Azure.Queues
{
    public class AzureQueues
    {
        public CloudQueue FirstTestQueue;
        public AzureQueues()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzureConnectionString"));
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            FirstTestQueue = queueClient.GetQueueReference("first-test-queue");
            FirstTestQueue.CreateIfNotExists();
        }
    }
}
