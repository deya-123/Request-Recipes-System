# Recipe Request System

## Introduction

Welcome to the Recipe Request project, developed using ASP.NET Core with Entity Framework Core and Oracle Database. This repository contains the system documentation for a web application designed to simplify recipe searching and management.

### Objectives

The Recipe Blog aims to provide users with a convenient platform to discover and access cooking recipes based on available ingredients, leveraging modern web technologies.

## Table of Contents

1. [User Roles](#user-roles)
2. [Functional Requirements](#functional-requirements)
3. [Workflow Charts](#workflow-charts)
4. [Interface Design](#interface-design)
5. [Project Details](#project-details)
   - [Programming Language and Technologies](#programming-language-and-technologies)
   - [Setup and Installation](#setup-and-installation)
6. [Additional Features](#additional-features)

## User Roles

The system supports the following user roles:

- **Admin**: Manages system operations, user roles, and recipe approvals.
- **User**: Browses recipes, requests recipes from chefs, and manages personal profile.
- **Chef**: Adds new recipes, manages recipe requests, and interacts with users.
- **Guest User**: Visitors who browse recipes without logging in.

## Functional Requirements

### Admin

- Secure authentication and authorization for system access.
- CRUD operations for recipe categories.
- Approval or rejection of recipes submitted by chefs.
- User management including profile updates and access control.
- Generation of reports on recipe requests and sales.
- Management of static content pages (Home, About Us, Contact Us).

### User

- Secure registration and login functionality.
- View and search recipes by category and ingredients.
- Request recipes from specific chefs.
- Receive notifications and email confirmations for approved recipes.
- Payment integration for recipe requests.
- Download recipes as PDF after approval.
- Update personal profile information.

### Chef

- Registration and login capabilities.
- Adding new recipes with detailed ingredients and instructions.
- Viewing other chefs' recipes and managing own recipes.
- Accepting or rejecting recipe requests from users.
- Notifications for new recipe requests.
- Interaction with users through comments and feedback.

## Workflow Charts

Detailed workflow diagrams are available in the project's documentation.
![image](https://github.com/deaaAldeen45112/request-recipes-system/assets/99683685/30ae287c-922a-44f1-9232-723791a179fd)

## Interface Design

The user interface is designed to be intuitive and responsive, ensuring a seamless experience across various devices.

## Project Details

### Programming Language and Technologies

The project is developed using:

- **ASP.NET Core**: Framework for building modern web applications.
- **C#**: Primary programming language for backend logic.
- **Entity Framework Core (EF Core)**: Object-Relational Mapping (ORM) for database interaction.
- **Oracle Database**: Reliable and scalable database solution for data storage.

### Setup and Installation

To set up and run the project locally:

1. Clone the repository from [GitHub Repo URL].
2. Open the project in Visual Studio or your preferred IDE.
3. Restore NuGet packages.
4. Update the database connection string in `appsettings.json` to point to your Oracle Database instance.
5. Ensure Oracle Data Provider for .NET (ODP.NET) is installed on your development environment.
6. Run Entity Framework Core migrations to create or update the database schema.
7. Build and run the project.

## Additional Features

- **PDF Generation**: We use the `wkhtmltopdf` tool to generate beautiful PDFs for recipes and invoices.
- **Security Enhancements**: 
  - Password hashing for secure user authentication.
  - Forget password functionality with email verification.
  - Email verification for new user registrations.
- **Rate Limiter**: Implemented to protect the system from abuse and ensure fair usage.
