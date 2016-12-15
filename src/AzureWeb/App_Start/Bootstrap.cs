using Core.Azure;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace AzureWeb
{
    public class Bootstrap
    {
        public static void CreateKnownAzureQueues()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzureConnectionString"));
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            foreach (var queueName in AzureQueues.KnownQueues)
            {
                queueClient.GetQueueReference(queueName).CreateIfNotExists();
            }
        }
    }
}