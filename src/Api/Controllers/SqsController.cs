using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace Api.Controllers
{
    [RoutePrefix("sqs")]
    public class SqsController : ApiController
    {
        private AmazonSQSClient _sqlClient;
        private readonly string _testSqsQueueUrl;
        public SqsController()
        {
            //Set up the config
            var awsConfig = new AmazonSQSConfig();
            awsConfig.ServiceURL = "http://sqs.us-west-2.amazonaws.com";

            //Create the SQS Client
            _sqlClient = new AmazonSQSClient(awsConfig);

            //Create the Queue, and store the QueueUrl for future use
            _testSqsQueueUrl = _sqlClient.CreateQueue("TestSqsQueue").QueueUrl;
        }

        [Route("{name}")]
        public IHttpActionResult PostToQueue(string name)
        {
            //Create the message to send
            SendMessageRequest message = new SendMessageRequest(_testSqsQueueUrl, $"Hello, {name}");

            //Send the message
            _sqlClient.SendMessage(message);

            return StatusCode(HttpStatusCode.Accepted);
        }
    }
}
