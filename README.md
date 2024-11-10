# Project Setup and Development Guide

⚠️ **Important**: Before working with the database, run `dotnet restore` in the backend directory to install required Entity Framework dependencies.

## Initial Setup

### Backend
1. Navigate to `2024-fall-psi/backend`
2. Run `dotnet restore` to install dependencies
3. Navigate to `2024-fall-psi/backend/backend` and run `dotnet run` to start the backend server

### Frontend
1. Navigate to `2024-fall-psi/frontend`
2. Run `npm install` to install dependencies
3. Run `npm run dev` to start the development server

## Format code

- To format backend: in `2024-fall-psi/backend` run `dotnet format --exclude backend/Migrations` (this will format both the main project and test directory)
- To format frontend: in `2024-fall-psi/frontend` run `npm run format`

## Add more games

- Create a `.tsx` file in `2024-fall-psi/frontend/src/games` and lay out your game components
- Add game info to `games` variable in `App.tsx`

## Database Setup and Management

### Initial Setup using Docker

These commands should be run in the base directory:

1. Start the containers needed for the database:
```bash
docker compose -p psi up -d
```

2. To reset the database fully:
```bash
docker compose -p psi down -v
```
Then start the containers again using the command from step 1.

### Install Required Tools

Install the Entity Framework Core CLI tools:
```bash
dotnet tool install --global dotnet-ef
```

Note: All subsequent commands should be run in the 'backend/backend' directory, otherwise you'll have to specify the project manually.

### Database Migrations

#### Apply Existing Migrations

After setting up the database containers, apply existing migrations to create the database schema:
```bash
dotnet ef database update
```

#### Create New Database Tables

1. Make changes to your model classes in the C# code

2. Add your table to GamesDbContext.cs:
```csharp
public DbSet<YourModel> AimTrainerGameHighscores { get; set; }
```

Note: The name of your DbSet variable will be the name of the table in the database. Table names will be case-sensitive because of this. For example, with a DbSet named `AimTrainerGameHighscores`, you would need to use that exact casing in SQL queries (e.g., `TRUNCATE "AimTrainerGameHighscores"`).

3. Create a new migration:
```bash
dotnet ef migrations add YourMigrationName
```
Replace `YourMigrationName` with a descriptive name for your changes (e.g., `AddUserProfileTable`)

4. Apply the new migration:
```bash
dotnet ef database update
```

#### Common Migration Commands

- List all migrations:
```bash
dotnet ef migrations list
```

- Remove the last migration (if not yet applied to the database):
```bash
dotnet ef migrations remove
```

- Generate SQL script for all migrations:
```bash
dotnet ef migrations script
```

### Fixing Migration Mistakes

If you need to fix a migration that hasn't worked as expected:

1. Remove the last migration:
```bash
dotnet ef migrations remove
```

2. Update your model classes as needed

3. Reset the database:
```bash
docker compose -p psi down -v
docker compose -p psi up -d
```

4. Create a new migration:
```bash
dotnet ef migrations add YourMigrationName
```

5. Apply migrations again:
```bash
dotnet ef database update
```

This process gives you a clean slate to work from when a migration doesn't go as planned.

### Code-First Development Tips

1. Create your model classes in the appropriate directory (e.g., `Models/`)
2. Add your DbSet properties to the DbContext class:
```csharp
public DbSet<YourModel> AimTrainerGameHighscores { get; set; }
```
3. Configure any relationships or constraints in the DbContext's OnModelCreating method
4. Create and apply migrations as described above

#### Example Model Class
```csharp
using System.ComponentModel.DataAnnotations;

namespace backend.AimTrainerGame.Models;

public class Highscore
{
    [Key]
    public Guid Id { get; set; }
    public string Username { get; set; }
    public int Score { get; set; }
    public DateTime Date { get; set; }
}
```

#### Example DbContext Class
```csharp
using backend.AimTrainerGame.Models;
using Microsoft.EntityFrameworkCore;

namespace backend;

public class GamesDbContext(DbContextOptions<GamesDbContext> options) : DbContext(options)
{
    public DbSet<Highscore> AimTrainerGameHighscores { get; set; }
}
```

For a complete example of how to implement everything together (models, controllers, and services), check out the `AimTrainerGame` directory in the backend project. It demonstrates:
- Model structure
- Controller setup
- Service implementation
- Database configuration
- API endpoint organization