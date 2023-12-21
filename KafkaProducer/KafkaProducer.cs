using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace KafkaProducer
{
    public class KafkaProducer
    {
        private IProducer<Null, string> _producer;

        // Constructor that takes an IProducer<Null, string>
        public KafkaProducer(IProducer<Null, string> producer)
        {
            _producer = producer ?? throw new ArgumentNullException(nameof(producer));
        }

        // Alternative constructor that creates a producer using the provided config
        public KafkaProducer(ProducerConfig config)
            : this(new ProducerBuilder<Null, string>(config).Build())
        {
        }

        public async Task<DeliveryResult<Null, string>> ProduceAsync(string topic, string value)
        {
            var message = new Message<Null, string> { Value = value };
            return await _producer.ProduceAsync(topic, message);
        }

        public void FlushAndClose()
        {
            _producer.Flush(TimeSpan.FromSeconds(10));
            _producer.Dispose();
        }
    }
}