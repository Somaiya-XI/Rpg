# <img src="https://github.com/user-attachments/assets/41ebfa41-9b73-4edf-832d-b8278a3f1688" width="28" height="28" /> RPG API – ASP.NET Core 8.0

A role-playing game (RPG) API built with **ASP.NET Core 8**, featuring **JWT authentication**, **Entity Framework Core**, and **clean architecture** principles. This project is part of the [.NET 7 Web API & Entity Framework Jumpstart Course by Patrick God]().

## <img src="https://github.com/user-attachments/assets/e0bd9245-1ffa-4143-8ad2-93a8bf57537b" width="24" height="24" /> Features

- **User Authentication** (JWT-based)
- **Character Management**
  - Users can create and manage their own characters
  - Assign a primary weapon
  - Add a list of skills
- **Authorization**
  - Users can only access and modify their own characters
- **EF Core + SQL Server** for database management
- **Unified API Response Handling** with `ServiceResponse<T>`

- **DTO Pattern** for better data separation

## <img src="https://github.com/user-attachments/assets/59fa73e0-2852-4f23-87fa-9fa798ab0293" width="24" height="24" /> Tech Stack

- **C# & ASP.NET Core 8.0**
- **Entity Framework Core (EF Core)**
- **JWT Authentication**
- **SQL Server**
- **AutoMapper** for DTO mapping
- **Swagger (Swashbuckle)** for API documentation


## <img src="https://github.com/user-attachments/assets/2f0f2732-9f18-49ef-ba5b-14a4de9fc13d" width="24" height="24" /> Getting Started

###  **Setup & Installation**

#### Clone the Repository

```sh
git clone https://github.com/Somaiya-XI/Rpg.git
cd Rpg
```

#### Configure the Database

Modify `appsettings.Development.json` or `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLExpress; Database=RPG; Trusted_Connection=true; TrustServerCertificate=true;"
}
```

Ensure **SQL Server is running**, and update connection settings if needed.

#### Restore Dependencies

```sh
dotnet restore
```

#### Apply Migrations & Seed Database

```sh
dotnet ef database update
```

#### Run the API

```sh
dotnet watch run
```

API should now be available at `https://localhost:5113`.



## <img src="https://github.com/user-attachments/assets/1bac9ad5-e884-426e-a5d6-bd2c37652f1e" width="22" height="22" /> Unified API Response Handling

The API uses a [**ServiceResponse<T>**](https://github.com/Somaiya-XI/Rpg/blob/main/Rpg/Models/ServiceResponse.cs) wrapper for all responses to ensure **consistent structure** and **better error handling**.

Example Response:
```json
{
  "data": { "id": 1, "name": "Frodo" },
  "isOk": true,
  "message": "Character retrieved successfully"
}
```
- **data**: Contains the actual response (e.g., character, weapon, etc.)
- **isOk**: Boolean indicating request status
- **message**: Human-readable status message


## <img src="https://github.com/user-attachments/assets/0bd40076-5760-4681-b89d-5789ccf21c7d" width="22" height="22" /> Data Transfer Objects (DTOs)

The API follows a **DTO pattern** to keep the data layer separate from the API layer. This:
- Prevents **over-exposure** of database models
- Allows **controlled transformation** of data
- Makes it **easier to extend features** without breaking the database schema

Example:  
Instead of exposing the **Character entity**, the API uses `GetCharacterDto`, ensuring only necessary fields are returned.


## <img src="https://github.com/user-attachments/assets/69b70908-842f-405f-8cb7-32fa4e82edc6" width="24" height="24" /> API Endpoints

### Authentication

| Method | Endpoint             | Description                |
|--------|----------------------|----------------------------|
| POST   | `/api/auth/register` | Register a new user        |
| POST   | `/api/auth/login`    | Login and aquire JWT token |

### Characters

| Method | Endpoint                 | Description                        |
|--------|--------------------------|------------------------------------|
| POST   | `/api/characters`        | Create a new character             |
| GET    | `/api/characters/{id}`   | Get all characters (user-specific) |
| DELETE | `/api/characters/{id}`   | Delete character                   |
| PUT    | `/api/characters`        | Update character                   |
| GET    | `/api/characters/getAll` | Get all characters (user-specific) |

### Weapons

| Method | Endpoint                 | Description                        |
|--------|--------------------------|------------------------------------|
| POST   | `/api/weapon`            | Assign weapon to character         |

### Skills

| Method | Endpoint                 | Description                        |
|--------|--------------------------|------------------------------------|
| POST   | `/api/characters/skill`  | Assign skills to character         |


## <img src="https://github.com/user-attachments/assets/98f64c8d-34c6-4aff-9a01-b218f8c32952" width="24" height="24" /> Authentication & Security

- Uses **JWT Authentication**
- Users can only manage their own characters
- Unauthorized access returns **403 Forbidden**


## <img src="https://github.com/user-attachments/assets/679a662b-776b-44fd-973c-b643f519cc11" width="24" height="24" /> Contributing

Feel free to fork the repo, or submit PRs for improvements!




