using MetacriticAPI.Contracts;
using MetacriticAPI.Contracts.Game;
using MetacriticAPI.Controllers;
using Xunit;
using static System.Net.WebRequestMethods;

namespace IntegrationTests
{
    public class GameControllerTests
    {
        GameController gameController = new(new HttpClient(), "https://www.metacritic.com");

        [Fact]
        public async Task GetGameDetailsAsync_FireEmblemEngage_CorrectParameters_GetsOneResult2()
        {
            string url = "https://www.metacritic.com/game/switch/fire-emblem-engage";
            
            var gameDetails = await gameController.GetGameDetailsAsync(url);
            
            Assert.NotNull(gameDetails);
            Assert.Equal("Fire Emblem Engage", gameDetails.Title);
            Assert.Equal("Switch", gameDetails.Platform);
            Assert.Equal("Nintendo", gameDetails.Publisher);
            Assert.Equal("Jan 20, 2023", gameDetails.ReleaseDate);
            Assert.InRange(int.Parse(gameDetails.Metascore), 50, 100);
            Assert.InRange(double.Parse(gameDetails.Userscore), 50, 100);
            Assert.Equal("In a war against the Fell Dragon, four kingdoms worked together with heroes from other worlds to seal away this great evil. One-thousand years later, this seal has weakened and the Fell Dragon is about to reawaken. As a Divine Dragon, use rich strategies and robust customization to meet your destiny—to collect Emblem Rings and bring peace back to the Continent of Elyos.\r\rTeam up with iconic heroes from past Fire Emblem games\r\rSummon valiant heroes like Marth and Celica with the power of Emblem Rings and add their power to yours in this brand-new Fire Emblem story. Aside from merging appearances, Engaging lets you inherit weapons, skills, and more from these battle-tested legends. The turn-based, tactical battle system returns with a fresh cast of characters you can customize and Engage to carefully craft your strategy.", gameDetails.Summary);
            Assert.Equal("https://static.metacritic.com/images/products/games/9/e0ffbb047edcb35ec65801d8693fc347-98.jpg", gameDetails.BoxArtUrl);
            Assert.Equal("Intelligent Systems", gameDetails.Developer);
            
            Assert.NotNull(gameDetails.Genres);
            var genres = gameDetails.Genres.ToArray();

            Assert.Equal(3, genres.Length);
            Assert.Equal("Strategy", genres[0]);
            Assert.Equal("Turn-Based", genres[1]);
            Assert.Equal("Tactics", genres[2]);
        }

        [Fact]
        public async Task PerformSearchAsync_FireEmblemEngage_CorrectParameters_GetsOneResult()
        {

            var result = await gameController.PerformSearchAsync(new(
                "Fire Emblem Engage",
                "switch",
                SortMethod.Relevancy));

            Assert.NotNull(result);
            Assert.NotNull(result.QueryResultItems);
            Assert.Single(result.QueryResultItems);

            var item = result.QueryResultItems.First();

            Assert.Equal("Fire Emblem Engage", item.Title);
            Assert.Equal("Switch", item.Platform);
            Assert.Equal("2023", item.Year);
            Assert.InRange(int.Parse(item.Metascore), 50, 95);
            Assert.Equal("In a war against the Fell Dragon, four kingdoms worked together with heroes from other worlds to seal away this great evil. One-thousand years later, this seal has weakened and the Fell Dragon is...", item.Summary);
            Assert.Equal("/game/switch/fire-emblem-engage", item.GamePath);
            Assert.Equal("https://static.metacritic.com/images/products/games/9/e0ffbb047edcb35ec65801d8693fc347-78.jpg", item.ThumbnailUrl);
        }

        [Fact]
        public async Task PerformSearchAsync_NonExistingGame_ShouldGetZeroResults()
        {
            var results = await gameController.PerformSearchAsync(new("gamethatshouldnotexist", "", SortMethod.Relevancy));

            Assert.NotNull(results);
            Assert.Empty(results.QueryResultItems);
        }
    }
}