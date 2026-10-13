using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string EndpointName = "GetGame";
    private static readonly List<GameDto> games = [
        new(1, "Street Fighter II", "Fighting",19.99M, new DateOnly(1992, 7, 15)),
        new(2, "Street Fighter III", "Fighting",25.99M, new DateOnly(1995, 6, 25)),
        new(3, "Pokemon", "Playing",30.99M, new DateOnly(2001, 5, 25))
    ];

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        //GET /games
        group.MapGet("/", () => games);

        //GET /game/1
        group.MapGet("/{id}", (int id) =>
        {
            var game = games.Find(g => g.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        }).WithName(EndpointName);

        //POST /games
        group.MapPost("/", (CreateGameDto game) =>
        {
            var newGame = new GameDto(5, game.Name, game.Genre, game.Price, game.ReleaseDate);
            games.Add(newGame);

            return Results.CreatedAtRoute(EndpointName, new { id = newGame.Id }, game);
        });


        //PUT /games/1
        group.MapPut("/{id}", (int id, UpdateGameDto game) =>
        {
            var index = games.FindIndex(g => g.Id == id);

            if (index == -1) return Results.NotFound();

            games[index] = new GameDto(
                id,
                game.Name,
                game.Genre,
                game.Price,
                game.ReleaseDate
            );

            return Results.NoContent();

        });


        //DELETE /games/1
        group.MapDelete("/games/{id}", (int id) =>
        {
            games.RemoveAll(g => g.Id == id);

            return Results.NoContent();
        });
    }
}
