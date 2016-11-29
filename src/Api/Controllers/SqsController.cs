using System.Net;
using System.Web.Http;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace Api.Controllers
{
    [RoutePrefix("sqs")]
    public class SqsController : ApiController
    {
        private readonly AmazonSQSClient _sqlClient;
        private readonly string _testSqsQueueUrl;
        public SqsController()
        {
            //Set up the config
            var awsConfig = new AmazonSQSConfig();
            awsConfig.ServiceURL = "http://sqs.us-west-2.amazonaws.com";
            awsConfig.RegionEndpoint = RegionEndpoint.USEast1;
            //Create the SQS Client
            _sqlClient = new AmazonSQSClient(awsConfig);

            //Create the Queue, and store the QueueUrl for future use
            _testSqsQueueUrl = _sqlClient.CreateQueue("TestSqsQueue1").QueueUrl;
        }

        [Route("{name}")]
        public IHttpActionResult PostToQueue(string name)
        {
            //Create the message to send
            SendMessageRequest message = new SendMessageRequest(_testSqsQueueUrl, "Hello, reader");

            //Send the message
            _sqlClient.SendMessage(message);

            return StatusCode(HttpStatusCode.Accepted);
        }
    }
}
