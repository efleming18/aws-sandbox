Azure Queue Storage

It is common to want to be able to use messages, or message queues in your system's architecture. I'm actively using a queueing system in my day-to-day work called NServiceBus (https://particular.net/nservicebus) which uses the local computer for it's transport whether that be MSMQ, SQL Transport, or others that NServiceBus supports. I've recently come across a scenario where a service outside of my control needed access to the queue. This is where Azure Queue Storage comes in to play.

Azure Queue Storage can be accessed anywhere via authenticated http, or https requests. In these queues, you can store millions of messages to be consumed by some process that runs in your code base to read from the queue, an Azure web role, or an Azure worker role.

##Set Up
Before interacting with any of Azure's storage capabilites you'll need to create an Azure Storage Account. This will be place where all of the Azure Storage Data objects are stored. This can be accomplished from the Azure Portal, Azure CLI, or Azure PowerShell.

Portal: https://portal.azure.com/
CLI: https://docs.microsoft.com/en-us/azure/storage/storage-azure-cli
PowerShell: https://docs.microsoft.com/en-us/azure/storage/storage-powershell-guide-full

For this walk-through, we will be using the Azure Storage account in the cloud rather than the Azure storage emulator. I will be writing another article on using the Azure Storage emulator, but for now you can find information on it here, https://docs.microsoft.com/en-us/azure/storage/storage-use-emulator. The existence of the Azure Storage emulator is a big advantage for developers over the AWS SQS where there is no ability to test locally.

##Creating a new Queue Storage
The first step in being able to programatically create a new queue, is to set up the Connection string for being able to communicate with your Azure Storage account. This can be accomplished by adding the following line to the **appSettings** section of your App/Web.config file.

[code language="csharp" escaped="true"]
<add key="AzureConnectionString" value="DefaultEndpointsProtocol=https;AccountName=your-account-name;AccountKey=your-account-key" />
[/code]

You should know your AccountName, but you'll need to get your AccountKey from the Azure Portal. In order to find this, Select your Azure Storage account and then go to Access Keys. Here you will see key1 and key2 which can be used for your connection string.

After the connection string is in place, you can get to work! You'll need to install the following two NuGet packages to your project.
	WindowsAzure.Storage NuGet packages (with Dependencies pic)
	Microsoft.WindowsAzure.ConfigurationManager

Now, the first bit of code, will be to read your ConnectionString in order to connect to your Storage Account.

[code language="csharp" escaped="true"]
CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzureConnectionString"));
[/code]

We can then create the Azure CloudQueueClient from the CloudStorageAccount we just created. This CloudQueueClient is what we will use to interact with our Queue Storage queues.

[code language="csharp" escaped="true"]
CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
[/code]

Now that we have the client, we can create the queue. You'll notice first, we call the **GetQueueReference** method which will get information about a queue from your Azure Storage Account. It will contain information such as whether or not the queue exists which is used in the next line where we call **CreateIfNotExists**. This will do exactly as the method says, if the queue does not exist, it will be created; if it does the command to create the queue will nto be executed.

[code language="csharp" escaped="true"]
//Get back relevant information about the queue
_queue = queueClient.GetQueueReference("first-test-queue");

//Create the queue if it does not exist
_queue.CreateIfNotExists();
[/code]

##Push A Message To The Queue
Now that we have the queue created, we can push messages to it!

[code language="csharp" escaped="true"]
_queue.AddMessage(new CloudQueueMessage("This is a test message."));
[/code]

The code above will push a message to the queue we created with the content of "This is a test message.". There is no way to view the contents of the queue we created through the Azure Portal. For this, we can use a third party tool called **Microsoft Azure Storage Explorer** which can be found here, http://storageexplorer.com/.

[Show picture of message in queue in MASE]

There you have it, you've just pushed a message to your Azure Queue!

For your reference, here is the code I used to quickly test this.
**Note:** This is not typically how you would want to set this up. You would move the queue creation and set up to the Startup of the project and inject some sort of settings and/or queue objects using DI, which I'd recommend Autofac to help you out there.

[code language="csharp" escaped="true"]
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

[/code]
