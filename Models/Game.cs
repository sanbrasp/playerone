namespace PlayerOne.Models;


internal class Game
{
    public int GameId { get; init; }
    public required string Title { get; init; }
    public int PlatformId { get; init; }
    public required string PlatformName { get; init; }
    public int GenreId { get; init; }
    public required string GenreName { get; init; }
    public int ReleaseYear { get; init; }
}