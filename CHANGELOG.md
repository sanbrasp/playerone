# Changelog

All notable changes to **PlayerOne** are documented here.  
Format loosely follows [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

---

## [Unreleased]
> Features planned or in progress

- Delete and update games
- Search and filter by platform, genre, name, added_by, added_at
- Seed data for common platforms and genres
- Fancify the console output using Spectre
- Add more navigational freedom to the console
- Tidy up the console output

---

## [0.4.0] - 2026-05-06

### Added
- User login system with username and 6-character password
- First-time setup flow when no users exist in the database
- `UserRepository` with `GetAllUsers`, `AddUser` and `Login` methods
- `User` model with `UserId`, `Username` and `Password` properties
- `added_by` foreign key column on `games` table referencing `users`
- `added_on` timestamp column on `games` table, auto-set by MySQL on insert
- `AddedByUserID`, `AddedByUserName` and `AddedOn` properties on `Game` model
- Username and timestamp displayed in library view per game entry
- `IdFormatter.User` for formatting user IDs with `USR-` prefix

### Changed
- `GetAllGames` query now JOINs the `users` table to retrieve username alongside game data
- `AddGame` INSERT now includes `added_by` field
- Game display in library now shows added date and username instead of raw user ID
- Login retries on wrong password instead of exiting

### Fixed
- `added_by` field missing from INSERT causing MySQL constraint crash
- `PickOrCreateGenre` refactored to return full `Genre` object instead of int ID
- `PlatformName` and `GenreName` now resolved from already-fetched lists instead of extra user input

---

## [0.3.0] - 2026-05-06

### Added
- GitHub Actions workflow for CodeQL static analysis (`codeql.yml`)
- GitHub Actions workflow for Gitleaks secret scanning (`security.yml`)
- Dependabot configuration for weekly NuGet dependency updates (`dependabot.yml`)
- GitLab CI/CD pipeline for Gitleaks secret scanning (`.gitlab-ci.yml`)
- `.gitignore` based on community C# template
- `appsettings.example.json` as a safe template for connection string configuration
- `.env.example` for Docker credentials template
- `Docker/.env` added to `.gitignore` to prevent credential exposure
- `appsettings.json` added to `.gitignore`
- `README.md` with background, setup instructions, tools table and acknowledgements

### Changed
- `docker-compose.yml` updated to use environment variables from `.env` instead of hardcoded credentials
- Local function signatures in `Program.cs` simplified — `repo` parameter removed, outer scope used directly

### Fixed
- Docker container failing to start due to port 3306 already allocated
- `appsettings.json` not found at runtime — fixed via `.csproj` `CopyToOutputDirectory` setting
- `USE player_one` not being picked up in DBeaver — fixed by selecting database from toolbar

---

## [0.2.0] - 2026-05-05

### Added
- `IdFormatter` static class with `Platform`, `Genre`, `Game` and `User` formatters using `PLT-`, `GNR-`, `GAM-` and `USR-` prefixes with zero-padded 3-digit numbers
- `InputHelpers` imported from semester project with `ReadRequiredString`, `ReadInt` and `ReadMenuChoice`
- `ValidationHelpers` and `IDFormat` enum imported from semester project
- Inline genre creation from within the `AddGame` flow — no need to exit and add separately
- Cancel option (`0`) at platform and genre selection steps
- `PickOrCreateGenre` local function for encapsulated genre picking logic
- `AppSettings` class moved to `Settings/AppSettings.cs` with `required` and `init` modifiers
- Platform and genre guard checks — friendly message if none exist when adding a game

### Changed
- All model string properties marked `required` for nullability correctness
- All classes changed from `public` to `internal` where appropriate
- `IdFormatter`, `InputHelpers` and `ValidationHelpers` confirmed as `static`
- `Program.cs` uses `ReadMenuChoice` for main menu — raw `Console.ReadLine` removed
- Release year validated to sensible range (1970 to current year)

### Fixed
- `AddGenre` and `GetAllGenres` were merged into one broken method — separated correctly
- `Dictionary<string, string>` deserialisation issue in Visual Studio — replaced with typed `AppSettings` class
- Emoji rendering fixed by adding `Console.OutputEncoding = System.Text.Encoding.UTF8`

---

## [0.1.0] - 2026-05-05

### Added
- Initial project created with `dotnet new console -n PlayerOne`
- `MySqlConnector` NuGet package added
- Docker Compose setup for isolated MySQL 8 database container (`docker-compose.yml`)
- SQL init script (`Docker/init/01_schema.sql`) auto-run on fresh container startup
- Database schema with four tables: `platforms`, `genres`, `games` and `users`
- Foreign key relationships: `games` references `platforms`, `genres` and `users`
- DBeaver connected to Docker MySQL container with `allowPublicKeyRetrieval=true`
- `GameRepository` with `AddPlatform`, `GetAllPlatforms`, `AddGenre`, `GetAllGenres`, `AddGame` and `GetAllGames`
- `GetAllGames` uses SQL JOIN across `platforms`, `genres` and `users` tables
- `Game`, `Platform` and `Genre` model classes with `init` setters
- `Program.cs` with main menu loop and local functions: `ViewGames`, `AddGame`, `ManagePlatforms`, `ManageGenres`
- Project folder structure: `Models/`, `Data/`, `Helpers/`, `Settings/`, `Enums/`, `Docker/`, `Docs/`
- `appsettings.json` for connection string configuration

### Infrastructure
- MySQL running in Docker instead of local Windows service
- Persistent data volume (`player_one_data`) so data survives container restarts
- Dedicated MySQL user (`playerone_app`) with limited permissions recommended over root

---

*PlayerOne — keeping game nostalgia alive. 👾🎮*