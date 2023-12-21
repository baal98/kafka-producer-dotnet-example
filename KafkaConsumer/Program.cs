using System;
using System.Threading;
using Confluent.Kafka;

namespace KafkaConsumer
{
    class Program
    {
        public static void Main(string[] args)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            var kafkaConsumerService = new KafkaConsumerService(conf);
            kafkaConsumerService.Subscribe("test_topic");

            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true; // prevent the process from terminating.
                cts.Cancel();
            };

            try
            {
                while (true)
                {
                    var cr = kafkaConsumerService.Consume(cts.Token);
                    if (cr != null)
                    {
                        Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                kafkaConsumerService.Close();
            }
        }
    }
}


//using System;
//using System.Threading;
//using Confluent.Kafka;

//namespace KafkaConsumer
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var conf = new ConsumerConfig
//            {
//                GroupId = "test-consumer-group",
//                BootstrapServers = "localhost:9092",
//                AutoOffsetReset = AutoOffsetReset.Earliest
//            };

//            using var c = new ConsumerBuilder<Ignore, string>(conf).Build();
//            c.Subscribe("test_topic");

//            var cts = new CancellationTokenSource();
//            Console.CancelKeyPress += (_, e) =>
//            {
//                e.Cancel = true; // prevent the process from terminating.
//                cts.Cancel();
//            };

//            try
//            {
//                while (true)
//                {
//                    try
//                    {
//                        var cr = c.Consume(cts.Token);
//                        Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
//                    }
//                    catch (ConsumeException e)
//                    {
//                        Console.WriteLine($"Error occured: {e.Error.Reason}");
//                    }
//                }
//            }
//            catch (OperationCanceledException)
//            {
//                // Close the consumer.
//                c.Close();
//            }
//        }
//    }
//}