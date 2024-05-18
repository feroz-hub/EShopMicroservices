# EShopMicroservices

## Project Overview

EShopMicroservices is a sample project demonstrating the use of microservices architecture for building a scalable and maintainable e-commerce platform. This project is built using modern technologies such as .NET Core, ASP.NET Core, MassTransit, RabbitMQ, and gRPC.

## Architecture

The project is designed using microservices architecture, where each service is responsible for a specific domain or functionality of the e-commerce platform. The main services include:

- **Basket Service**: Manages the shopping cart functionality.
- **Ordering Service**: Handles order creation, processing, and management.
- **Discount Service**: Provides discount calculation for products.

## Services

### 1. Basket Service

- **Endpoint**: `/api/basket`
- **Description**: Manages shopping cart operations such as adding, updating, and removing items.

### 2. Ordering Service

- **Endpoint**: `/api/orders`
- **Description**: Manages order processing, including creating orders from the basket and handling order statuses.

### 3. Discount Service

- **Endpoint**: `/api/discount`
- **Description**: Provides discount information and applies discounts to products in the basket.

## Technologies Used

- **.NET Core**: For building the microservices.
- **ASP.NET Core**: For creating RESTful APIs.
- **MassTransit**: For messaging and communication between microservices.
- **RabbitMQ**: As the message broker.
- **gRPC**: For inter-service communication.
- **Entity Framework Core**: For data access and database management.
- **Docker**: For containerizing the services.

## Setup Instructions

### Prerequisites

- .NET Core SDK 6.0 or higher
- Docker
- RabbitMQ
- SQL Server

### Clone the Repository

```sh
git clone https://github.com/yourusername/EShopMicroservices.git
cd EShopMicroservices
