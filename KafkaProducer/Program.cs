using Confluent.Kafka;

namespace KafkaProducer
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            var kafkaProducer = new KafkaProducer(config);

            Console.WriteLine("Enter your messages (type 'exit' or 'quit' to stop):");

            while (true)
            {
                string message = Console.ReadLine();

                if (message.Equals("exit", StringComparison.OrdinalIgnoreCase) ||
                    message.Equals("quit", StringComparison.OrdinalIgnoreCase))
                {
                    break; // Exit the loop to end the program
                }

                try
                {
                    var deliveryResult = await kafkaProducer.ProduceAsync("test_topic", message);
                    Console.WriteLine($"Delivered '{deliveryResult.Value}' to '{deliveryResult.TopicPartitionOffset}'");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }

            kafkaProducer.FlushAndClose();
            Console.WriteLine("Producer closed");
        }
    }
}



//using Confluent.Kafka;

//namespace KafkaProducer
//{
//    class Program
//    {
//        public static async Task Main(string[] args)
//        {
//            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

//            using var producer = new ProducerBuilder<Null, string>(config).Build();

//            Console.WriteLine("Enter your messages (type 'exit' or 'quit' to stop):");

//            while (true)
//            {
//                string message = Console.ReadLine();

//                if (message.Equals("exit", StringComparison.OrdinalIgnoreCase) ||
//                    message.Equals("quit", StringComparison.OrdinalIgnoreCase))
//                {
//                    break; // Exit the loop to end the program
//                }

//                try
//                {
//                    var deliveryResult = await producer.ProduceAsync(
//                        "test_topic",
//                        new Message<Null, string> { Value = message });

//                    Console.WriteLine($"Delivered '{deliveryResult.Value}' to '{deliveryResult.TopicPartitionOffset}'");
//                }
//                catch (ProduceException<Null, string> e)
//                {
//                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
//                }
//            }

//            producer.Flush(TimeSpan.FromSeconds(10));
//            Console.WriteLine("Producer closed");
//        }
//    }
//}


//using Confluent.Kafka;
//using System;
//using System.Threading.Tasks;

//class Program
//{
//    public static async Task Main(string[] args)
//    {
//        var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

//        using var producer = new ProducerBuilder<Null, string>(config).Build();
//        try
//        {
//            var deliveryResult = await producer.ProduceAsync(
//                "test_topic",
//                new Message<Null, string> { Value = "Hello Kafka" });

//            Console.WriteLine($"Delivered '{deliveryResult.Value}' to '{deliveryResult.TopicPartitionOffset}'");
//        }
//        catch (ProduceException<Null, string> e)
//        {
//            Console.WriteLine($"Delivery failed: {e.Error.Reason}");
//        }
//    }
//}


//using Confluent.Kafka;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//class Program
//{
//    public static async Task Main(string[] args)
//    {
//        var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

//        using var producer = new ProducerBuilder<Null, string>(config).Build();
//        var cancellationTokenSource = new CancellationTokenSource();
//        Console.CancelKeyPress += (_, e) =>
//        {
//            e.Cancel = true; // prevent the process from terminating.
//            cancellationTokenSource.Cancel();
//        };

//        try
//        {
//            while (!cancellationTokenSource.IsCancellationRequested)
//            {
//                var deliveryResult = await producer.ProduceAsync(
//                    "test_topic",
//                    new Message<Null, string> { Value = "Hello Kafka" },
//                    cancellationTokenSource.Token);

//                Console.WriteLine($"Delivered '{deliveryResult.Value}' to '{deliveryResult.TopicPartitionOffset}'");

//                // Check for stop command after sending each message
//                if (Console.KeyAvailable)
//                {
//                    var key = Console.ReadKey(intercept: true).Key; // intercept: true prevents the key from being displayed
//                    if (key == ConsoleKey.X) // let's say 'X' is the stop command
//                    {
//                        break;
//                    }
//                }

//                // Wait for a bit before sending the next message
//                await Task.Delay(1000, cancellationTokenSource.Token); // waits for 1 second
//            }
//        }
//        catch (ProduceException<Null, string> e)
//        {
//            Console.WriteLine($"Delivery failed: {e.Error.Reason}");
//        }
//        catch (OperationCanceledException)
//        {
//            Console.WriteLine("Operation was canceled");
//        }
//        finally
//        {
//            producer.Flush(TimeSpan.FromSeconds(10));
//            Console.WriteLine("Producer closed");
//        }
//    }
//}
