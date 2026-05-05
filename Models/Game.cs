namespace PlayerOne.Models;


internal class Game
{
    public int GameId { get; init; }
    public string Title { get; init; }
    public int PlatformId { get; init; }
    public string PlatformName { get; init; }
    public int GenreId { get; init; }
    public string GenreName { get; init; }
    public int ReleaseYear { get; init; }
}