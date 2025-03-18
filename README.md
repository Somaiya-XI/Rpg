# 🏹 RPG API – ASP.NET Core 8.0

A role-playing game (RPG) API built with **ASP.NET Core 8**, featuring **JWT authentication**, **Entity Framework Core**, and **clean architecture** principles. This project is part of the [.NET 7 Web API & Entity Framework Jumpstart Course by Patrick God]().

## 🚀 Features

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

## 🛠 Tech Stack

- **C# & ASP.NET Core 8.0**
- **Entity Framework Core (EF Core)**
- **JWT Authentication**
- **SQL Server**
- **AutoMapper** for DTO mapping
- **Swagger (Swashbuckle)** for API documentation

---

## 📌 Getting Started

### 🔧 **Setup & Installation**

#### 1️⃣ Clone the Repository

```sh
git clone https://github.com/Somaiya-XI/Rpg.git
cd Rpg
```

#### 2️⃣ Configure the Database

Modify `appsettings.Development.json` or `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLExpress; Database=RPG; Trusted_Connection=true; TrustServerCertificate=true;"
}
```

Ensure **SQL Server is running**, and update connection settings if needed.

#### 3️⃣ Restore Dependencies

```sh
dotnet restore
```

#### 4️⃣ Apply Migrations & Seed Database

```sh
dotnet ef database update
```

#### 5️⃣ Run the API

```sh
dotnet watch run
```

API should now be available at `https://localhost:5113`.

---

## 🌜 Unified API Response Handling

The API uses a **ServiceResponse<T>** wrapper for all responses to ensure **consistent structure** and **better error handling**.

Example Response:
```json
{
  "data": { "id": 1, "name": "Frodo" },
  "isOk": true,
  "message": "Character retrieved successfully"
}
```
- **data**: Contains the actual response (e.g., character, weapon, etc.)
- **success**: Boolean indicating request status
- **message**: Human-readable status message

---

## 🎯 Data Transfer Objects (DTOs)

The API follows a **DTO pattern** to keep the data layer separate from the API layer. This:
- Prevents **over-exposure** of database models
- Allows **controlled transformation** of data
- Makes it **easier to extend features** without breaking the database schema

Example:  
Instead of exposing the **Character entity**, the API uses `GetCharacterDto`, ensuring only necessary fields are returned.

---

## 📜 API Endpoints

### 🔐 Authentication

| Method | Endpoint             | Description                |
|--------|----------------------|----------------------------|
| POST   | `/api/auth/register` | Register a new user        |
| POST   | `/api/auth/login`    | Login and aquire JWT token |

### 🏹 Characters

| Method | Endpoint                 | Description                        |
|--------|--------------------------|------------------------------------|
| POST   | `/api/characters`        | Create a new character             |
| GET    | `/api/characters/{id}`   | Get all characters (user-specific) |
| DELETE | `/api/characters/{id}`   | Delete character                   |
| PUT    | `/api/characters`        | Update character                   |
| GET    | `/api/characters/getAll` | Get all characters (user-specific) |

### 🔪 Weapons

| Method | Endpoint                 | Description                        |
|--------|--------------------------|------------------------------------|
| POST   | `/api/weapon`            | Assign weapon to character         |

### 🎭 Skills

| Method | Endpoint                 | Description                        |
|--------|--------------------------|------------------------------------|
| POST   | `/api/characters/skill`  | Assign skills to character         |

---

## 🔒 Authentication & Security

- Uses **JWT Authentication**
- Users can only manage their own characters
- Unauthorized access returns **403 Forbidden**


---

## 🤝 Contributing

Feel free to fork the repo, or submit PRs for improvements!




