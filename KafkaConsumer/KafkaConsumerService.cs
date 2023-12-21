using Confluent.Kafka;
using System;
using System.Threading;

namespace KafkaConsumer
{
    public class KafkaConsumerService
    {
        private readonly IConsumer<Ignore, string> _consumer;

        public KafkaConsumerService(ConsumerConfig config)
        {
            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        }

        public KafkaConsumerService(IConsumer<Ignore, string> consumer)
        {
            _consumer = consumer ?? throw new ArgumentNullException(nameof(consumer));
        }

        public ConsumeResult<Ignore, string>? Consume(CancellationToken cancellationToken)
        {
            try
            {
                var result = _consumer.Consume(cancellationToken);
                // Make sure to access the message value through .Message.Value
                Console.WriteLine($"Consumed message '{result.Message.Value}' at: '{result.TopicPartitionOffset}'.");
                return result;
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"Error occurred: {e.Error.Reason}");
                return null;
            }
        }

        public void Subscribe(string topic)
        {
            _consumer.Subscribe(topic);
        }

        public void Close()
        {
            _consumer.Close();
        }
    }
}