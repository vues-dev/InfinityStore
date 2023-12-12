using InfinityStoreAdmin.BlazorApp.Data;
using InfinityStoreAdmin.BlazorApp.Services.Models;
using System.Globalization;

namespace InfinityStoreAdmin.BlazorApp.Services
{
    public class GameService
    {
        public async Task<GetGamesResponse> GetGamesAsync(GetGamesRequest request)
        {
            GetGamesResponse result = new();

            await Task.Delay(100);

            // filtering

            var gamesProcessed =
                GameData.Games.Where(x => string.IsNullOrEmpty(request.SearchString) || x.Title.ToLower().Contains(request.SearchString.ToLower()));

            //sorting

            if (request.IsTitleUp.HasValue && request.IsPriceUp.HasValue)
                throw new Exception("Only one sorting should be applied");

            if (request.IsTitleUp.HasValue)
            {
                gamesProcessed = request.IsTitleUp.Value ? gamesProcessed.OrderBy(x => x.Title) : gamesProcessed.OrderByDescending(x => x.Title);
            }

            if (request.IsPriceUp.HasValue)
            {
                gamesProcessed = request.IsPriceUp.Value ? gamesProcessed.OrderBy(x => x.Price) : gamesProcessed.OrderByDescending(x => x.Price);
            }

            result.TotalGames = gamesProcessed.Count();

            //pagination
            gamesProcessed = gamesProcessed
                .Skip((request.CurrentPage - 1) * request.ItemsPerPage)
                .Take(request.ItemsPerPage);

            result.Games = gamesProcessed.Select(x => new GameModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price.ToString("C", CultureInfo.CreateSpecificCulture("en-US")),
                ImageUrl = x.Image
            }).ToList();

            return result;
        }

        public async Task DeleteGameById(Guid id)
        {
            await Task.Delay(100);

            var game = GameData.Games.FirstOrDefault(x => x.Id == id);

            if (game != null)
            {
                GameData.Games.Remove(game);
            }
        }

        public Task<GameModel> GetGameByIdAsync(Guid itemId)
        {
            var game = GameData.Games.FirstOrDefault(x => x.Id == itemId);

            if (game == null)
                return Task.FromResult<GameModel>(null);

            return Task.FromResult(new GameModel
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                Price = PriceToString(game.Price),
                ImageUrl = game.Image
            });
        }

        //update
        public async Task UpdateGameAsync(GameModel request)
        {
            await Task.Delay(100);
            var gameToUpdate = GameData.Games.FirstOrDefault(x => x.Id == request.Id);
            if (gameToUpdate == null)
                throw new Exception("Game not found");
            gameToUpdate.Title = request.Title;
            gameToUpdate.Description = request.Description;
            var price = PriceToDecimal(request.Price);
            gameToUpdate.Price = price;
            gameToUpdate.Image = request.ImageUrl;
        }

        private string PriceToString(decimal price)
        {
            return price.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
        }

        private decimal PriceToDecimal(string price)
        {
            var priceString = price.Replace("$", string.Empty);

            if (decimal.TryParse(priceString, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-US"), out var result))
            {
                return result;
            }

            return 0;
        }
    }
}
