using NUnit.Framework;
using Moq;
using Confluent.Kafka;
using System.Threading;

namespace KafkaConsumer.Tests
{
    [TestFixture]
    public class KafkaConsumerServiceTests
    {
        private Mock<IConsumer<Ignore, string>> mockConsumer;
        private KafkaConsumerService kafkaConsumerService;

        [SetUp]
        public void SetUp()
        {
            mockConsumer = new Mock<IConsumer<Ignore, string>>();
            kafkaConsumerService = new KafkaConsumerService(mockConsumer.Object);
        }

        [Test]
        public void Consume_ShouldReturnMessage_WhenCalled()
        {
            // Arrange
            var expectedMessageValue = "Test Message";
            var expectedConsumeResult = new ConsumeResult<Ignore, string>
            {
                Message = new Message<Ignore, string> { Value = expectedMessageValue },
                TopicPartitionOffset = new TopicPartitionOffset("test_topic", 0, Offset.Unset)
            };
            mockConsumer.Setup(x => x.Consume(It.IsAny<CancellationToken>()))
                .Returns(expectedConsumeResult);

            using var cts = new CancellationTokenSource();

            // Act
            var result = kafkaConsumerService.Consume(cts.Token);

            // Assert
            Assert.That(result.Message.Value, Is.EqualTo(expectedMessageValue));
            mockConsumer.Verify(x => x.Consume(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}