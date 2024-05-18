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
Running the Services
Using Docker Compose
To run all the services using Docker Compose, navigate to the root directory of the project and run:

docker-compose up
This will start all the services along with RabbitMQ and SQL Server containers.

Endpoints
Basket Service
GET /api/basket/{username}
POST /api/basket
DELETE /api/basket/{username}
Ordering Service
POST /api/orders
GET /api/orders/{id}
Discount Service
GET /api/discount/{productName}
POST /api/discount
Sample Data
You can use the following sample data for testing the services:

Contributing
Contributions are welcome! Please open an issue or submit a pull request for any improvements or new features.

License
This project is licensed under the MIT License.


