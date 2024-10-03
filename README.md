
# Login API - .NET 8.0 Web API

This is a simple .NET 8.0 Web API that implements basic CRUD (Create, Read, Update, Delete) operations for a login system using MySQL. The API handles user details such as `Id`, `FullName`, and `Email`. It also includes Swagger (via **Swashbuckle**) for easy API documentation.

## Technologies Used
- **ASP.NET Core Web API (.NET 8.0)**
- **Entity Framework Core**
- **MySQL** (as the database)
- **Swashbuckle** (for API documentation)

## Features
- **Create a new user** (`POST`)
- **Get all users** (`GET`)
- **Get a specific user by ID** (`GET`)
- **Update an existing user** (`PUT`)
- **Delete a user** (`DELETE`)
- **Swagger API documentation**

## Requirements

- [.NET SDK 8.0](https://dotnet.microsoft.com/download)
- [MySQL](https://dev.mysql.com/downloads/)
- [MySQL Workbench](https://www.mysql.com/products/workbench/) (optional, for database management)

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/your-repo-name.git
cd your-repo-name
```

### 2. Set Up MySQL Database

- Ensure that MySQL is installed and running on your system.
- Create a new MySQL database for the project:

```sql
CREATE DATABASE LoginDB;
```

- Update the connection string in the `appsettings.json` file to point to your MySQL instance:

```json
"ConnectionStrings": {
  "LoginDbContext": "server=localhost;database=LoginDB;user=root;password=yourpassword;"
}
```

### 3. Install Required Packages

Make sure the following packages are installed (already included in the `.csproj` file):
- **Entity Framework Core Tools** (version 8.0.8)
- **MySQL Entity Framework Core Provider** (version 8.0.5)
- **Swashbuckle.AspNetCore** (version 6.4.0)

If needed, install them via the .NET CLI:

```bash
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.8
dotnet add package MySql.EntityFrameworkCore --version 8.0.5
dotnet add package Swashbuckle.AspNetCore --version 6.4.0
```

### 4. Migrations and Database Update

You have already used these commands to apply migrations:
- Add migration:
  ```bash
  dotnet ef migrations add InitialCreate
  ```
- Update the database:
  ```bash
  dotnet ef database update
  ```

### 5. Run the Project

Run the application using the .NET CLI:

```bash
dotnet run
```

The API will be available at: `http://localhost:5000/`

### 6. Access Swagger API Documentation

Once the project is running, Swagger documentation will be available at:
```
http://localhost:5000/swagger/index.html
```

## API Endpoints

### Get All Users
**GET** `/Login`

Returns a list of all users.

### Get User by ID
**GET** `/Login/users/q={Id}`

Returns the user that matches the specified `Id`.

### Create a New User
**POST** `/Login`

**Request Body:**

```json
{
    "FullName": "John Doe",
    "Email": "john@example.com"
}
```

Creates a new user with a generated `Id`.

### Update a User
**PUT** `/Login/{id}`

**Request Body:**

```json
{
    "FullName": "John Smith",
    "Email": "john.smith@example.com"
}
```

Updates the user information with the specified `Id`.

### Delete a User
**DELETE** `/Login/{id}`

Deletes the user with the specified `Id`.

