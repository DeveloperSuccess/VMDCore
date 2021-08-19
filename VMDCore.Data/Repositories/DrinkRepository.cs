using VMDCore.Data.Interfaces;
using VMDCore.Data.Models;

namespace VMDCore.Data.Repositories
{
    public class DrinkRepository : BaseRepository<Drink>, IDrinkRepository
    {
        public DrinkRepository(VmdDbContext context) : base(context)
        {
        }
    }
}
