# E-Commerce Web Application

## Introduction

This is an e-commerce web application built with ASP.NET Core MVC, Entity Framework Core, and SQL Server. The project follows a "Code First" approach for database modeling and leverages ASP.NET Identity Framework to manage user roles and authentication.

## Features

### User Roles

- **Super Admin**: 
  - Can add or remove admin accounts.
  - Manages customer accounts.
  - Changes order status from pending to shipping.


- **Admin**: 
  - Manages customer accounts.
  - Manages products and categories with CRUD operations.
  - Changes order status from pending to shipping.

- **Customer**:
  - Adds products to the cart.
  - Places orders.

### Authentication

- Cookie-based authentication system.

### Database

- Utilizes Entity Framework Core with SQL Server for data storage.
- Code First approach to define data models.

### Models and Repositories

- Model and repository implementation for:
  - Users
  - Products
  - Categories
  - Cart items
  - Orders
  - Order details

## Getting Started

To run the application locally, follow these steps:

1. Clone the repository to your local machine:

   ```shell
   git clone https://github.com/your-username/your-repo.git
   ```
2. the project in Visual Studio or your preferred IDE.

3. Configure the database connection in appsettings.json.

4. Run the following commands in the package manager console to create and apply migrations:
   ```
   dotnet ef migrations add InitialCreate
   dotnet ef database update
5. Build and run the application.
   
## Usage
- Super Admin:

    - Access the Super Admin dashboard to manage admin and customer accounts.

- Admin:

    - Access the Admin dashboard to manage customer accounts, products, categories, and order statuses.

- Customer:

    - Browse products, add them to the cart, and place orders.


## Default Super Admin Account:
```Username: TemporaryUsername```

```Password: TemporaryPassword```

> [!WARNING]
> You Have To Change Those Before Using The App, Either in the code or in the `Edit Profile` Page.

## Contributing
Contributions are welcome! Feel free to open an issue or submit a pull request.

## Acknowledgments
- ASP.NET Core
- Entity Framework Core
- SQL Server
- ASP.NET Identity Framework

## Contact
For any questions or inquiries, please contact Abdelarhman.Anwar@bk.ru.
