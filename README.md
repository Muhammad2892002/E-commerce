E-Commerce Management System
Overview

This project is an E-Commerce Management System built with ASP.NET Core MVC following the N-Tier Architecture. The application focuses on clean architecture, maintainability, and scalability by separating responsibilities into different layers.

The project includes product and category management, user authentication, authorization, and administrative user management.

Features
Authentication & Authorization
User Registration
User Login & Logout
ASP.NET Core Identity Authentication
Role-Based Authorization
Input Validation for Authentication Forms
Product Management
Create Product
View Products
Update Product
Delete Product
Server-side Validation
Category Management
Create Category
View Categories
Update Category
Delete Category
Server-side Validation
User Administration
View Registered Users
Promote Users to Admin
Demote Admins to Users
Lock User Accounts
Unlock User Accounts

Architecture

This project follows the N-Tier Architecture, separating the application into distinct layers to improve maintainability, scalability, and separation of concerns.

The project consists of the following layers:

Presentation Layer – Handles the user interface, controllers, views, and user interactions.
Business Logic Layer (BLL) – Contains the business rules, services, validation logic, DTOs, and application workflows.
Data Access Layer (DAL) – Manages data access through repositories, the Unit of Work pattern, and Entity Framework Core.
Domain Layer – Contains the core entities, interfaces, and domain models shared across the application.
Database – SQL Server database used to persist application data.

Each layer has a specific responsibility, making the application easier to maintain, test, and extend while keeping business logic independent from data access and presentation.

Validation

The application includes validation for user input to help maintain data integrity.

Examples include:

Required fields
Email validation
Password validation
Product validation
Category validation

Note

The current implementation does not include validation to check if the category exists when adding a new product.

Database

The project uses:

SQL Server
Entity Framework Core
Code First Migrations
Admin Features

Administrators can:

Manage Products
Manage Categories
View Users
Promote Users to Admin
Demote Admins
Lock User Accounts
Unlock User Accounts

#####images
Login  Page

<img width="497" height="429" alt="image" src="https://github.com/user-attachments/assets/f650677d-b9cd-4a60-820c-f08c3bc3db47" />

Registration Page
<img width="256" height="394" alt="image" src="https://github.com/user-attachments/assets/fa9b1145-f213-4987-93ff-7bc249ccec16" />


Product Index Page 
<img width="758" height="376" alt="image" src="https://github.com/user-attachments/assets/ff2e8c62-8b9a-4e3c-a9c5-58789bb6f546" />
Category Index Page

<img width="902" height="461" alt="image" src="https://github.com/user-attachments/assets/90d4d714-8cdd-48cc-815f-286b7ff8d09f" />

User Index Page
<img width="914" height="451" alt="image" src="https://github.com/user-attachments/assets/95e7bc1f-f13e-4348-926f-c852f03e5170" />



