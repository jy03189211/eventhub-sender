using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.Extensions.Azure;
using Newtonsoft.Json;

namespace EventHub_sender
{

    class Program
    {
        private const string connectionString = "Endpoint=sb://jimmylab-eventhub.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=FCU1pR9ZiBDhT8Gj/b81RZFlVcNwotFVIXr9v4gYUYs=";
        private const string eventHubName = "jimmylab-eventhub1";

        static async Task Main()
        {
            // Create a producer client that you can use to send events to an event hub
            await using (var producerClient = new EventHubProducerClient(connectionString, eventHubName))
            {
                // Create a batch of events 
                
                    
                for (int i = 0; i < 100; i++)
                {
                    using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();
                    string messageBody = $"Message {i}";
                    string jsonlist = $"[{{ \"number\":\"{i}\", \"name\":\"Manas\",\"gender\":\"Male\",\"birthday\":\"1987-8-8\"}},{{ \"number\":\"{i}\", \"name\":\"Manas\",\"gender\":\"Male\",\"birthday\":\"1987-8-8\"}}]";
                    // Add events to the batch. An event is a represented by a collection of bytes and metadata. 
                    //EventData evdata= new EventData(Encoding.UTF8.GetBytes(jsonlist));
                    //evdata.AddEventHubConsumerClient
                    eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(jsonlist)));
                    //eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("Second event")));
                    //eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("Third event")));
                    await producerClient.SendAsync(eventBatch);
                    Console.WriteLine(jsonlist);
                }

                Console.WriteLine("A batch of events has been published.");
            }
            
        }


    }


}
