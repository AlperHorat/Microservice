# Microservice Architecture Project 🧩

This project is a .NET-based microservices system designed to manage a simple contact directory and generate reports using a queue-based asynchronous architecture.

The system demonstrates distributed system design using message-driven communication and background processing.

---

## 🧠 Project Overview

The system consists of:

- 2 REST API microservices
- 1 console application (background worker)
- 1 unit test project
- RabbitMQ-based message queue system

The main goal is to simulate a scalable architecture where report generation is handled asynchronously without blocking the main services.

---

## 📦 System Components

### 1. Contact Service (ASP.NET Core Web API)
Responsible for managing contact data.

**Features:**
- Create / delete person
- Add contact information (email, phone, location)
- Manage directory data via REST APIs
- Swagger documentation available

---

### 2. Report Service (ASP.NET Core Web API)
Responsible for report management and request handling.

**Features:**
- Create report request
- List reports
- View report details
- Publishes report requests to message queue

---

### 3. Console Application (Background Worker)

Acts as a queue consumer.

**Responsibilities:**
- Continuously listens to report request queue
- Fetches required data from Contact Service
- Generates report file (Excel)
- Updates report status
- Publishes completed report data to result queue

---

### 4. Unit Test Project

- Built with xUnit
- Used for testing core service logic

---

## 🔄 Report Generation Flow

1. User sends a report request via Report Service (Swagger API)
2. Request is published to RabbitMQ queue
3. Console application consumes the request
4. Console app retrieves data from Contact Service
5. Report is generated as an Excel file
6. File path is saved back to the system
7. Report status is updated as completed
8. Final result is published to report queue

---

## ⚙️ Architecture Highlights

- Microservices architecture
- Asynchronous processing using RabbitMQ
- Decoupled system design
- Database per service (PostgreSQL)
- Background worker processing model

---

## 🛠️ Technologies Used

- .NET Core Web API
- .NET Core Console Application
- xUnit Testing Framework
- RabbitMQ (Message Broker)
- PostgreSQL
- Swagger (API documentation)

---

## 🚀 How to Run the Project

> Important: All services must run simultaneously for the system to work correctly.

1. Start Contact Service
2. Start Report Service
3. Run Console Application (Worker)
4. Use Swagger UI on Report Service
5. Create a report request via `AddReportRequest` endpoint

---

## 📚 What This Project Demonstrates

- Microservices architecture design
- Message-driven asynchronous communication
- Background processing with queue consumers
- Separation of concerns between services
- Real-world distributed system workflow

---

## 🔮 Possible Improvements

- Add API Gateway (Ocelot / YARP)
- Add authentication (JWT)
- Dockerize entire system
- Add centralized logging (ELK stack)
- Add retry & dead-letter queue handling
- Improve observability (metrics, tracing)

---

## 📫 Contact

GitHub: https://github.com/AlperHorat
