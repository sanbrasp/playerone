using System.Text.Json;
using PlayerOne.Data;
using PlayerOne.Models;
using PlayerOne.Helpers;
using PlayerOne.Settings;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// Load connection string from appsettings.json
var json = File.ReadAllText("appsettings.json");
var settings = JsonSerializer.Deserialize<AppSettings>(json);
if (string.IsNullOrWhiteSpace(settings?.ConnectionString))
{
    Console.WriteLine("Connection string is missing from appsettings.json. Please provide a valid connection string.");
    return;
}
var connectionString = settings.ConnectionString;


// Single repository instance
var repo = new GameRepository(connectionString);

// ==== Main Loop ====
bool running = true;

while (running)
{
    Console.WriteLine("\n=== 👾 PlayerOne 🎮 ===");
    Console.WriteLine("1. View all games.");
    Console.WriteLine("2. Add a game.");
    Console.WriteLine("3. Manage platforms.");
    Console.WriteLine("4. Manage genres.");
    Console.WriteLine("0. Exit.");

    switch (InputHelpers.ReadMenuChoice("\nSelect an option: ", 0, 4))
    {
        case 1: ViewGames(repo); break;
        case 2: AddGame(repo); break;
        case 3: ManagePlatforms(repo); break;
        case 4: ManageGenres(repo); break;
        case 0: running = false; break;
        default: Console.WriteLine("Invalid choice... You need to read more."); break;
    }
}


// ==== View Games ====
void ViewGames(GameRepository repo)
{
    var games = repo.GetAllGames();
    
    if (games.Count == 0)
    {
        Console.WriteLine("\nNo games added yet!");
        return;
    }

    Console.WriteLine("\n=== Your Library ===");
    foreach (var game in games)
    {
        Console.WriteLine(
            $"- [{IdFormatter.Game(game.GameId)}] {game.Title} ({game.ReleaseYear})\n" +
            $"- [{IdFormatter.Platform(game.PlatformId)}]: {game.PlatformName}\n" +
            $"- [{IdFormatter.Genre(game.GenreId)}]: {game.GenreName}");
    }
}


// ==== Add Game ====
void AddGame(GameRepository repo)
{
    var title = InputHelpers.ReadRequiredString("\nGame Title: ");

    int year;
    while (true)
    {
        year = InputHelpers.ReadInt("Release Year: ");
        if (year >= 1970 && year <= DateTime.Now.Year)
            break;
        Console.WriteLine($"Please enter a sensible year between 1970 and {DateTime.Now.Year}");
    }

    // === Platforms ===
    var platforms = repo.GetAllPlatforms();
    if (platforms.Count == 0)
    {
        Console.WriteLine("No platforms registered yet - add one first.");
        return;
    }


    Console.WriteLine("\n=== Platforms ===");
    foreach (var p in platforms)
    {
        Console.WriteLine($" [{IdFormatter.Platform(p.PlatformId)}] {p.PlatformName}");
    }

    int platformId = InputHelpers.ReadMenuChoice("Platform ID (or '0' to go back): ", 1, platforms.Count);
    if (platformId == 0)
    {
        Console.WriteLine("Cancelling...");
        return;
    }


    // === Genres ===
    int genreId = PickOrCreateGenre(repo);
    if (genreId == 0)
    {
        Console.WriteLine("Cancelling...");
        return;
    }


    // Build the game object, repo handles the insert
    var game = new Game
    {
        Title = title,
        ReleaseYear = year,
        PlatformId = platformId,
        GenreId = genreId
    };

    repo.AddGame(game);
    Console.WriteLine($"\n✔️ '{title}' added to your library!");
}



// ==== Manage Platforms ====
void ManagePlatforms(GameRepository repo)
{
    var platforms = repo.GetAllPlatforms();

    Console.WriteLine("\n=== Platforms ===");
    if (platforms.Count == 0)
    {
        Console.WriteLine("No platforms registered yet!");
    }
    else
    {
        foreach (var p in repo.GetAllPlatforms())
        {
            Console.WriteLine($"  [{IdFormatter.Platform(p.PlatformId)}] {p.PlatformName}");
        }
    }

    Console.WriteLine("1. Add new platform.");
    Console.WriteLine("0. Go back.");

    switch (InputHelpers.ReadMenuChoice("\nChoice: ", 0, 1))
    {
        case 1:
            var name = InputHelpers.ReadRequiredString("Platform Name: ");
            repo.AddPlatform(name);
            Console.WriteLine($"✔ Platform '{name}' added!");
            break;
        case 0:
            break;
    }
}


// ==== Manage Genres ====
void ManageGenres(GameRepository repo)
{
    var genres = repo.GetAllGenres();

    Console.WriteLine("=== Genres ===");
    if (genres.Count == 0)
    {
        Console.WriteLine("No genres yet - add one first.");
    }
    else
    {
        foreach (var g in repo.GetAllGenres())
        {
            Console.WriteLine($"  [{IdFormatter.Genre(g.GenreId)}] {g.GenreName}");
        }
    }

    Console.WriteLine("1. Add new genre.");
    Console.WriteLine("0. Go back.");

    switch (InputHelpers.ReadMenuChoice("Choice: ", 0, 1))
    {
        case 1:
            var name = InputHelpers.ReadRequiredString("Genre name: ");
            repo.AddGenre(name);
            Console.WriteLine($"✔ Genre '{name}' added!");
            break;
        case 0:
            break;
    }
}

// Lets the user pick an existing genre or create a new one inline.
// Returns 0 if the user cancels.
int PickOrCreateGenre(GameRepository repo)
{
    while (true)
    {
        var genres = repo.GetAllGenres();

        Console.WriteLine("\n=== Genres ===");

        if (genres.Count == 0)
        {
            Console.WriteLine("  No genres registered yet.");
        }
        else
        {
            foreach (var g in genres)
                Console.WriteLine($" [{IdFormatter.Genre(g.GenreId)}] {g.GenreName}");
        }

        // Always show the create and cancel options
        Console.WriteLine("\n  [C] Create new genre");
        Console.WriteLine("  [0] Cancel");
        Console.Write("Genre ID: ");

        var input = Console.ReadLine()?.Trim().ToLower();

        if (input == "0")
            return 0;

        if (input == "c")
        {
            // Inline genre creation — loops back to picker afterwards
            var name = InputHelpers.ReadRequiredString("New genre name: ");
            repo.AddGenre(name);
            Console.WriteLine($"✔️ Genre '{name}' created!");
            continue; // Loop back so user can now select the new genre
        }

        // Try to parse as a number and validate against the list
        if (int.TryParse(input, out int picked) &&
            genres.Any(g => g.GenreId == picked))
            return picked;

        Console.WriteLine("Invalid choice — enter a genre ID, C to create, or 0 to cancel.");
    }
}