using Core.Azure.Interfaces;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Core.Azure
{
    public class QueueResolver : IQueueResolver
    {
        private readonly CloudQueueClient _queueClient;
        public QueueResolver()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzureConnectionString"));
            _queueClient = storageAccount.CreateCloudQueueClient();
        }

        public CloudQueue GetQueue(string queueName)
        {
            return _queueClient.GetQueueReference(queueName);
        }
    }
}
