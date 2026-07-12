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
