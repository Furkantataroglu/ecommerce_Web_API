# E-Commerce Web API

## About the Project
This project is a dynamic e-commerce Web API built on .NET 8, employing modern web application architectures and clean code principles. The API provides secure user registration, login, role management, and shopping cart functionalities. It leverages JWT for authentication and integrates Microsoft Identity for comprehensive user management.

## Technologies Used
- **.NET 8**  
  Core development platform for the application.

- **Microsoft Identity**  
  Integrated for secure user management, authentication, and role-based authorization.

- **JWT (JSON Web Token)**  
  Provides a secure token-based authentication mechanism.

- **SQL Server**  
  Local SQL Server instance for data management.

- **Entity Framework Core**  
  ORM used to interact with the SQL Server database.

- **AutoMapper**  
  Facilitates automatic mapping between DTOs and entity objects.

- **UnitOfWork Pattern**  
  Ensures holistic and consistent database operations through atomic transactions.

- **DTO (Data Transfer Object)**  
  Structured models used for data exchange between client and server.

## Project Structure
The project is organized using a multi-layer architecture:
- **API**  
  Handles HTTP requests and acts as the external interface.

- **DAL (Data Access Layer)**  
  Manages all database interactions with the SQL Server.

- **ENTITIES**  
  Contains the application's core data models.

- **MVC**  
  Manages business logic and presentation using the Model-View-Controller pattern.

- **SERVICES**  
  Hosts business logic for user, product, and shopping cart management.

- **SHARED**  
  Contains common, dynamically built base classes (e.g., `EntityRepositoryBase`, `Entity`, and `EntityBase`) used across the project.

## Features
- **User Registration and Login**  
  - Secure user management via Microsoft Identity  
  - JWT-based authentication for login and session management

- **Role Management**  
  - Role-based access control implemented with Microsoft Identity

- **Shopping Cart Management**  
  - Dynamic functionalities for adding, updating, and removing products from the cart

- **DTO & AutoMapper Integration**  
  - Automatic mapping of data models between the client and server

- **UnitOfWork Pattern**  
  - Ensures consistent and atomic database transactions, preserving data integrity
