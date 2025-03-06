# Permission Management Service
## Overview
The **Permission Management Service** is a .NET Core application that provides functionality for managing permissions within an organization. It allows users to create, modify, and retrieve permissions for employees, while also integrating with Elasticsearch for indexing and Kafka for messaging.
## Features
- Create permission requests for employees.
- Modify existing permission requests.
- Retrieve permissions from Elasticsearch.
- Integration with Kafka for messaging.
- Logging using a custom logging service.
## Technologies Used
- .NET 6/7
- Entity Framework Core
- Elasticsearch
- Apache Kafka
- xUnit for unit testing
- Moq for mocking dependencies
## Installation
### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (6.0 or later)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (for database)
- [Elasticsearch](https://www.elastic.co/downloads/elasticsearch) (for indexing)
- [Apache Kafka](https://kafka.apache.org/downloads) (for messaging)

