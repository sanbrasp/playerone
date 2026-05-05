using MySqlConnector;
using PlayerOne.Models;

namespace PlayerOne.Data;

internal class GameRepository
{
    private readonly string _connectionString;

    public GameRepository(string connectionString)
    {
        _connectionString = connectionString;
    }


    // ===== Platforms =====

    public void AddPlatform(string name)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand(
            "INSERT INTO platforms (name) VALUES (@name)", connection);
        command.Parameters.AddWithValue("@name", name);
        command.ExecuteNonQuery();
    }

    public List<Platform> GetAllPlatforms()
    {
        var platforms = new List<Platform>();

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand(
            "SELECT * FROM platforms", connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            platforms.Add(new Platform
            {
                PlatformId = reader.GetInt32("platformid"),
                PlatformName = reader.GetString("name")
            });
        }
        return platforms;
    }

    // ==== Genres =====

    public void AddGenre(string name)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand(
            "INSERT INTO genres (name) VALUES (@name)", connection);
        command.Parameters.AddWithValue("@name", name);
        command.ExecuteNonQuery();
    }

    public List<Genre> GetAllGenres()
    {
        var genres = new List<Genre>();

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand(
            "SELECT * FROM genres", connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            genres.Add(new Genre
            {
                GenreId = reader.GetInt32("genreid"),
                GenreName = reader.GetString("name")
            });
        }
        return genres;
    }

    // ===== Games =====

    public void AddGame(Game game)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand(@"
            INSERT INTO games (title, platform_id, genre_id, release_year, added_by)
            VALUES (@title, @platformId, @genreId, @releaseYear, @addedBy)", connection);

        command.Parameters.AddWithValue("@title", game.Title);
        command.Parameters.AddWithValue("@platformId", game.PlatformId);
        command.Parameters.AddWithValue("@genreId", game.GenreId);
        command.Parameters.AddWithValue("@releaseYear", game.ReleaseYear);
        command.Parameters.AddWithValue("@addedBy", game.AddedByUserID);
        command.ExecuteNonQuery();
    }

    public List<Game> GetAllGames()
    {
        var games = new List<Game>();

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();


        var command = new MySqlCommand(@"
            SELECT g.gameid, g.title, g.release_year, g.added_on, 
            p.platformid, p.name AS platform_name,
            ge.genreid, ge.name AS genre_name,
            u.userid, u.username
            FROM games g
            JOIN platforms p ON g.platform_id = p.platformid
            JOIN genres ge ON g.genre_id = ge.genreid
            JOIN users u ON g.added_by = u.userid", connection);

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            games.Add(new Game
            {
                GameId = reader.GetInt32("gameid"),
                Title = reader.GetString("title"),
                ReleaseYear = reader.GetInt32("release_year"),
                PlatformId = reader.GetInt32("platformid"),
                PlatformName = reader.GetString("platform_name"),
                GenreId = reader.GetInt32("genreid"),
                GenreName = reader.GetString("genre_name"),
                AddedByUserID = reader.GetInt32("userid"),
                AddedByUserName =  reader.GetString("username"),
                AddedOn = reader.GetDateTime("added_on")
            });
        }
        return games;
    }
}