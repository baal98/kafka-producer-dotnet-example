using Confluent.Kafka;
using System;
using System.Threading;

namespace KafkaConsumer
{
    public class KafkaConsumerService
    {
        private readonly IConsumer<Ignore, string> _consumer;

        // Constructor for actual usage
        public KafkaConsumerService(ConsumerConfig config)
        {
            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        }

        // Constructor for testing
        public KafkaConsumerService(IConsumer<Ignore, string> consumer)
        {
            _consumer = consumer ?? throw new ArgumentNullException(nameof(consumer));
        }


        public ConsumeResult<Ignore, string> Consume(CancellationToken cancellationToken)
        {
            try
            {
                return _consumer.Consume(cancellationToken);
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