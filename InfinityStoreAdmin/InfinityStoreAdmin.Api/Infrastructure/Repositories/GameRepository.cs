using InfinityStoreAdmin.Api.Infrastructure.Database;
using InfinityStoreAdmin.Api.Shared.Entities;

namespace InfinityStoreAdmin.Api.Infrastructure.Repositories
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        public GameRepository(DatabaseContext context) : base(context, context.Games)
        {
        }
    }
}
