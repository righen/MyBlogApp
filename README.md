# MyBlogApp

MyBlogApp is a blog application built using ASP.NET Core 8.0. It follows Clean Architecture principles to ensure maintainability, scalability, and testability. This document provides detailed information on the project setup, architecture, design decisions, and additional features implemented.

## Table of Contents

1. [Project Structure](#project-structure)
2. [Setup and Installation](#setup-and-installation)
3. [Architecture](#architecture)
4. [Design Decisions](#design-decisions)
5. [Features](#features)
6. [Usage](#usage)
7. [Testing](#testing)

## Project Structure

The project is divided into several layers to promote separation of concerns:

```
MyBlogApp/
├── MyBlogApp.API/           # Presentation layer (ASP.NET Core API)
├── MyBlogApp.Application/   # Application layer (Use cases and business logic)
├── MyBlogApp.Domain/        # Domain layer (Entities and interfaces)
├── MyBlogApp.Infrastructure/# Infrastructure layer (Data access and external services)
├── MyBlogApp.Tests/         # Testing project (Unit tests)
└── .idea/                   # IDE configuration files
```

## Setup and Installation

### Prerequisites

- .NET 8.0 SDK or later
- Rider or Visual Studio 2022 or later / Visual Studio Code

### Installation

1. Clone the repository:
   ```sh
   git clone https://github.com/yourusername/MyBlogApp.git
   cd MyBlogApp
   ```

2. Restore the dependencies:
   ```sh
   dotnet restore
   ```

### Running the Application

1. Build the solution:
   ```sh
   dotnet build
   ```

2. Run the API:
   ```sh
   dotnet run --project MyBlogApp.API
   ```

### Docker Setup

#### Building the Docker Image

A Dockerfile is included in the project to facilitate containerization. To build the Docker image, follow these steps:

1. Navigate to the root directory of the project where the Dockerfile is located.
2. Build the Docker image using the following command:
   ```sh
   docker build -t myblogapp .
   ```

#### Running the Docker Container

Once the image is built, you can run the Docker container using the following command:
```sh
docker run -d -p 8080:80 --name myblogapp-container myblogapp
```

- `-d` runs the container in detached mode.
- `-p 8080:80` maps port 80 in the container to port 8080 on your host machine.
- `--name myblogapp-container` assigns a name to the running container.
- `myblogapp` is the name of the Docker image you built.

#### Accessing the Application

After running the container, the application will be accessible at `http://localhost:8080`.

#### Stopping and Removing the Docker Container

To stop the running container:
```sh
docker stop myblogapp-container
```

To remove the container:
```sh
docker rm myblogapp-container
```

## Architecture

The application follows Clean Architecture principles, which include:

- **Presentation Layer (MyBlogApp.API)**: Handles HTTP requests and responses. It uses controllers to interact with the application layer.
- **Application Layer (MyBlogApp.Application)**: Contains business logic and use cases. It includes commands, handlers, and validators.
- **Domain Layer (MyBlogApp.Domain)**: Defines the core entities and interfaces. This layer is independent of other layers.
- **Infrastructure Layer (MyBlogApp.Infrastructure)**: Provides implementations for data access and external services. It interacts with the database and other external systems.

### MediatR

MediatR is used to implement the mediator pattern, decoupling the sender and receiver of requests. This promotes a clean separation between the application's business logic and the infrastructure code.

### AutoMapper - (This is not yet done) - Need to create a class library first that contain all the (DTO requests and responses)

AutoMapper is used to map between DTOs and domain models, ensuring a clean separation of concerns.


## Design Decisions

### Clean Architecture

The application is designed following Clean Architecture principles to ensure:

- **Separation of Concerns**: Different layers handle different responsibilities.
- **Testability**: Business logic can be tested independently of the infrastructure and UI.
- **Maintainability**: Code is organized in a way that makes it easy to modify and extend.

### Validation

FluentValidation is used for input validation. Validators are defined in the application layer and automatically registered and used in the API layer.

### Logging

Serilog is used for logging, providing structured and configurable logging throughout the application.

## Features

- **CRUD Operations**: Create, read, update, and delete posts.
- **Validation**: Input validation using FluentValidation.
- **Logging**: Structured logging with Serilog.
- **Health Checks**: Health checks to monitor the application's status.
- **Swagger**: API documentation using Swagger/OpenAPI.

## Usage

### API Endpoints

- **GET /api/posts**: Retrieve all posts.
- **GET /api/posts/{id}**: Retrieve a specific post by ID.
- **POST /api/posts**: Create a new post.
- **PUT /api/posts/{id}**: Update an existing post.
- **DELETE /api/posts/{id}**: Delete a post.

### Example Request

**Creating a Post**
```sh
POST /api/posts
Content-Type: application/json

{
  "title": "New Post Title",
  "content": "This is the content of the new post."
}
```

## Testing

To run the unit tests:
```sh
dotnet test
```




