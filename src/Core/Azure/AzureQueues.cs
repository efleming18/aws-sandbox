using System.Collections.Generic;

namespace Core.Azure
{
    public static class AzureQueues
    {
        public static string FirstTestQueueName = "first-test-queue";
        public static List<string> KnownQueues = new List<string> { FirstTestQueueName };
    }
}
