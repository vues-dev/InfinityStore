using InfinityStoreAdmin.BlazorApp.Services.Models;

namespace InfinityStoreAdmin.BlazorApp.Services
{
    public class GameService
    {
        private List<GameEntity> _games = new();

        public GameService()
        {
            var random = new Random();

            for (int i = 1; i < 21; i++)
            {
                _games.Add(new GameEntity
                {
                    Id = Guid.NewGuid(),
                    Title = GenerateRandomString(),
                    Description = $"Description {i}",
                    Price = random.Next(3, 10),
                    Image = $"https://picsum.photos/seed/{i}/200/300"
                });
            }
        }

        public async Task<GetGamesResponse> GetGamesAsync(GetGamesRequest request)
        {
            GetGamesResponse result = new();

            await Task.Delay(100);

            // filtering

            var gamesProcessed =
                _games.Where(x => string.IsNullOrEmpty(request.SearchString) || x.Title.ToLower().Contains(request.SearchString.ToLower()));

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
                Price = x.Price.ToString("C"),
                Image = x.Image
            }).ToList();

            return result;
        }

        public async Task DeleteGameById(Guid id)
        {
            await Task.Delay(100);

            var game = _games.FirstOrDefault(x => x.Id == id);

            if (game != null)
            {
                _games.Remove(game);
            }
        }

        private string GenerateRandomString()
        {
            Random rand = new Random();
            int length = rand.Next(5, 11); // Random length between 5 and 10

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[rand.Next(s.Length)]).ToArray());
        }
    }
}
