using ExampleApp;
using MetacriticAPI.Contracts;
using MetacriticAPI.Contracts.Game;
using MetacriticAPI.Controllers;
using System.Text.Json;

var gameController = new GameController(new HttpClient(), "https://www.metacritic.com/");
var serializerOptions = new JsonSerializerOptions() { WriteIndented = true };

while (true)
{
    await ExecutionHelper.TryExecuteAsync(async () =>
    {
        Console.Write("Search game (1) or get game details from page (2): ");
        var actionKey = Console.ReadLine();
        Console.WriteLine();

        if (actionKey == "1")
        {
            await SearchGame(gameController, serializerOptions);
        }
        else if (actionKey == "2")
        {
            await GetGameDetails(gameController, serializerOptions);
        }
        else
        {
            Environment.Exit(0);
        }
    });
}

static async Task SearchGame(GameController gameController, JsonSerializerOptions serializerOptions)
{
    Console.Write("Enter game name: ");
    string? gameName = Console.ReadLine();

    Console.Write("Specify game platform (leave empty to avoid filtering on platform): ");
    string? platform = Console.ReadLine();

    var queryParams = new GameQueryParameters(
        gameName ?? throw new NullReferenceException(nameof(gameName)),
        platform ?? throw new NullReferenceException(nameof(platform)),
        SortMethod.Relevancy);

    await ExecutionHelper.ExecuteAndLogDurationAsync(async () =>
    {
        var queryResult = await gameController.PerformSearchAsync(queryParams);
        Console.WriteLine(JsonSerializer.Serialize(queryResult, serializerOptions));
    });
}
static async Task GetGameDetails(GameController gameController, JsonSerializerOptions serializerOptions)
{
    Console.Write("Provide absolute or relative game page URL: ");
    var url = Console.ReadLine() ?? throw new NullReferenceException("url");

    await ExecutionHelper.ExecuteAndLogDurationAsync(async () =>
    {
        var result = await gameController.GetGameDetailsAsync(url);
        Console.WriteLine(JsonSerializer.Serialize(result, serializerOptions));
    });
}