# Financial Planner Microservices

A robust, scalable microservices architecture for managing personal finances, including digital wallets, user accounts, and financial transactions.

## Overview

Financial Planner Microservices is a comprehensive solution for personal finance management. It leverages a microservices architecture to provide a flexible and scalable platform for tracking income, expenses, and financial goals across multiple wallets.

## Architecture Diagram

![Microservices Architecture Diagram](https://github.com/pepega90/financial_planner_microservices/blob/main/finan_diag.png)

## Features

- **User Management**: Create and manage user accounts
- **Multi-Wallet Support**: Users can create and manage multiple wallets
- **Transaction Tracking**: 
  - Record income and expenses
  - Categorize transactions
  - Transfer funds between wallets
- **Financial Analysis**:
  - View records between specific dates
  - Calculate cash flow (income vs. expenses)
  - Get expense recaps by category
  - Retrieve recent transaction history

## Architecture

The application is built using a microservices architecture, consisting of:

- UserService: Handles user account management
- WalletService: Manages wallets and their balances
- TransactionService: Processes and stores financial transactions

Each service follows a clean architecture pattern:
- API: Handles HTTP requests and responses
- Application: Contains business logic and use cases
- Domain: Defines the core domain models and interfaces
- Infrastructure: Implements data access and external service integrations

## Tech Stack

- .NET 8 / C#
- Entity Framework Core
- PostgreSQL
- Docker
- MassTransit RabbitMQ (for inter-service communication)

## Getting Started

### Prerequisites

- .NET 8 SDK
- Docker
- PostgreSQL

### Running the Application

1. Clone the repository:
   ```
   git clone https://github.com/yourusername/financial_planner_microservices.git
   ```

2. Navigate to the project directory:
   ```
   cd financial_planner_microservices
   ```

3. Run the application using Docker Compose:
   ```
   docker-compose up
   ```

4. The services will be available at:
   - UserService: http://localhost:5001
   - WalletService: http://localhost:5002
   - TransactionService: http://localhost:5003

## Testing

To run the tests for each service:

```
dotnet test
```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
