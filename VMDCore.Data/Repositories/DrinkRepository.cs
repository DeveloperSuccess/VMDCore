using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
