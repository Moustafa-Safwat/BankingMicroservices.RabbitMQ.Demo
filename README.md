# BankingMicroservices.RabbitMQ.Demo 🏦🐇

This repository demonstrates the implementation of a Banking Microservices architecture using RabbitMQ as the message broker and following Clean Architecture principles. The project aims to showcase how to design and build scalable, loosely coupled microservices that communicate asynchronously via RabbitMQ.

## ✨ Features
* 🏗️ Microservices Architecture: Demonstrates how to separate concerns into individual services (e.g., Account, Transaction, Notification).
* 📨 RabbitMQ Integration: Implements RabbitMQ as a message broker for inter-service communication.
* 🧼 Clean Architecture: Ensures that each service follows Clean Architecture principles, providing clear separation of concerns between application layers.
* ⚙️ Asynchronous Messaging: Microservices communicate asynchronously using message queues, improving scalability and reliability.
## 🛠️ Technologies Used
* .NET Core: Backend framework for building the microservices.
* RabbitMQ: Message broker for handling asynchronous messaging between microservices.
* Entity Framework Core: ORM for database interactions.
* SQL Server: Relational database for persisting service data.
## 🧩 Services
* Account Service: Manages user accounts and balances.
* Transaction Service: Handles financial transactions, like deposits and withdrawals.
* Notification Service: Sends notifications upon successful transactions.
