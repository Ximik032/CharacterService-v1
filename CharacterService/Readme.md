# CharacterService

**Tech stack:** .NET 9, PostgreSQL, EF Core, Swagger

## Description
CharacterService is a microservice for creating, storing and managing roleplay characters.

The service is designed as part of a future roleplay chat platform powered by local and remote LLM models.
## Features

- Create character
- Get character by id
- Update character
- Delete character
- Get user characters
- Pagination
- Validation
- Error handling

## Technologies

- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- FluentValidation
- Swagger

## Architecture

```text
Controller
    ↓
Application Service
    ↓
Repository
    ↓
Entity Framework Core
    ↓
PostgreSQL
```

## Project Structure

```text
Application
├── DTOs
├── Interfaces
├── Mappers
├── Services
├── Validators
├── Exceptions

Persistence
├── Migrations

Domain
├── Entities
├── Exceptions
├── Abstractions

Infrastructure
├── Data
├── Repositories
├── Services
├── Extensions

WebApi
├── Controllers
├── Extensions
├── Middleware
```

## Database

Main entity:

Character

Fields:

- Id
- UserId
- Name
- Description
- SystemPrompt
- Background
- Traits
- Quirks
- Skills
- CreatedAt
- UpdatedAt

Character data is stored in PostgreSQL.

JSONB is used for:
- Background
- Traits
- Quirks
- Skills

## API Endpoints

| Method | Route | Description |
|----------|----------|----------|
| GET | /api/character/{id} | Get character by id |
| GET | /api/character | Get characters |
| GET | /api/character/user/{userId} | Get user characters |
| POST | /api/character/{userId} | Create character |
| PATCH | /api/character/{id} | Update character |
| DELETE | /api/character/{id} | Delete character |
| DELETE | /api/character/user/{userId} | Delete all user characters |

Detailed request and response schemas are available through Swagger UI.git

## Running Project

Apply migrations:

```bash
dotnet ef database update
```

Run application:

```bash
dotnet run
```

Open Swagger:

```text
https://localhost:{port}/swagger
```


## Error Handling

The service uses centralized exception handling middleware.

Example:

```json
{
  "error": "NOT_FOUND",
  "message": "Character not found",
  "traceId": "..."
}
```

## Future Improvements

### Quality

- Unit Tests
- Integration Tests
- Docker Support

### Features

- Character tags
- Categories
- Visibility settings
- Advanced search
- Import / Export
- Character ratings