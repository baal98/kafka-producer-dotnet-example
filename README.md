# Kafka Producer .NET Example

This repository hosts a .NET Core application which serves as an example of producing messages to an Apache Kafka topic using the Confluent Kafka library.

## Introduction

Apache Kafka is an open-source distributed event streaming platform used by thousands of companies for high-performance data pipelines, streaming analytics, data integration, and mission-critical applications.

This project demonstrates a simple Kafka producer that allows the user to send messages to a Kafka topic via the command line.

## Prerequisites

- .NET Core SDK (version 3.1 or later recommended)
- An accessible Apache Kafka cluster
- A Kafka topic created to which messages will be produced

## Setup

1. Clone this repository:
    ```bash
    git clone https://github.com/baal98/kafka-producer-dotnet-example.git
    cd kafka-producer-dotnet-example
    ```

2. Ensure you have a Kafka broker running and note down its address.

3. Update the Kafka broker details in `Program.cs` file:

    ```csharp
    var config = new ProducerConfig { BootstrapServers = "localhost:9092" }; // Replace with your broker's address
    ```

## Building the Application

To build the application, run the following command in the terminal:

```bash
dotnet build
```

## Running the Application

To run the application, execute:

```bash
dotnet run
```

After starting, the application will wait for input. Type your message and press enter to send it to the configured Kafka topic.

## How to Use

1. Start the application with `dotnet run`.
2. When prompted, type your message and hit Enter to produce it to the Kafka topic.
3. To stop sending messages and exit the application, type `exit` or `quit`.

## Features

- Interactive command-line interface to produce messages to Kafka.
- Configurable Kafka producer settings.
- Graceful error handling and message delivery confirmation.

## Contributing

We encourage public contributions! Please review our `CONTRIBUTING.md` file if you are interested in helping.

## License

This project is licensed under the MIT License - see the `LICENSE.md` file for details.