using Confluent.Kafka;
using Moq;

namespace KafkaProducer.Tests
{
    [TestFixture]
    public class KafkaProducerTests
    {
        private Mock<IProducer<Null, string>> mockProducer;
        private KafkaProducer kafkaProducer;
        private const string TestTopic = "test_topic";
        private const string TestValue = "Hello Kafka";

        [SetUp]
        public void SetUp()
        {
            mockProducer = new Mock<IProducer<Null, string>>();
            kafkaProducer = new KafkaProducer(mockProducer.Object);
        }

        [Test]
        public async Task ProduceAsync_SendsMessageToCorrectTopic()
        {
            // Arrange
            var expectedDeliveryResult = new DeliveryResult<Null, string>
            {
                TopicPartitionOffset = new TopicPartitionOffset(TestTopic, new Partition(0), new Offset(0)),
                Message = new Message<Null, string> { Value = TestValue }
            };

            mockProducer.Setup(x => x.ProduceAsync(TestTopic, It.IsAny<Message<Null, string>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedDeliveryResult);

            // Act
            var result = await kafkaProducer.ProduceAsync(TestTopic, TestValue);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(TestTopic, result.TopicPartitionOffset.Topic);
            Assert.AreEqual(TestValue, result.Value);
            mockProducer.Verify(x => x.ProduceAsync(TestTopic, It.Is<Message<Null, string>>(m => m.Value == TestValue), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
