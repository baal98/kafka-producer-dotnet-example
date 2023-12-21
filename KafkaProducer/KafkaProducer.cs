using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace KafkaProducer
{
    public class KafkaProducer
    {
        private IProducer<Null, string>? _producer;

        public KafkaProducer(IProducer<Null, string> producer)
        {
            _producer = producer ?? throw new ArgumentNullException(nameof(producer));
        }

        public KafkaProducer(ProducerConfig config)
            : this(new ProducerBuilder<Null, string>(config).Build())
        {
        }

        public async Task<DeliveryResult<Null, string>> ProduceAsync(string topic, string value)
        {
            // Ensure the message is not null before sending
            var message = new Message<Null, string> { Value = value ?? throw new ArgumentNullException(nameof(value)) };
            return await _producer.ProduceAsync(topic, message);
        }

        public void FlushAndClose()
        {
            // Use null conditional operator to avoid potential NullReferenceException
            _producer?.Flush(TimeSpan.FromSeconds(10));
            _producer?.Dispose();
            _producer = null;
        }
    }
}