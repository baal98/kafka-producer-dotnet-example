using NUnit.Framework;
using Moq;
using Confluent.Kafka;
using System.Threading;
using KafkaConsumer;

namespace KafkaConsumer.Tests
{
    [TestFixture]
    public class KafkaConsumerServiceTests
    {
        private Mock<IConsumer<Ignore, string>> _mockConsumer;
        private KafkaConsumerService _kafkaConsumerService;

        [SetUp]
        public void SetUp()
        {
            _mockConsumer = new Mock<IConsumer<Ignore, string>>();
            var config = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            // Instead of creating a new ConsumerBuilder here, use the mocked IConsumer
            _kafkaConsumerService = new KafkaConsumerService(_mockConsumer.Object);
        }

        [Test]
        public void Consume_ShouldReturnMessage_WhenCalled()
        {
            // Arrange
            var expectedMessageValue = "Test Message";
            var cts = new CancellationTokenSource(); // Create a CancellationTokenSource
            var expectedConsumeResult = new ConsumeResult<Ignore, string>
            {
                Message = new Message<Ignore, string> { Value = expectedMessageValue },
                TopicPartitionOffset = new TopicPartitionOffset("test_topic", 0, Offset.Unset)
            };

            // Set up the mock to return a successful result when Consume is called
            _mockConsumer.Setup(x => x.Consume(It.IsAny<CancellationToken>()))
                .Returns(expectedConsumeResult);

            // Act
            var result = _kafkaConsumerService.Consume(cts.Token); // Pass the cancellation token here

            // Assert
            Assert.AreEqual(expectedMessageValue, result.Message.Value);

            // Clean up
            cts.Cancel(); // Cancel the token to simulate a cancellation
            cts.Dispose(); // Dispose the CancellationTokenSource when done
        }
    }
}
