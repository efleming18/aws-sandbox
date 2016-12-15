using Microsoft.WindowsAzure.Storage.Queue;

namespace Core.Azure.Interfaces
{
    public interface IQueueResolver
    {
        CloudQueue GetQueue(string queueName);
    }
}
