//Don't mind this, yet
Before all of this, I will assume you've created an AWS account.
I will also assume you've created an AWS Profile and set it as your default Profile to use.
If not, this is how...
First you'll need to add the AWSSDK nuget package to your project.
///

Creating an AWS SQS Queue
Before I talk about creating an SQS queue programmatically I should say this is possible through the AWS Console UI. It is pretty straight forward, so for the scope of this article I will leave those details, but I encourage you to check it out. 
The first step in creating an SQS queue programmatically is creating an **AmazonSQSConfig**. For this config, you'll need to set **ServiceURL** and a **Region**, which specifies the region in which your AWS account resides.

```csharp
    //Set up the config
    var awsConfig = new AmazonSQSConfig();
    awsConfig.ServiceURL = "http://sqs.us-west-2.amazonaws.com";
    awsConfig.RegionEndpoint = RegionEndpoint.USWest2;
```
**Note:** When creating an SQS queue from the SDK, the default region will be set to US East, but the AWS Console will default to US West. Be sure to specify which Region you want to use when creating an SQS queue whether it is through code, or through the AWS Console.

Once we've created the config, we can now use it to create the **AmazonSQSClient**. This client will be used for all of our commands going forward such as creating the queue, pushing items to the queue, reading from the queue, etc.

```csharp
    //Create the SQS Client
    var _sqlClient = new AmazonSQSClient(awsConfig);
```

Creating the Queue
Now that we have our SQS Client set up, we can create our SQS queue. The **CreateQueue** method accepts a queue name as a parameter.

```csharp
    _sqlClient.CreateQueue("TestSqsQueue")
```

Creating the queue is great, but you'll need to have the **QueueUrl** handy for subsequent requests that interact with that specific queue. The **CreateQueue** call can return this Url for you, so what I would recommend doing is the following.

```csharp
    var _testSqsQueueUrl = _sqlClient.CreateQueue("TestSqsQueue").QueueUrl;
```

Creating a queue this way, will create the queue but with all the defaults of an SQS queue. If you want to set specific attributes on an SQS queue you'll need to either modify the existing queue through the AWS Management Console or use the **SetQueueAttributes** method, which I can further explain in another article.

//NOt done yet... just where I stopped yesterday.